using MoreBosses.RelicEffects;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;

namespace MoreBosses.Sins
{
    public class PurifyingAmulet
    {
        public static readonly string ID = "PurifyingAmulet";
        public static readonly RegistrationPriority Priority = RegistrationPriority.MEDIUM;

        public static void AutoRegister()
        {
            new SinsDataBuilder
            {
                SinsID = "PurifyingAmulet",
                IconPath = "Assets/Icons/SIN_Easy_Purify.png",
                EffectBuilders =
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(RelicEffectClearStatusEndOfTurn),
                        ParamInt = (int)StatusEffectData.DisplayCategory.Negative,
                        ParamSourceTeam = Team.Type.Heroes,
                        ParamBool = false,
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
