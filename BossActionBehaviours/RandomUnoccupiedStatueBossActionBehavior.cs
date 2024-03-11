using ShinyShoe.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.Constants;

namespace MoreBosses.BossActionBehaviours
{
    /// <summary>
    /// Used by Fel Resurrective.
    /// </summary>
    public class RandomUnoccupiedStatueBossActionBehavior : IBossActionBehavior
    {
        List<CharacterState> characters = new List<CharacterState>();

        public void Setup(BossActionData actionData)
        {
        }

        public bool CanExecute(RoomManager roomManager)
        {
            var list = GetAvailableRooms(roomManager);
            Trainworks.Trainworks.Log("CanExecute: " + (roomManager.GetNumRooms() - 1 > 0 && list.Count > 0));
            return roomManager.GetNumRooms() - 1 > 0 && list.Count > 0;
        }

        public int SelectRoom(RoomManager roomManager, List<int> visitedRoomRecord, int currentRoomIndex)
        {
            List<int> availableRooms = GetAvailableRooms(roomManager);
            if (availableRooms.Count == 0)
            {
                // All alabaster guardians are up so pick a random room. This shouldn't happen though...
                return RandomBossActionBehavior.SelectRandomRoom(roomManager, visitedRoomRecord, currentRoomIndex);
            }
            return RandomBossActionBehavior.SelectRandomRoom(availableRooms, visitedRoomRecord, currentRoomIndex);
        }

        private List<int> GetAvailableRooms(RoomManager roomManager)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < roomManager.GetNumRooms() - 1; i++)
            {
                var roomState = roomManager.GetRoom(i);
                characters.Clear();
                roomState.AddCharactersToList(characters, Team.Type.Heroes);
                bool flag = true;
                foreach (var character in characters)
                {
                    if (character.GetSourceCharacterData().GetID() == VanillaCharacterIDs.AlabasterGuardian)
                    {
                        
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    list.Add(i);
                }
            }

            return list;
        }
    }
}
