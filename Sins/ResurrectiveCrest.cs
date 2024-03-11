using MoreBosses.RelicEffects;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;

namespace MoreBosses.Sins
{
    public class ResurrectiveCrest
    {
        public static RegistrationPriority Priority = RegistrationPriority.MEDIUM;
        public static string ID = "ResurrectiveCrest";
        public static string TriggerID = "ResurrectiveCrest_Enchant";

        public static void AutoRegister()
        {
            var alabasterGuardian = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.AlabasterGuardian);

            // In battle effect, Alabaster Guardians enchant apply Regen 3.
            // This is like any artifact.
            new SinsDataBuilder
            {
                SinsID = ID,
                IconPath = "Assets/Icons/SIN_Boss_StatueResurrection.png",
                EffectBuilders =
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(RelicEffectAddTrigger),
                        ParamSourceTeam = Team.Type.Heroes,
                        ParamCharacterSubtype = VanillaSubtypeIDs.Statue,
                        ParamCardType = CardType.Monster,
                        TriggerBuilders =
                        {
                            new CharacterTriggerDataBuilder
                            {
                                TriggerID = TriggerID,
                                Trigger = CharacterTriggerData.Trigger.OnTurnBegin,
                                EffectBuilders =
                                {
                                    new CardEffectDataBuilder
                                    {
                                        EffectStateType = typeof(CardEffectAddStatusEffect),
                                        TargetMode = TargetMode.Room,
                                        TargetTeamType = Team.Type.Heroes,
                                        TargetIgnoreBosses = true,
                                        ParamStatusEffects =
                                        {
                                            new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Regen, count = 3 },
                                        }
                                    },
                                }
                            }
                        }
                    },
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(RelicEffectAddStatusImmunity),
                        ParamSourceTeam = Team.Type.Heroes,
                        ParamCharacterSubtype = VanillaSubtypeIDs.Statue,
                        ParamCardType = CardType.Monster,
                        ParamStatusEffects =
                        {
                            new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Regen, count = 0},
                        }
                    },
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(RelicEffectSpawnUnitStartOfCombat),
                        ParamSourceTeam = Team.Type.Heroes,
                        ParamInt = 2,
                        ParamCharacters =
                        {
                            alabasterGuardian,
                        }
                    },
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(RelicEffectSpawnUnitStartOfCombat),
                        ParamSourceTeam = Team.Type.Heroes,
                        ParamInt = 1,
                        ParamCharacters =
                        {
                            alabasterGuardian,
                        }
                    },
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(RelicEffectSpawnUnitStartOfCombat),
                        ParamSourceTeam = Team.Type.Heroes,
                        ParamInt = 0,
                        ParamCharacters =
                        {
                            alabasterGuardian,
                        }
                    }
                },
            }.BuildAndRegister();

        }
    }
}
