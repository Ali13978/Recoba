using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Game_Designer_Online.Scripts.Photon_Setup_Scripts
{
    /// <summary>
    /// A script to handle all multi-player settings
    /// </summary>
    public class MultiplayerSettings : MonoBehaviourPunCallbacks, IInRoomCallbacks
    {
        //Singleton
        public static MultiplayerSettings Instance;

        #region Multiplayer control variables

        [Header("Multiplayer setting variables")]
        public bool delayStart;

        public int maxPlayers;

        public int menuScene;
        public int multiPlayerScene;

        #endregion

        #region Unity Functions, PUNROOMCALLBACKS

        private void Awake()
        {
            //Singleton setting
            if (MultiplayerSettings.Instance == null)
            {
                MultiplayerSettings.Instance = this;
            }
            else
            {
                //Destroying previous singleton
                if (MultiplayerSettings.Instance != this)
                {
                    Destroy(this.gameObject);
                }

                Instance = this;
            }

            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            delayStart = true;
            maxPlayers = 2;
            menuScene = 1;
            multiPlayerScene = 2;
        }

        #endregion
    }
}