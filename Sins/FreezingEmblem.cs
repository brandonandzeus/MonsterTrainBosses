using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;

namespace MoreBosses.Sins
{
    public class FreezingEmblem
    {
        public static RegistrationPriority Priority = RegistrationPriority.MEDIUM;
        public static string ID = "FreezingEmblem";

        public static void AutoRegister()
        {
            // In battle effect, All units enter with Frostbite 2.
            // This is like any artifact.
            new SinsDataBuilder
            {
                SinsID = ID,
                IconPath = "Assets/Icons/SIN_Boss_Frost.png",
                EffectBuilders =
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(RelicEffectAddStatusEffectOnSpawn),
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamStatusEffects =
                        {
                            new StatusEffectStackData {count = 2, statusId = VanillaStatusEffectIDs.Frostbite},
                        },
                    }
                },
            }.BuildAndRegister();

        }
    }
}
