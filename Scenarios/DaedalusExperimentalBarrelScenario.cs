using MoreBosses.Bosses;
using MoreBosses.Enemies;
using MoreBosses.Sins;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;

namespace MoreBosses.Scenarios
{
    public class DaedalusExperimentalBarrelScenario
    {
        public static readonly string ID = "DaedalusExperimentalBarrel";
        public static readonly RegistrationPriority Priority = RegistrationPriority.SCENARIO;

        public static void AutoRegister()
        {
            var battleNameKey = "ScenarioData_battleNameKey-cf97a0fcb7af1b05-4c3815c1e2e409648a85f66f24f62af0-v2";
            var battleDescriptionKey = "DaedalusExperimentalBarrel_ScenarioData_BattleDescriptionKey";
            // If More Boss Information installed use a more descriptive name and description.
            if (PluginManager.PluginExists("com.rising_dusk.morebossinformation"))
            {
                battleNameKey = "DaedalusExperimentalBarrelMBI_ScenarioData_BattleNameKey";
                battleDescriptionKey = "DaedalusExperimentalBarrelMBI_ScenarioData_BattleDescriptionKey";
            }

            var bossCharacter = CustomCharacterManager.GetCharacterDataByID(DaedalusSlickExplosive.ID);
            var bossCharacterPact = CustomCharacterManager.GetCharacterDataByID(DaedalusSlickExplosivePact.ID);

            var baseScenario = CustomScenarioManager.GetScenarioDataByID(VanillaScenarioIDs.DaedalusArmor);
            var experimentalBarrel = CustomScenarioManager.GetSinsDataByID(ExperimentalBarrel.ID);

            var overchargedApprentice = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.OverchargedApprentice);
            var apprenticeOfLight = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.ApprenticeofLight);

            SpawnWaveHelper spawnWaveHelper = new SpawnWaveHelper(baseScenario);
            spawnWaveHelper.ReplaceCharacter(overchargedApprentice, apprenticeOfLight);
            spawnWaveHelper.ReplaceOuterTrainBoss(bossCharacter, bossCharacterPact);

            // Make a scenario.
            ScenarioDataBuilder scenario = new ScenarioDataBuilder
            {
                ScenarioID = ID,
                BattleNameKey = battleNameKey,
                BattleDescriptionKey = battleDescriptionKey,
                Distance = 2,
                SpawnPattern = spawnWaveHelper.Build(),
                BossVariants =
                {
                    // Important if non flying boss chooses a boss randomly.
                    // But still must be filled out with something otherwise crash.
                    bossCharacter,
                },
                // Controls the map node in the map screen.
                CompletedSpritePath = "Assets/Icons/BattleNodes_Daedalus_Completed.png",
                InactiveSpritePath = "Assets/Icons/BattleNodes_Daedalus_SlickExplosive_Inactive.png",
                ActiveSpritePath = "Assets/Icons/BattleNodes_Daedalus_SlickExplosive_Active.png",
                HighlightSpritePath = "Assets/Icons/BattleNodes_MajorBoss_Highlight.png",
                EnemyBlessingData =
                {
                    // Add your sins data here.
                    experimentalBarrel,
                },
                BattleTrackNameData = "BattleMusic_Daedalus",
                Difficulty = ScenarioDifficulty.Boss,
                DisplayedEnemies =
                {
                },
                BackgroundData = baseScenario.GetBackgroundData(),
                BossIcon = "Assets/Icons/Daedalus_Slick_Icon.png",
            };

            var scenarioData = scenario.Build();
            CustomScenarioManager.RegisterCustomScenario(scenarioData, scenario.Distance);
        }
    }
}
