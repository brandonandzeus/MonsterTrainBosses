using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MoreBosses.RelicEffects
{
    public class RelicEffectAddStatusImmunity : RelicEffectBase
    {
        private StatusEffectStackData[] statusEffects;

        private Team.Type sourceTeam;

        private SubtypeData characterSubtype;

        public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
        {
            base.Initialize(relicState, relicData, relicEffectData);
            statusEffects = relicEffectData.GetParamStatusEffects();
            sourceTeam = relicEffectData.GetParamSourceTeam();
            characterSubtype = relicEffectData.GetParamCharacterSubtype();
        }

        public override IEnumerator OnCharacterAdded(CharacterState character, CardState fromCard, RelicManager relicManager, SaveManager saveManager, PlayerManager playerManager, RoomManager roomManager, CombatManager combatManager, CardManager cardManager)
        {
            if (character.GetTeamType() != sourceTeam || !character.GetCharacterManager().DoesCharacterPassSubtypeCheck(character, characterSubtype))
            {
                yield break;
            }
            foreach (var statusEffectStackData in statusEffects)
            {
                character.AddImmunity(statusEffectStackData.statusId);
            }
        }
    }
}
