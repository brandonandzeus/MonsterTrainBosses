using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using UnityEngine.Serialization;
using UnityEngine;
using static SpawnGroupData;
using static SpawnPatternData;
using System.Security.Cryptography;

namespace MoreBosses
{
    public class SpawnWaveHelper
    {
        SpawnPatternDataBuilder Builder { get; set; }

        public SpawnWaveHelper(ScenarioData scenarioData)
        {
            var spawnPattern = scenarioData.GetSpawnPattern();

            Builder = new SpawnPatternDataBuilder();

            Builder.IsLoopingScenario = spawnPattern.GetIsLoopingScenario();
            Builder.ForceCrystalVariant = !spawnPattern.GetIncreaseHellforgedVariantSize();
            Builder.BossType = spawnPattern.GetBossType();
            Builder.BossCharacter = spawnPattern.GetOuterTrainBossCharacter(false);
            Builder.HardBossCharacter = spawnPattern.GetOuterTrainBossCharacter(true);
            Builder.BossCharactersPerFloor.AddRange(spawnPattern.GetBossCharactersPerFloor());


            var spawnGroupWaves = (SpawnPatternData.SpawnGroupPoolsDataList) AccessTools.Field(typeof(SpawnPatternData), "spawnGroupWaves").GetValue(spawnPattern);
            for (int i = 0; i < spawnPattern.GetNumGroups(); i++)
            {
                var spawnGroupPoolDataBuilder = new SpawnGroupPoolDataBuilder();

                SpawnGroupPoolData spawnGroupPool = spawnGroupWaves[i];
                var possibleGroups = (SpawnGroupPoolData.SpawnGroupDataList) AccessTools.Field(typeof(SpawnGroupPoolData), "possibleGroups").GetValue(spawnGroupPool);
                foreach (SpawnGroupData spawnGroup in possibleGroups)
                {
                    var spawnGroupDataBuilder = new SpawnGroupDataBuilder();
                    if (spawnGroup.HasWaveMessage())
                    {
                        // GetWaveMessage localizes.
                        spawnGroupDataBuilder.WaveMessageKey = (string) AccessTools.Field(typeof(SpawnGroupData), "waveMessageKey").GetValue(spawnGroup);
                    }
                    var characterDataContainerList = (SpawnGroupData.CharacterDataContainerList) AccessTools.Field(typeof(SpawnGroupData), "characterDataContainerList").GetValue(spawnGroup);
                    foreach (var character in characterDataContainerList)
                    {
                        var spawnCharacterDataBuilder = new SpawnCharacterDataBuilder();
                        spawnCharacterDataBuilder.CharacterData = character.Character;
                        spawnCharacterDataBuilder.UseBossCharacter = character.UseBossCharacter;
                        spawnCharacterDataBuilder.RequiredCovenant = character.Covenant;
                        spawnGroupDataBuilder.Characters.Add(spawnCharacterDataBuilder);
                    }
                    spawnGroupPoolDataBuilder.PossibleGroups.Add(spawnGroupDataBuilder);
                }

                Builder.SpawnGroupWaves.Add(spawnGroupPoolDataBuilder);
            }
        }

        /// <summary>
        /// Replaces all instances of one character with another.
        /// </summary>
        /// <param name="old">Character to replace</param>
        /// <param name="nnew">Character to replace with.</param>
        public void ReplaceCharacter(CharacterData old, CharacterData nnew)
        {
            foreach (var spawnGroupPool in Builder.SpawnGroupWaves)
            {
                foreach (var spawnGroupData in spawnGroupPool.PossibleGroups)
                {
                    foreach (var container in spawnGroupData.Characters)
                    {
                        if (container.CharacterData == old)
                        {
                            container.CharacterData = nnew;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Replace a Character in a particular wave, group, and position.
        /// </summary>
        /// <param name="waveNo">Wave number.</param>
        /// <param name="groupno">Possible Group number for randomized waves.</param>
        /// <param name="position">Remember this is in reverse. Index 0 is the backline.</param>
        /// <param name="nnew">CharacterData to replace with</param>
        public void ReplaceCharacterInWave(int waveNo, int groupno, int position, CharacterData nnew)
        {
            Builder.SpawnGroupWaves[waveNo].PossibleGroups[groupno].Characters[position].CharacterData = nnew;
        }

        /// <summary>
        /// Swaps a Spawn Wave 
        /// </summary>
        /// <param name="wave1">First wave number</param>
        /// <param name="wave2">Second wave number</param>
        public void SwapWaves(int wave1, int wave2)
        {
            var temp = Builder.SpawnGroupWaves[wave1];
            Builder.SpawnGroupWaves[wave1] = Builder.SpawnGroupWaves[wave2];
            Builder.SpawnGroupWaves[wave2] = temp;
        }

        /// <summary>
        /// Replaces the Outer Train boss.
        /// </summary>
        /// <param name="cov0">Covenant 0 variant</param>
        /// <param name="cov1">Covenent 1 varaint with pact shard upgrades.</param>
        public void ReplaceOuterTrainBoss(CharacterData cov0, CharacterData cov1)
        {
            Builder.BossCharacter = cov0;
            Builder.HardBossCharacter = cov1;
        }

        public SpawnPatternData Build()
        {
            return Builder.Build();
        }

        public void Print()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
            foreach (var spawnGroupPool in Builder.SpawnGroupWaves)
            {
                int j = 0;
                foreach (var spawnGroupData in spawnGroupPool.PossibleGroups)
                {
                    stringBuilder.Append(string.Format("Wave {0} Group {1}: ", i, j));
                    foreach (var container in spawnGroupData.Characters)
                    {
                        stringBuilder.Append(container.CharacterData.GetNameEnglish());
                        if (container.RequiredCovenant != null)
                            stringBuilder.Append(string.Format(" Cov: {0}", container.RequiredCovenant.AscensionLevel));
                        stringBuilder.Append(string.Format(" Boss: {0}", container.UseBossCharacter));
                        stringBuilder.Append("| ");
                    }
                    stringBuilder.Append('\n');
                    j++;
                }
                i++;
            }
            Trainworks.Trainworks.Log(stringBuilder.ToString());
        }
    }
}
