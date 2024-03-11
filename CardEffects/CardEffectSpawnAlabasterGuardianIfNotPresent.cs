using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Trainworks.ConstantsV2;

namespace MoreBosses.CardEffects
{
    public class CardEffectSpawnAlabasterGuardianIfNotPresent : CardEffectBase
    {
        List<CharacterState> characters = new List<CharacterState>();
        public override bool CanApplyInPreviewMode => false;

        public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
        {
            var roomState = cardEffectParams.GetSelectedRoom();
            characters.Clear();
            roomState.AddCharactersToList(characters, Team.Type.Heroes);

            foreach (var character in characters)
            {
                if (character.GetSourceCharacterData().GetID() == VanillaCharacterIDs.AlabasterGuardian)
                {
                    return false;
                }
            }

            return cardEffectParams.GetSelectedRoom().GetFirstEmptyHeroPoint() != null;
        }

        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
        {
            CharacterState outCharacterState = null;
            yield return cardEffectParams.heroManager.SpawnHeroInRoom(cardEffectState.GetParamCharacterData(), cardEffectParams.selectedRoom, outCharacterState);
        }
    }
}
