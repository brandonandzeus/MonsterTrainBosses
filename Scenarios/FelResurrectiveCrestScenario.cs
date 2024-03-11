using MoreBosses.Bosses;
using MoreBosses.Sins;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;

namespace MoreBosses.Scenarios
{
    public class FelResurrectiveCrestScenario
    {
        public static readonly string ID = "FelResurrectiveCrest";
        public static readonly RegistrationPriority Priority = RegistrationPriority.SCENARIO;

        public static void AutoRegister()
        {
            var battleNameKey = "ScenarioData_battleNameKey-cf97a0fcb7af1b05-4c3815c1e2e409648a85f66f24f62af0-v2";
            var battleDescriptionKey = "ScenarioData_battleDescriptionKey-1661d0fc64d51c74-38638b26641e73d4cac4444281b6364b-v2";
            // If More Boss Information installed use a more descriptive name and description.
            if (PluginManager.PluginExists("com.rising_dusk.morebossinformation"))
            {
                battleNameKey = "FelResurrectiveCrestMBI_ScenarioData_BattleNameKey";
                battleDescriptionKey = "FelResurrectiveCrestMBI_ScenarioData_BattleDescriptionKey";
            }

            var bossCharacter = CustomCharacterManager.GetCharacterDataByID(FelResurrective.ID);
            var bossCharacterPact = CustomCharacterManager.GetCharacterDataByID(FelResurrectivePact.ID);

            var baseScenario = CustomScenarioManager.GetScenarioDataByID(VanillaScenarioIDs.FelRage);
            var sin = CustomScenarioManager.GetSinsDataByID(ResurrectiveCrest.ID);

            var quillMarksman = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.QuillMarksman);
            var lightHarnesser = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.LightHarnesser);

            SpawnWaveHelper spawnWaveHelper = new SpawnWaveHelper(baseScenario);
            spawnWaveHelper.ReplaceCharacter(quillMarksman, lightHarnesser);
            spawnWaveHelper.ReplaceCharacterInWave(0, 0, 0, quillMarksman);
            spawnWaveHelper.SwapWaves(1, 2);
            spawnWaveHelper.SwapWaves(3, 5);
            spawnWaveHelper.SwapWaves(2, 3);
            spawnWaveHelper.ReplaceOuterTrainBoss(bossCharacter, bossCharacterPact);

            // Make a scenario.
            ScenarioDataBuilder scenario = new ScenarioDataBuilder
            {
                ScenarioID = ID,
                BattleNameKey = battleNameKey,
                BattleDescriptionKey = battleDescriptionKey,
                Distance = 5,
                SpawnPattern = spawnWaveHelper.Build(),
                BossVariants =
                {
                    // Important if non flying boss chooses a boss randomly.
                    // But still must be filled out with something otherwise crash.
                    bossCharacter,
                },
                // Controls the map node in the map screen.
                CompletedSpritePath = "Assets/Icons/BattleNodes_Fel_Completed.png",
                InactiveSpritePath = "Assets/Icons/BattleNodes_Fel_Resurrective_Inactive.png",
                ActiveSpritePath = "Assets/Icons/BattleNodes_Fel_Resurrective_Active.png",
                HighlightSpritePath = "Assets/Icons/BattleNodes_MajorBoss_Highlight.png",
                EnemyBlessingData =
                {
                    // Add your sins data here.
                    sin,
                },
                BattleTrackNameData = "BattleMusic_Fel",
                Difficulty = ScenarioDifficulty.Boss,
                DisplayedEnemies =
                {
                },
                BackgroundData = baseScenario.GetBackgroundData(),
                BossIcon = "Assets/Icons/Fel_Resurrective_Icon.png",
            };

            var scenarioData = scenario.Build();
            CustomScenarioManager.RegisterCustomScenario(scenarioData, scenario.Distance);
        }
    }
}
