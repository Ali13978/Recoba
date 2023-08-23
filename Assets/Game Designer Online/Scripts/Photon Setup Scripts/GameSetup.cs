using System.IO;
using Photon.Pun;
using UnityEngine;

namespace Game_Designer_Online.Scripts.Photon_Setup_Scripts
{
    /// <summary>
    /// GameSetup for multi-player functions
    /// </summary>
    public class GameSetup : MonoBehaviour
    {
        public static GameSetup gS;

        [Header("Spawn Points")] 
        public Transform[] spawnPoints;

        private void OnEnable()
        {
            //Singleton setting
            if (GameSetup.gS == null)
            {
                GameSetup.gS = this;
            }
            
            //Telling master client to spawn masterClientGameController
            if (PhotonNetwork.IsMasterClient == true)
            {
                PhotonNetwork.Instantiate(
                    Path.Combine("PhotonPrefabs", "MasterClientGameController"),
                    transform.position,
                    Quaternion.identity
                );
                
                print("Master client spawned Game Controller");
            }
        }
    }
}