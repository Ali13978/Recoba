using System;
using UnityEngine;

namespace Game_Designer_Online.Scripts.Constants
{
    /// <summary>
    /// A script that will hold the constants that the game requires
    /// </summary>
    public class GameConstants : MonoBehaviour
    {
        public const string PlayerNameKey = "PlayerNameKey";
        public const string PlayerUserNameKey = "PlayerUserNameKey";
        public const string PlayerGenderKey = "PlayerGenderKey";

        public const string PlayerNumberOfWins = "NumberOfWins";
        public const string PlayerNumberOfLosses = "NumberOfLosses";

        public const string PrivateRoomCode = "PrivateRoomCode";
        public const string FriendRoomCode = "FriendsCode";

        /// <summary>
        /// Sets the keys at the very start
        /// </summary>
        public static void SetKeysAtStart()
        {
            //Setting room code key
            PlayerPrefs.SetString(PrivateRoomCode, "");
            PlayerPrefs.SetString(PrivateRoomCode, "");
            PlayerPrefs.SetString(FriendRoomCode, "");
            
            print("All keys were reset");
        }

        private void Start()
        {
            //SetKeysAtStart();
        }
    }
}