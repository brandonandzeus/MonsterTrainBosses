using MoreBosses.Sins;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;

namespace MoreBosses.Trials
{
    public class PurifyingAmuletForGoldT3
    {
        public static readonly string ID = "PurifyingAmuletForGoldT3";
        public static readonly RegistrationPriority Priority = RegistrationPriority.LOW;

        public static void AutoRegister()
        {
            var purifyingAmulet = CustomScenarioManager.GetSinsDataByID(PurifyingAmulet.ID);
            var t3Trial = CustomScenarioManager.GetTrialDataByID(VanillaTrialIDs.T3GoldForSpikes);
            var trial = new TrialDataBuilder
            {
                TrialID = ID,
                Sin = purifyingAmulet,
                Reward = t3Trial.Reward,
            }.BuildAndRegister();

            TrialHelper.AddTrialToLevels(2, trial);
        }
    }
}
