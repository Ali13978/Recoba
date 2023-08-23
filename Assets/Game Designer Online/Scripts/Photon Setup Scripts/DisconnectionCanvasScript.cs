using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Video;

namespace Game_Designer_Online.Scripts.Photon_Setup_Scripts
{
    /// <summary>
    /// Functions for the disconnection canvas
    /// </summary>
    public class DisconnectionCanvasScript : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
        
        private IEnumerator Routine_DisconnectFromServers()
        {
            videoPlayer.Play();
            
            yield return new WaitForSeconds(7.5f);
            
            while (PhotonNetwork.IsConnected)
            {
                print("Trying to disconnect from Photon");
                PhotonNetwork.Disconnect();
                yield return new WaitForEndOfFrame();
            }
        }
        
        private void OnEnable()
        {
            StartCoroutine(Routine_DisconnectFromServers());
        }
    }
}