using MoreBosses.Sins;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;

namespace MoreBosses.Trials
{
    public class RegenerativeBlessingForGoldT3
    {
        public static readonly string ID = "RegenerativeBlessingForGoldT3";
        public static readonly RegistrationPriority Priority = RegistrationPriority.LOW;

        public static void AutoRegister()
        {
            var sin = CustomScenarioManager.GetSinsDataByID(RegenerativeBlessingIII.ID);
            var existingTrial = CustomScenarioManager.GetTrialDataByID(VanillaTrialIDs.T3GoldForEnemyArmor);
            var trial = new TrialDataBuilder
            {
                TrialID = ID,
                Sin = sin,
                Reward = existingTrial.Reward,
            }.BuildAndRegister();

            TrialHelper.AddTrialToLevels(2, trial);
        }
    }
}
