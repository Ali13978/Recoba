using System;
using Game_Designer_Online.Scripts.Constants;
using Game_Designer_Online.Scripts.Photon_Setup_Scripts;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Designer_Online.Scripts.Category_Scripts
{
    /// <summary>
    /// Contains all the functions required for the correct
    /// functions of the category canvas
    /// </summary>
    public class CategoryCanvasScript : MonoBehaviour
    {
        #region Category Functions

        [Header("Category Canvas Variables")]
        //Photon view
        [SerializeField]
        private PhotonView photonView;

        //User name field
        [SerializeField] private Text userNameField;

        //Wins and loses field
        [SerializeField] private Text winField, lostField;

        //References to all screens
        [SerializeField] private MensLanguageScript mensLanguageScriptRef;

        /// <summary>
        /// When the mens language button is clicked
        /// </summary>
        public void OnMensLanguageClicked()
        {
            //We will return if we are not the master client as only the master
            //client can change the categories
            if (PhotonNetwork.IsMasterClient == false)
            {
                print("Master client did not select category");
                return;
            }

            print("Master Client selected Mens Language Category");
            MasterClientGameController.Instance.masterPhotonView.RPC(
                "SelectMensLanguageCategory",
                RpcTarget.All
            );
        }

        /// <summary>
        /// When the when we are out is clicked
        /// </summary>
        public void OnWhenWeAreOutClicked()
        {
            //We will return if we are not the master client as only the master
            //client can change the categories
            if (PhotonNetwork.IsMasterClient == false)
            {
                print("Master client did not select category");
                return;
            }

            print("Master Client selected When We Are Out Category");
            MasterClientGameController.Instance.masterPhotonView.RPC(
                "SelectWhenYouAreOutCategory",
                RpcTarget.All
            );
        }

        /// <summary>
        /// When the love and sex button is clicked
        /// </summary>
        public void OnLoveAndSexClicked()
        {
            //We will return if we are not the master client as only the master
            //client can change the categories
            if (PhotonNetwork.IsMasterClient == false)
            {
                print("Master client did not select category");
                return;
            }
            
            print("Master Client selected Love and Sex Category");
            MasterClientGameController.Instance.masterPhotonView.RPC(
                "SelectLoveAndSexCategory",
                RpcTarget.All
            );
        }

        /// <summary>
        /// When the money and finance button is clicked
        /// </summary>
        public void OnMoneyAndFinanceClicked()
        {
            //We will return if we are not the master client as only the master
            //client can change the categories
            if (PhotonNetwork.IsMasterClient == false)
            {
                print("Master client did not select category");
                return;
            }
            
            print("Master Client selected Money and Finance Category");
            MasterClientGameController.Instance.masterPhotonView.RPC(
                "SelectMoneyAndFinanceCategory",
                RpcTarget.All
            );
        }

        #endregion

        #region Unity Functions

        private void Start()
        {
            //Setting user name
            userNameField.text = PlayerPrefs.GetString(GameConstants.PlayerUserNameKey);
            
            //Setting wins and losses
            winField.text = PlayerPrefs.GetInt(GameConstants.PlayerNumberOfWins).ToString();
            lostField.text = PlayerPrefs.GetInt(GameConstants.PlayerNumberOfLosses).ToString();

            //Returning if we are not the local player
            if (photonView.IsMine == false)
            {
                return;
            }
        }

        #endregion
    }
}