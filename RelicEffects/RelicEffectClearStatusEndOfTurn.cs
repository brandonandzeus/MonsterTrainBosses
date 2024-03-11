using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoreBosses.RelicEffects
{
    public sealed class RelicEffectClearStatusEndOfTurn : RelicEffectBase, IEndOfTurnRelicEffect, IRelicEffect
    {
        private StatusEffectData.DisplayCategory displayCategory;
        private bool halveStatuses;
        private Team.Type sourceTeam;
        private VfxAtLoc appliedVfx;
        private List<CharacterState> targets = new List<CharacterState>();
        private List<CharacterState.StatusEffectStack> statusEffectStacks = new List<CharacterState.StatusEffectStack>();
        private SubtypeData[] excludeCharacterSubtypes;

        public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
        {
            base.Initialize(relicState, relicData, relicEffectData);
            displayCategory = (StatusEffectData.DisplayCategory) relicEffectData.GetParamInt();
            halveStatuses = relicEffectData.GetParamBool();
            sourceTeam = relicEffectData.GetParamSourceTeam();
            appliedVfx = relicEffectData.GetAppliedVfx();
            excludeCharacterSubtypes = relicEffectData.GetParamExcludeCharacterSubtypes();
        }

        public bool TestEffect(EndOfTurnRelicEffectParams relicEffectParams)
        {
            return true;
        }

        public IEnumerator ApplyEffect(EndOfTurnRelicEffectParams relicEffectParams)
        {
            targets.Clear();
            relicEffectParams.roomState.AddMovableCharactersToList(targets, sourceTeam);
            foreach (var characterState in targets)
            {
                bool flag = false;
                foreach (SubtypeData subtypeData in excludeCharacterSubtypes)
                {
                    if (characterState.GetHasSubtype(subtypeData))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    continue;
                }

                characterState.GetStatusEffects(out statusEffectStacks);
                for (int j = 0; j < statusEffectStacks.Count; j++)
                {
                    if (statusEffectStacks[j].State.GetDisplayCategory() == displayCategory)
                    {
                        int num = statusEffectStacks[j].Count;
                        if (halveStatuses)
                        {
                            num = Mathf.CeilToInt((float)num / 2f);
                        }
                        characterState.RemoveStatusEffect(statusEffectStacks[j].State.GetStatusId(), removeAtEndOfTurn: false, num, showNotification: true, SourceRelicState);
                        characterState.GetCharacterUI().ShowEffectVFX(characterState, appliedVfx);
                    }
                }
                statusEffectStacks.Clear();
            }
            yield break;
        }
    }
}
