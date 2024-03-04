using MoreBosses.Bosses;
using MoreBosses.Sins;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;

namespace MoreBosses.Scenarios
{
    public class SeraphTheColdheartedScenario
    {
        public static readonly string ID = "SeraphTheColdhearted";
        public static readonly RegistrationPriority Priority = RegistrationPriority.LOW;

        public static void AutoRegister()
        {

            var battleDescriptionKey = "SeraphTheColdhearted_ScenarioData_BattleDescriptionKey";
            // If More Boss Information installed use a more descriptive description.
            if (PluginManager.PluginExists("com.rising_dusk.morebossinformation"))
            {
                battleDescriptionKey = "SeraphTheColdheartedMBI_ScenarioData_BattleDescriptionKey";
            }

            var frostbiteSeraph = CustomCharacterManager.GetCharacterDataByID(SeraphTheColdhearted.ID);
            var frostbiteSeraphPact = CustomCharacterManager.GetCharacterDataByID(SeraphTheColdheartedPact.ID);

            // 11/140 (Harvest armor 15)
            var darkwings = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.Darkwings);
            // 10/190
            var gildedwing = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.GildedWing);
            // 0/(15+100)
            var steelwing = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.Steelwings);
            // 15/3
            var lightwing = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.Lightwings);
            // 4/150/sweep
            var pyrewing = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.Pyrewings);

            var freezingEmblem = CustomScenarioManager.GetSinsDataByID(FreezingEmblem.ID);

            CovenantData cov1 = ProviderManager.SaveManager.GetAllGameData().GetAscensionCovenantForLevel(1);
            CovenantData cov3 = ProviderManager.SaveManager.GetAllGameData().GetAscensionCovenantForLevel(3);

            var baseScenario = CustomScenarioManager.GetScenarioDataByID(VanillaScenarioIDs.SeraphtheChaste);

            // Make a scenario.
            ScenarioDataBuilder scenario = new ScenarioDataBuilder
            {
                ScenarioID = ID,
                BattleDescriptionKey = battleDescriptionKey,
                // Distance from the first battle, 7 fights away so distance 7.
                Distance = 7,
                SpawnPattern = baseScenario.GetSpawnPattern(),
                SpawnPatternBuilder = new SpawnPatternDataBuilder
                {
                    SpawnGroupWaves =
                    {
                        new SpawnGroupPoolDataBuilder
                        {
                            // Can have more than one group here and its randomly chosen.
                            PossibleGroups =
                            {
                                 new SpawnGroupDataBuilder
                                 {
                                     Characters =
                                     {
                                         // These are defined in reverse order. Backliners first.
                                         new SpawnCharacterDataBuilder
                                         {
                                             CharacterData = lightwing,
                                         },
                                         new SpawnCharacterDataBuilder
                                         {
                                             CharacterData = lightwing,
                                             RequiredCovenant = cov3,
                                         },
                                         new SpawnCharacterDataBuilder
                                         {
                                             CharacterData = darkwings,
                                         },
                                     }
                                 }
                            }
                        },
                        new SpawnGroupPoolDataBuilder
                        {
                            PossibleGroups =
                            {
                                new SpawnGroupDataBuilder
                                {
                                    Characters =
                                    {
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = lightwing,
                                        },
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = lightwing,
                                        },
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = pyrewing,
                                        }

                                    }
                                }
                            }
                        },
                        new SpawnGroupPoolDataBuilder
                        {
                            PossibleGroups =
                            {
                                new SpawnGroupDataBuilder
                                {
                                    Characters =
                                    {
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = lightwing,
                                        },
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = gildedwing,
                                        },
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = gildedwing,
                                            RequiredCovenant = cov3,
                                        }
                                    }
                                }
                            }
                        },
                        new SpawnGroupPoolDataBuilder
                        {
                            PossibleGroups =
                            {
                                new SpawnGroupDataBuilder
                                {
                                    Characters =
                                    {
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = gildedwing,
                                        },
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = pyrewing,
                                        },
                                    }
                                }
                            }
                        },
                        new SpawnGroupPoolDataBuilder
                        {
                            PossibleGroups =
                            {
                                new SpawnGroupDataBuilder
                                {
                                    Characters =
                                    {
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = lightwing,
                                        },
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = gildedwing,
                                        },
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = gildedwing,
                                            RequiredCovenant = cov1,
                                        },
                                    }
                                }
                            }
                        },
                        new SpawnGroupPoolDataBuilder
                        {
                            PossibleGroups =
                            {
                                new SpawnGroupDataBuilder
                                {
                                    Characters =
                                    {
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = lightwing,
                                        },
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = gildedwing,
                                        },
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = pyrewing,
                                            RequiredCovenant = cov3,
                                        },
                                    }
                                }
                            }
                        },
                        new SpawnGroupPoolDataBuilder
                        {
                            PossibleGroups =
                            {
                                new SpawnGroupDataBuilder
                                {
                                    Characters =
                                    {
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = lightwing,
                                        },
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = gildedwing,
                                        },
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = steelwing,
                                        },
                                    }
                                }
                            }
                        },
                        new SpawnGroupPoolDataBuilder
                        {
                            PossibleGroups =
                            {
                                new SpawnGroupDataBuilder
                                {
                                    Characters =
                                    {
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = steelwing,
                                        },
                                        new SpawnCharacterDataBuilder
                                        {
                                            CharacterData = steelwing,
                                        },
                                    }
                                }
                            }
                        },
                    },
                    BossType = SpawnPatternData.BossType.OuterTrainBoss,
                    // Here's where the flying boss data is used.
                    BossCharacter = frostbiteSeraph,
                    HardBossCharacter = frostbiteSeraphPact,

                },
                BossVariants =
                {
                    // Important if non flying boss chooses a boss randomly.
                    // But still must be filled out with something otherwise crash.
                    frostbiteSeraph,
                },
                // Controls the map node in the map screen.
                CompletedSpritePath = "Assets/Icons/BattleNodes_Seraph_Completed.png",
                InactiveSpritePath = "Assets/Icons/BattleNodes_Seraph_Frost_Inactive.png",
                ActiveSpritePath = "Assets/Icons/BattleNodes_Seraph_Frost_Active.png",
                HighlightSpritePath = "Assets/Icons/BattleNodes_Seraph_Highlight.png",
                EnemyBlessingData =
                {
                    // Add your sins data here.
                    freezingEmblem,
                },
                BattleTrackNameData = "BattleMusic_Seraph",
                Difficulty = ScenarioDifficulty.Boss,
                DisplayedEnemies =
                {
                },
                BackgroundData = baseScenario.GetBackgroundData(),
                BossSpawnSFXCue = "Seraph_Intro",
                BossIcon = "Assets/Icons/SeraphTheTraitor_Freeze_Icon.png",
                BossPortrait = "Assets/Icons/SeraphTheTraitor_Freeze_Portrait.png",
            };

            var scenarioData = scenario.Build();
            CustomScenarioManager.RegisterCustomScenario(scenarioData, scenario.Distance);
        }
    }
}
