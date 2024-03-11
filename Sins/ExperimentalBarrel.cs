using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;

namespace MoreBosses.Sins
{
    public class ExperimentalBarrel
    {
        public static RegistrationPriority Priority = RegistrationPriority.MEDIUM;
        public static string ID = "ExperimentalBarrel";
        public static string TriggerID = "ExperimentalBarrel_Action";

        public static void AutoRegister()
        {
            // In battle effect, Exploding Barrels apply 2 sap to friendly units and 2 rage to enemy units.
            // This is like any artifact.
            new SinsDataBuilder
            {
                SinsID = ID,
                IconPath = "Assets/Icons/SIN_Boss_SlickExplosive.png",
                EffectBuilders =
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(RelicEffectAddTrigger),
                        ParamSourceTeam = Team.Type.Heroes,
                        ParamCharacterSubtype = VanillaSubtypeIDs.ExplodingBarrel,
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
                                        ParamStatusEffects =
                                        {
                                            new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Rage, count = 3 },
                                        }
                                    },
                                    new CardEffectDataBuilder
                                    {
                                        EffectStateType = typeof(CardEffectAddStatusEffect),
                                        TargetMode = TargetMode.Room,
                                        TargetTeamType = Team.Type.Monsters,
                                        ParamStatusEffects =
                                        {
                                            new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Sap, count = 2 },
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
            }.BuildAndRegister();

        }
    }
}
