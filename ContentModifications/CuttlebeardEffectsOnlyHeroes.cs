using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.ConstantsV2;
using Trainworks.Managers;

namespace MoreBosses.ContentModifications
{
    /// <summary>
    /// Unfortunately our friend Cuttlebeard is indiscrimate when it comes to the effects.
    /// Seraph the Coldhearted is impossible when Cuttlebeard is in play.
    /// </summary>
    public class CuttlebeardEffectsOnlyHeroes
    {
        public static readonly RegistrationPriority Priority = RegistrationPriority.LOW;

        public static void AutoRegister()
        {
            var cuttlebeard = CustomCollectableRelicManager.GetRelicDataByID(VanillaCollectableRelicIDs.Cuttlebeard);
            AccessTools.Field(typeof(CollectableRelicData), "descriptionKey").SetValue(cuttlebeard, "Cuttlebeard_CollectableRelicData_DescriptionKey");
            var effects = cuttlebeard.GetEffects();
            foreach (var effect in effects)
            {
                AccessTools.Field(typeof(RelicEffectData), "paramSourceTeam").SetValue(effect, Team.Type.Heroes);
            }
        }
    }
}
