using System;
using System.IO;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game_Designer_Online.Scripts.Photon_Setup_Scripts
{
    /// <summary>
    /// For the photon player
    /// </summary>
    public class PhotonPlayer : MonoBehaviourPunCallbacks
    {
        //PhotonView
        private PhotonView _pV;
        //Avatar
        public GameObject myAvatar;
        
        //Number in room
        public int myNumberInRoom;

        /// <summary>
        /// Spawns the player avatar 
        /// </summary>
        public void SpawnAvatarAndSetRoomNumber()
        {
            //Random Spawn
            int spawnPicker = Random.Range(0,
                GameSetup.gS.spawnPoints.Length);

            //Spawning local player
            if (_pV.IsMine)
            {
                myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
                    GameSetup.gS.spawnPoints[spawnPicker].position,
                    GameSetup.gS.spawnPoints[spawnPicker].rotation,
                    0);
            }
        }
        
        #region Unity Functions

        private void Awake()
        {
            //PhotonView
            _pV = GetComponent<PhotonView>();
        }

        #endregion
    }
}