using MoreBosses.Sins;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;

namespace MoreBosses.Trials
{
    public class RegenerativeBlessingForGoldT1
    {
        public static readonly string ID = "RegenerativeBlessingForGoldT1";
        public static readonly RegistrationPriority Priority = RegistrationPriority.LOW;

        public static void AutoRegister()
        {
            var sin = CustomScenarioManager.GetSinsDataByID(RegenerativeBlessingI.ID);
            var existingTrial = CustomScenarioManager.GetTrialDataByID(VanillaTrialIDs.T1GoldForInvasion);
            var trial = new TrialDataBuilder
            {
                TrialID = ID,
                Sin = sin,
                Reward = existingTrial.Reward,
            }.BuildAndRegister();

            TrialHelper.AddTrialToLevels(1, trial);
        }
    }
}
