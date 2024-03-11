using MoreBosses.Sins;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;

namespace MoreBosses.Trials
{
    public class RegenerativeBlessingForUnitDraftT1
    {
        public static readonly string ID = "RegenerativeBlessingForUnitDraftT1";
        public static readonly RegistrationPriority Priority = RegistrationPriority.LOW;

        public static void AutoRegister()
        {
            var sin = CustomScenarioManager.GetSinsDataByID(RegenerativeBlessingI.ID);
            // The reward is actually a CardDraft.
            var existingTrial = CustomScenarioManager.GetTrialDataByID(VanillaTrialIDs.T1GoldForEnemyArmor);
            var trial = new TrialDataBuilder
            {
                TrialID = ID,
                Sin = sin,
                Reward = existingTrial.Reward,
            }.BuildAndRegister();

            TrialHelper.AddTrialToLevels(0, trial);
        }
    }
}
