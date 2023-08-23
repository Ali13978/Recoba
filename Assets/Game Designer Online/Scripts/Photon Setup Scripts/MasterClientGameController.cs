using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Designer_Online.Scripts.Photon_Setup_Scripts
{
    /// <summary>
    /// Contains all the functions required for the master client to control the game
    /// </summary>
    public class MasterClientGameController : MonoBehaviour
    {
        #region Master Client Functions and Variables

        public static MasterClientGameController Instance;

        [Header("Variables for Master Client Control GameObject")]
        //Photon view of master client
        public PhotonView masterPhotonView;

        //Screen references
        public GameObject categorySelectionGameObject,
            mensLanguageGameObject,
            whenYouAreOutGameObject,
            loveAndSexGameObject,
            moneyAndFinanceGameObject;
        
        //Category button references
        public GameObject mensLanguageCategoryButton,
            moneyAndFinanceCategoryButton,
            loveAndSexCategoryButton,
            whenYouAreOutCategoryButton;


        //Holds the current player objects in the scene
        public List<GameObject> playerList = new List<GameObject>();
        
        //General info canvas object
        [SerializeField]
        private GameObject generalCanvasObject;
        
        //General info canvas text
        [SerializeField]
        private Text generalInfoCanvasText;

        #endregion

        #region RPC for Game Control

        /// <summary>
        /// Selects the mens language category for both clients
        /// </summary>
        [PunRPC]
        public void SelectMensLanguageCategory()
        {
            categorySelectionGameObject.SetActive(false);
            mensLanguageGameObject.SetActive(true);
        }

        /// <summary>
        /// Selects the when you are out category for both clients
        /// </summary>
        [PunRPC]
        public void SelectWhenYouAreOutCategory()
        {
            whenYouAreOutGameObject.SetActive(true);
            categorySelectionGameObject.SetActive(false);
        }

        /// <summary>
        /// Selects the love and sex category for both clients
        /// </summary>
        [PunRPC]
        public void SelectLoveAndSexCategory()
        {
            loveAndSexGameObject.SetActive(true);
            categorySelectionGameObject.SetActive(false);
        }

        /// <summary>
        /// Selects the money and finance category for both clients
        /// </summary>
        [PunRPC]
        public void SelectMoneyAndFinanceCategory()
        {
            moneyAndFinanceGameObject.SetActive(true);
            categorySelectionGameObject.SetActive(false);
        }

        /// <summary>
        /// A function that will populate the player list on both clients
        /// </summary>
        [PunRPC]
        private void PopulatePlayerList()
        {
            //We are going to add the players in the list
            StartCoroutine(Routine_PopulatePlayerList());
        }

        /// <summary>
        /// This will be called when the local MasterClient has found all the players. This will actually enable all
        /// the different category cards for both clients
        /// </summary>
        [PunRPC]
        private void RPC_EnableAllCategoryButtonsWhenAllPlayersAreFound()
        {
            //Turning off starting canvas
            GameObject.Find("StartingCanvas").SetActive(false);
            
            //Turning on category selection canvas
            categorySelectionGameObject.SetActive(true);
            
            mensLanguageCategoryButton.SetActive(true);
            moneyAndFinanceCategoryButton.SetActive(true);
            loveAndSexCategoryButton.SetActive(true);
            whenYouAreOutCategoryButton.SetActive(true);
        }

        /// <summary>
        /// An RPC which will add a player to the list. This is called by a routine below, and will simply process
        /// the Photon View Id to find the player on both clients
        /// </summary>
        [PunRPC]
        private void RPC_AddPlayerWithViewIDToList(int playerViewId)
        {
            var playerOnNetwork = PhotonView.Find(playerViewId).gameObject;
            playerList.Add(playerOnNetwork);
            print("Player with name " + playerOnNetwork.name + " was added to the list");
        }

        /// <summary>
        /// A routine that will run a for each loop until we have found two players
        /// in the game and added them to the list
        /// </summary>
        /// <returns></returns>
        private IEnumerator Routine_PopulatePlayerList()
        {
            //We are going to add the players in the list
            var playersInScene = GameObject.FindGameObjectsWithTag("Player");

            //While loop will run until we have found both the players
            while (playersInScene.Length < 2)
            {
                playersInScene = GameObject.FindGameObjectsWithTag("Player");

                yield return new WaitForEndOfFrame();
            }

            //For each loop to go through all the players
            foreach (var player in playersInScene)
            {
                //Getting the photon view and calling the RPC to add the player to the list
                PhotonView playerPhotonView = player.GetComponent<PhotonView>();
                if (playerPhotonView == true)
                {
                    masterPhotonView.RPC(
                        "RPC_AddPlayerWithViewIDToList",
                        RpcTarget.All,
                        playerPhotonView.ViewID
                    );
                }
            }

            print("All players found on master client");
            
            //Enabling the category screens for both clients
            masterPhotonView.RPC(
                "RPC_EnableAllCategoryButtonsWhenAllPlayersAreFound",
                RpcTarget.All
            );

            yield break;
        }

        #endregion

        #region Unity Functions

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                masterPhotonView.RPC(
                    "PopulatePlayerList",
                    RpcTarget.MasterClient
                );
            }
        }

        #endregion
    }
}