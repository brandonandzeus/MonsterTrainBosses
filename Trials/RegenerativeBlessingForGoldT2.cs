using MoreBosses.Sins;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;

namespace MoreBosses.Trials
{
    public class RegenerativeBlessingForGoldT2
    {
        public static readonly string ID = "RegenerativeBlessingForGoldT2";
        public static readonly RegistrationPriority Priority = RegistrationPriority.LOW;

        public static void AutoRegister()
        {
            var sin = CustomScenarioManager.GetSinsDataByID(RegenerativeBlessingII.ID);
            var existingTrial = CustomScenarioManager.GetTrialDataByID(VanillaTrialIDs.T2GoldForEnemyArmor);
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
