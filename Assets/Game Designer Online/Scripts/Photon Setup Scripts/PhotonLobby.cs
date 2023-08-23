using System.Collections;
using Game_Designer_Online.Scripts.Constants;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game_Designer_Online.Scripts.Photon_Setup_Scripts
{
    /// <summary>
    /// Photon Lobby
    /// </summary>
    public class PhotonLobby : MonoBehaviourPunCallbacks
    {
        public static PhotonLobby Instance; //Singleton

        #region Variables

        [Header("Lobby Buttons and Info Text")]
        //Connection wait text 
        [SerializeField]
        private GameObject connectionWaitText;
        
        //Splash Animation
        [SerializeField] private GameObject splashAnimation;

        //Login GameObject
        [SerializeField] private GameObject loginGameObject;
        
        //Login information container
        [SerializeField] private GameObject loginInformationContainer;

        //Login button reference
        [SerializeField] private GameObject loginButtonReference;

        //Profile picture reference
        [SerializeField] private GameObject profilePictureReference;

        //Lobby Type canvas reference
        [SerializeField] private GameObject lobbyTypeCanvasReference;

        //Animated logo reference
        [SerializeField] private GameObject animatedLogoReference;

        #endregion

        #region Unity Functions, Photon Callbacks

        private void Awake()
        {
            //Creating singleton
            Instance = this;
        }

        private void Start()
        {
            //connectionWaitText.SetActive(true);
            loginInformationContainer.SetActive(false);
            loginButtonReference.SetActive(false);
            profilePictureReference.SetActive(false);
        }


        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Failed to join a room! Trying to create one!");
            //Creating random room
            CreateRoom();
        }

        #endregion

        #region Photon Connection Functions

        public void RequestConnectionToPhoton()
        {
            //We will return if we are already connected
            if (PhotonNetwork.IsConnected) return;
            
            //Connecting using settings
            PhotonNetwork.ConnectUsingSettings();
            print("Trying to connect to servers");
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            Debug.Log("Connection Successful");
            PhotonNetwork.AutomaticallySyncScene = true;
            StartCoroutine("Routine_ConnectionSucceeded");
        }

        #endregion

        #region Buttons for Photon Server Handling, Room Creation,

        /// <summary>
        /// Creates room for the player if none exists
        /// </summary>
        void CreateRoom()
        {
            int randomRoomName = Random.Range(0, 10000);
            RoomOptions roomOps = new RoomOptions()
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = (byte) MultiplayerSettings.Instance.maxPlayers,
                EmptyRoomTtl = 100,
            };

            //Creating room
            PhotonNetwork.CreateRoom(randomRoomName.ToString(), roomOps);
            Debug.Log("Room created");
        }

        /// <summary>
        /// A function that will create a private room
        /// </summary>
        private void CreatePrivateRoom()
        {
            string roomCode = PlayerPrefs.GetString(GameConstants.PrivateRoomCode);
            RoomOptions roomOps = new RoomOptions()
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = (byte) MultiplayerSettings.Instance.maxPlayers,
                EmptyRoomTtl = 100,
            };

            //Creating private room
            PhotonNetwork.CreateRoom(roomCode, roomOps);
            print("Private room created");
        }

        /// <summary>
        /// A function that will search for a friends room
        /// </summary>
        private void SearchForFriendsRoom()
        {
            var friendCode = PlayerPrefs.GetString(GameConstants.FriendRoomCode);
            PhotonNetwork.JoinRoom(friendCode);
            print("Trying to join friend name");
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            Debug.Log("Room creation failed! Attempting to recreate.");
            CreateRoom();
        }

        /// <summary>
        /// Begin Search Button to join random room
        /// </summary>
        public void OnBeginSearchClicked()
        {
            //When the private friend room key is not empty, we will run this code
            if (string.IsNullOrEmpty(PlayerPrefs.GetString(GameConstants.FriendRoomCode)) == false)
            {
                SearchForFriendsRoom();
                print("Trying to join a friend");

                //Turning off login information canvas
                loginInformationContainer.SetActive(false);
                loginButtonReference.SetActive(false);

                /*connectionWaitText.GetComponent<Text>().text
                    = $"Joining Friend room with code {PlayerPrefs.GetString(GameConstants.FriendRoomCode)}";*/
                connectionWaitText.SetActive(false);

                return;
            }

            //When the Private room key is not empty, we will run this code
            if (string.IsNullOrEmpty(PlayerPrefs.GetString(GameConstants.PrivateRoomCode)) == false)
            {
                CreatePrivateRoom();
                Debug.Log("Trying to create Private Room!");

                //Turning off login information canvas
                loginInformationContainer.SetActive(false);
                loginButtonReference.SetActive(false);

                /*connectionWaitText.GetComponent<Text>().text
                 = $"Creating private room with code {PlayerPrefs.GetString(GameConstants.PrivateRoomCode)}";*/
                connectionWaitText.SetActive(false);

                return;
            }

            //Joining random room
            PhotonNetwork.JoinRandomRoom();
            Debug.Log("Trying to join random room!");

            //Turning off login information canvas
            loginInformationContainer.SetActive(false);
            loginButtonReference.SetActive(false);

            //connectionWaitText.GetComponent<Text>().text = "Searching for games........";
            connectionWaitText.SetActive(false);
        }

        /// <summary>
        /// A routine to run when the player successfully connects to the a server
        /// </summary>
        /// <returns></returns>
        private IEnumerator Routine_ConnectionSucceeded()
        {
            yield return new WaitForSeconds(1.5f);
            connectionWaitText.SetActive(false);
            animatedLogoReference.SetActive(false);
            
            //Disabling splash animation
            splashAnimation.SetActive(false);
            loginGameObject.SetActive(true);

            //Activating login canvas
            loginInformationContainer.SetActive(true);
            loginButtonReference.SetActive(true);
            profilePictureReference.SetActive(true);
        }

        #endregion
    }
}