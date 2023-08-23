using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game_Designer_Online.Scripts.Photon_Setup_Scripts
{
    /// <summary>
    /// Contains all the functions required to handle disconnections well
    /// </summary>
    public class DisconnectionHandlerScript : MonoBehaviour
    {
        public static DisconnectionHandlerScript Instance;

        //Reference to Disconnect canvas
        [SerializeField] private GameObject disconnectCanvas;

        //Disconnection text
        [SerializeField] private Text disconnectionCauseText;

        /// <summary>
        /// This function will run when one of the players disconnects and the player is not on
        /// the login screen
        /// </summary>
        public void DisplayDisconnectionCanvasAndReturnToMainMenu()
        {
            print("A player was disconnected and Disconnect Canvas was activated");

            //Spawning the disconnection canvas
            GameObject disconnectionCanvas = Instantiate(
                disconnectCanvas,
                transform.position,
                Quaternion.identity
            );

            //Handling disconnection text
            disconnectionCauseText = disconnectionCanvas.transform.GetChild(1).GetComponent<Text>();
            disconnectionCauseText.text = "A Player Disconnected! Returning to Main Menu...";
        }

        /// <summary>
        /// A function that will return the player to the main menu after a while
        /// </summary>
        /// <returns></returns>
        private IEnumerator Routine_DisconnectRoutineToReturnToMain()
        {
            yield return new WaitForSeconds(5.0f);
            print("Routine to disconnect current player has started");
            PhotonRoom.Instance.StartDisconnectRoutine();

            yield break;
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}