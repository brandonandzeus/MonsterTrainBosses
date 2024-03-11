using MoreBosses.Sins;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;

namespace MoreBosses.Trials
{
    public class PurifyingAmuletForArtifactT2
    {
        public static readonly string ID = "PurifyingAmuletForArtifactT2";
        public static readonly RegistrationPriority Priority = RegistrationPriority.LOW;

        public static void AutoRegister()
        {
            var purifyingAmulet = CustomScenarioManager.GetSinsDataByID(PurifyingAmulet.ID);
            var t2Trial = CustomScenarioManager.GetTrialDataByID(VanillaTrialIDs.T2ArtifactForSpikes);
            var trial = new TrialDataBuilder
            {
                TrialID = ID,
                Sin = purifyingAmulet,
                Reward = t2Trial.Reward,
            }.BuildAndRegister();

            TrialHelper.AddTrialToLevels(1, trial);
        }
    }
}
