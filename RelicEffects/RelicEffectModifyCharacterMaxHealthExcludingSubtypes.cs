using ShinyShoe.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MoreBosses.RelicEffects
{
    public class RelicEffectModifyCharacterMaxHealthExcludingSubtypes : RelicEffectBase, IStartOfPlayerTurnBeforeDrawRelicEffect, ITurnTimingRelicEffect, IRelicEffect, ICharacterStatAdjustmentRelicEffect
    {
        private Team.Type sourceTeam;

        private SubtypeData characterSubtype;

        private int healAmount;

        private CharacterTriggerData.Trigger trigger;

        private SubtypeData[] excludeCharacterSubtypes;

        private SaveManager saveManager;

        public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
        {
            base.Initialize(relicState, relicData, relicEffectData);
            sourceTeam = relicEffectData.GetParamSourceTeam();
            characterSubtype = relicEffectData.GetParamCharacterSubtype();
            excludeCharacterSubtypes = relicEffectData.GetParamExcludeCharacterSubtypes();
            healAmount = relicEffectData.GetParamInt();
            trigger = relicEffectData.GetParamTrigger();
            if (trigger != CharacterTriggerData.Trigger.OnSpawn && trigger != CharacterTriggerData.Trigger.OnSpawnNotFromCard && trigger != CharacterTriggerData.Trigger.OnUnscaledSpawn)
            {
                Log.Warning(LogGroups.Gameplay, $"Relic effect {GetType()} configured with a trigger event ({trigger}) that is not supported!");
            }
        }

        public bool TestEffect(RelicEffectParams relicEffectParams)
        {
            return false;
        }

        public IEnumerator ApplyEffect(RelicEffectParams relicEffectParams)
        {
            yield break;
        }

        public override IEnumerator OnCharacterAdded(CharacterState character, CardState fromCard, RelicManager relicManager, SaveManager saveManager, PlayerManager playerManager, RoomManager roomManager, CombatManager combatManager, CardManager cardManager)
        {
            this.saveManager = saveManager;
            bool flag = fromCard != null;
            bool flag2 = character.HasStatusEffect("cardless");
            bool num = trigger == CharacterTriggerData.Trigger.OnSpawn && (flag || sourceTeam != Team.Type.Monsters) && !flag2;
            bool flag3 = trigger == CharacterTriggerData.Trigger.OnSpawnNotFromCard && (!flag || flag2);
            if ((num || flag3) && character.GetTeamType() == sourceTeam)
            {
                foreach (SubtypeData subtypeData in excludeCharacterSubtypes)
                {
                    if (character.GetHasSubtype(subtypeData))
                    {
                        yield break;
                    }
                }
                if (character != null && character.GetHP() > 0 && character.GetCharacterManager().DoesCharacterPassSubtypeCheck(character, characterSubtype))
                {
                    yield return TimingYieldIfNonCovenant(TimingContext.PreFire, saveManager);
                    fromCard?.GetTemporaryCardStateModifiers().IncrementAdditionalHP(healAmount);
                    yield return character.BuffMaxHP(healAmount, triggerOnHeal: false, _srcRelicState);
                    NotifyRelicTriggered(relicManager, character);
                    yield return TimingYieldIfNonCovenant(TimingContext.PostFire, saveManager);
                }
            }
        }

        public CharacterStatAdjustment GetStatAdjustment(CharacterData characterData, Team.Type team, RelicManager relicManager)
        {
            if (team != sourceTeam)
            {
                return null;
            }
            if (CardManager.DoesCharacterPassSubtypeCheck(characterData, characterSubtype, team, relicManager))
            {
                foreach (SubtypeData item in excludeCharacterSubtypes)
                {
                    if (characterData.GetSubtypes().Contains(item))
                    {
                        return null;
                    }
                }
                return new CharacterStatAdjustment
                {
                    healthAdjustment = healAmount
                };
            }
            return null;
        }

        public override string GetActivatedDescription()
        {
            string activatedKey = CardEffectAdjustMaxHealth.ActivatedKey;
            if (!activatedKey.HasTranslation())
            {
                return null;
            }
            return string.Format(activatedKey.Localize(), healAmount);
        }
    }

}
