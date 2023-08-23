using System.Collections;
using System.IO;
using Game_Designer_Online.Scripts.Constants;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game_Designer_Online.Scripts.Photon_Setup_Scripts
{
    /// <summary>
    /// For Photon Rooms of the game
    /// </summary>
    public class PhotonRoom : MonoBehaviourPunCallbacks
    {
        #region Variables

        [Header("Display UI")]
        //Info Text Box
        public Text infoText;
        
        //Connected logo
        public GameObject connectedLogo;
        //Code sent logo
        public GameObject waitingForPlayer2Logo;
        //Waiting for player one logo
        public GameObject waitingForPlayer1Logo;

        //Room variables
        public static PhotonRoom Instance;

        [Header("Room Control")] public int playersInRoom;
        public int myNumberInRoom;

        public int playerInGame;
        public float startingTime;
        private float _atMaxPlayers;
        private float _lessThanMaxPlayers;

        //Player info variables
        private Player[] _photonPlayers;
        private PhotonView _PV;

        public bool isGameLoaded;
        public int currentScene;

        //Variable for Delay Start
        private bool _readyToCount;
        private bool _readyToStart;
        private float _timeToStart;

        #endregion

        #region Unity Function, PUNCALLBACKS

        public override void OnEnable()
        {
            base.OnEnable();
            PhotonNetwork.AddCallbackTarget(this);
            SceneManager.sceneLoaded += OnSceneFinishedLoading;
        }

        /// <summary>
        /// Removes the callback target whenever required
        /// </summary>
        public void RemoveThisCallBackTarget()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        private void Awake()
        {
            //Setting singleton
            if (PhotonRoom.Instance == null)
            {
                PhotonRoom.Instance = this;
            }
            else
            {
                //Destroying previous singleton instance
                if (PhotonRoom.Instance != this)
                {
                    Destroy(PhotonRoom.Instance.gameObject);
                    PhotonRoom.Instance = this;
                }
            }

            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            //Initializing
            _PV = GetComponent<PhotonView>();
            _readyToCount = false;
            _readyToStart = false;
            _lessThanMaxPlayers = startingTime;
            _atMaxPlayers = 2;
            _timeToStart = startingTime;
            isGameLoaded = false;
        }

        private void Update()
        {
            //Multiplayer settings
            if (MultiplayerSettings.Instance.delayStart)
            {
                //Info text
                if (PhotonNetwork.InRoom)
                {
                    //If players in room are 1
                    if (PhotonNetwork.PlayerList.Length == 1)
                    {
                        //Getting room key
                        var privateRoomCode = PlayerPrefs.GetString(GameConstants.PrivateRoomCode);

                        if (string.IsNullOrEmpty(privateRoomCode) == false)
                        {
                            infoText.text = $"Please provide code {privateRoomCode} to your friend!";
                        }
                        else
                        {
                            if(infoText)
                                infoText.text = "Connected! Waiting for Players! " + _photonPlayers.Length + " \\ "
                                            + MultiplayerSettings.Instance.maxPlayers;
                        }
                    }
                    else
                    {
                        infoText.text = "The Game is About to Start!";
                        
                        //Disabling logo and turning on connected logo. Only displaying this if the logos
                        //are available in the scene
                        if (waitingForPlayer1Logo != null)
                        {
                            waitingForPlayer1Logo.SetActive(false);
                        }

                        if (waitingForPlayer2Logo != null)
                        {
                            waitingForPlayer2Logo.SetActive(false);
                        }

                        if (connectedLogo != null)
                        {
                            connectedLogo.SetActive(true);
                        }
                    }
                }

                //1 Player in room
                if (playersInRoom == 1)
                {
                    RestartTimer();
                }

                //When not game loaded
                if (!isGameLoaded)
                {
                    if (_readyToStart)
                    {
                        _atMaxPlayers -= Time.deltaTime;
                        _lessThanMaxPlayers = _atMaxPlayers;
                        _timeToStart = _atMaxPlayers;
                    }
                    else if (_readyToCount)
                    {
                        _lessThanMaxPlayers -= Time.deltaTime;
                        _timeToStart = _lessThanMaxPlayers;
                        Debug.Log("Display time remaining for start " + _timeToStart);
                    }

                    if (_timeToStart <= 0)
                    {
                        StartGame();
                    }
                }
            }
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("Room joined");
            //Getting room info
            _photonPlayers = PhotonNetwork.PlayerList;
            playersInRoom++;
            playerInGame = _photonPlayers.Length;
            myNumberInRoom = playersInRoom;
            print("My number in room" +
                  " is " + myNumberInRoom);
            //Getting saved nickname
            PhotonNetwork.NickName = PlayerPrefs.GetString(GameConstants.PlayerUserNameKey);
            //Delay start setting
            if (MultiplayerSettings.Instance.delayStart)
            {
                Debug.Log("Waiting for players\n " + playersInRoom + " \\ "
                          + MultiplayerSettings.Instance.maxPlayers);

                //Ready to count
                if (playersInRoom > 1)
                {
                    _readyToCount = true;
                }

                //Handling maximum player reach
                if (playersInRoom == MultiplayerSettings.Instance.maxPlayers)
                {
                    _readyToStart = true;
                    if (PhotonNetwork.IsMasterClient)
                        return;
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                }
            }
            else
            {
                StartGame();
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log("New player entered");
            playersInRoom++;
            _photonPlayers = PhotonNetwork.PlayerList;
            //Delay start
            if (MultiplayerSettings.Instance.delayStart)
            {
                Debug.Log("Waiting for players\n " + playersInRoom + " \\ "
                          + MultiplayerSettings.Instance.maxPlayers);
                //Ready to count
                if (playersInRoom > 1)
                {
                    _readyToCount = true;
                }

                //Max Players reached
                if (playersInRoom == MultiplayerSettings.Instance.maxPlayers)
                {
                    _readyToStart = true;
                    if (!PhotonNetwork.IsMasterClient)
                        return;
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                }
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);

            print($"Player with name {otherPlayer.NickName} Disconnected");

            //We will not run disconnection functions if we still have more than 1 player
            if (PhotonNetwork.PlayerList.Length > 1)
            {
                print("A player disconnected but there is more than 1 player in the game");
                return;
            }

            print("Trying to disconnect the player has only 1 player remains");

            if (DisconnectionHandlerScript.Instance)
            {
                DisconnectionHandlerScript.Instance.DisplayDisconnectionCanvasAndReturnToMainMenu();
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            Destroy(MultiplayerSettings.Instance.gameObject);
            Destroy(Instance.gameObject);
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        public void StartDisconnectRoutine()
        {
            StartCoroutine(nameof(Routine_DisconnectFromServers));
        }

        private IEnumerator Routine_DisconnectFromServers()
        {
            while (PhotonNetwork.IsConnected)
            {
                print("Trying to disconnect from Photon");
                PhotonNetwork.Disconnect();
                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary>
        /// For starting the multiplayer game
        /// </summary>
        void StartGame()
        {
            isGameLoaded = true;
            if (!PhotonNetwork.IsMasterClient)
                return;

            Debug.Log("Start game func started");
            //Delay start setting
            if (MultiplayerSettings.Instance.delayStart)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }

            PhotonNetwork.LoadLevel(MultiplayerSettings.Instance.multiPlayerScene);
        }

        /// <summary>
        /// To restart timer
        /// </summary>
        void RestartTimer()
        {
            _lessThanMaxPlayers = startingTime;
            _timeToStart = startingTime;
            _atMaxPlayers = 2;
            _readyToCount = false;
            _readyToStart = false;
        }

        /// <summary>
        /// Scene loading callback
        /// </summary>
        void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            currentScene = scene.buildIndex;
            if (currentScene == MultiplayerSettings.Instance.multiPlayerScene)
            {
                isGameLoaded = true;
                
                //Delay start loading
                if (MultiplayerSettings.Instance.delayStart)
                {
                    _PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
                }
                else
                {
                    RPC_CreatePlayer();
                }
            }
        }

        #endregion

        #region RPCs,

        /// <summary>
        /// RPC Loaded Game Scene
        /// </summary>
        [PunRPC]
        private void RPC_LoadedGameScene()
        {
            playerInGame++;
            if (playerInGame == PhotonNetwork.PlayerList.Length)
            {
                _PV.RPC("RPC_CreatePlayer", RpcTarget.All);
            }
        }

        /// <summary>
        /// Create Player RPC
        /// </summary>
        [PunRPC]
        private void RPC_CreatePlayer()
        {
            var networkPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", 
                    "PhotonNetworkPlayer"),
                Vector3.zero,
                Quaternion.identity,
                0);

            networkPlayer.GetComponent<PhotonPlayer>().SpawnAvatarAndSetRoomNumber();
        }

        #endregion
    }
}