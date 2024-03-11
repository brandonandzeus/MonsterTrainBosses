using MoreBosses.RelicEffects;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;

namespace MoreBosses.Sins
{
    public class RegenerativeBlessingII
    {
        public static RegistrationPriority Priority = RegistrationPriority.MEDIUM;
        public static string ID = "RegenerativeBlessingII";

        public static void AutoRegister()
        {
            // In battle effect, All units enter with Regen 10 and 10 Max Health.
            new SinsDataBuilder
            {
                SinsID = ID,
                IconPath = "Assets/Icons/SIN_Easy_Health.png",
                NameKey = "RegenerativeBlessing_SinsData_NameKey",
                DescriptionKey = "RegenerativeBlessing_SinsData_DescriptionKey",
                EffectBuilders =
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(RelicEffectModifyCharacterMaxHealthExcludingSubtypes),
                        ParamSourceTeam = Team.Type.Heroes,
                        ParamInt = 10,
                        ParamTrigger = CharacterTriggerData.Trigger.OnSpawnNotFromCard,
                        ParamCardType = CardType.Invalid,
                        ParamExcludeCharacterSubtypes = new string[]
                        {
                            VanillaSubtypeIDs.TreasureCollector,
                            VanillaSubtypeIDs.Boss
                        }
                    },
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(RelicEffectAddStatusEffectOnSpawn),
                        ParamSourceTeam = Team.Type.Heroes,
                        ParamStatusEffects =
                        {
                            new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Regen, count = 10},
                        },
                        ParamExcludeCharacterSubtypes = new string[]
                        {
                            VanillaSubtypeIDs.TreasureCollector,
                            VanillaSubtypeIDs.Boss
                        }
                    }
                },
            }.BuildAndRegister();

        }
    }
}
