using System;
using System.Collections;
using Game_Designer_Online.Scripts.Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game_Designer_Online.Scripts.Photon_Setup_Scripts
{
    /// <summary>
    /// Contains all the functions required for the login canvas to function
    /// properly
    /// </summary>
    public class LoginCanvasScript : MonoBehaviour
    {
        #region Login Canvas Functions

        [Header("Variables for Login Canvas")]
        //Username input field reference
        [SerializeField] private InputField usernameInputField;

        /// <summary>
        /// A field for the gender drop down component
        /// </summary>
        [SerializeField] private Dropdown genderDropDown;
        
        /// <summary>
        /// Stores the error text
        /// </summary>
        [SerializeField] private Text errorText;

        /// <summary>
        /// For the player name field
        /// </summary>
        private string _playerName = "";

        /// <summary>
        /// For the player username field
        /// </summary>
        private string _playerUserName = "";

        /// <summary>
        /// For storing the gender of the player
        /// </summary>
        private string _playerGender = "Male";

        /// <summary>
        /// For telling the player whether the player is player 1 or player 2
        /// </summary>
        private bool _isPlayerOne = false;

        /// <summary>
        /// Sets the username of the input field at the very start
        /// </summary>
        private void SetStoredUserNameAtStart()
        {
            //If the player has a stored username, set it to the username input field
            if (PlayerPrefs.HasKey(GameConstants.PlayerUserNameKey))
            {
                usernameInputField.text = PlayerPrefs.GetString(GameConstants.PlayerUserNameKey);
            }
            
            //Setting the player gender on the DropDown menu
            if (PlayerPrefs.HasKey(GameConstants.PlayerGenderKey))
            {
                //Setting the player's gender and storing it
                _playerGender = PlayerPrefs.GetString(GameConstants.PlayerGenderKey);
                
                //Modifying the dropdown component
                genderDropDown.value = _playerGender switch
                {
                    "Male" => 0,
                    "Female" => 1,
                    _ => genderDropDown.value
                };
            }
        }
        
        /// <summary>
        /// Runs when the clicked login button is clicked
        /// </summary>
        public void OnClickedLoginButton()
        {
            if (string.IsNullOrEmpty(_playerUserName) ||
                string.IsNullOrEmpty(_playerGender))
            {
                StartCoroutine(RoutineErrorText("A Required Field Was Empty!"));
                return;
            }

            if (_playerUserName.Contains(" "))
            {
                StartCoroutine(RoutineErrorText("Username is either empty or has a space!" +
                                                " Player remove the space"));
                return;
            }

            print($"Login details are {_playerName} {_playerUserName} {_playerGender}");

            //Setting player prefs
            PlayerPrefs.SetString(GameConstants.PlayerUserNameKey, _playerUserName);
            PlayerPrefs.SetString(GameConstants.PlayerGenderKey, _playerGender);

            PhotonLobby.Instance.OnBeginSearchClicked();

            //Handling the logo
            if (_isPlayerOne == true)
            {
                PhotonRoom.Instance.waitingForPlayer2Logo.SetActive(true);
                profilePictureImage.SetActive(false);
            }
            else
            {
                PhotonRoom.Instance.waitingForPlayer1Logo.SetActive(true);
                profilePictureImage.SetActive(false);
            }

            //Displaying login info
            PhotonRoom.Instance.infoText.gameObject.SetActive(false);
        }

        /// <summary>
        /// Stores the players username to be used later
        /// </summary>
        /// <param name="playerName"></param>
        public void OnPlayerNameInputChanged(string playerName)
        {
            _playerName = playerName;
        }
        
        /// <summary>
        /// Stores the username
        /// </summary>
        /// <param name="userName"></param>
        public void OnPlayerUserNameChanged(string userName)
        {
            _playerUserName = userName;
        }

        /// <summary>
        /// Changes the stored gender
        /// </summary>
        /// <param name="genderIndex"></param>
        public void OnGenderChanged(int genderIndex)
        {
            //Switch case that will change the gender based on index and also store the
            //value of the key
            switch (genderIndex)
            {
                case 0:
                    _playerGender = "Male";
                
                    //Setting the GenderKey Value
                    PlayerPrefs.SetString(GameConstants.PlayerGenderKey, _playerGender);
                    return;
                case 1:
                    _playerGender = "Female";
                
                    //Setting the GenderKey Value
                    PlayerPrefs.SetString(GameConstants.PlayerGenderKey, _playerGender);
                    break;
            }
        }

        /// <summary>
        /// A routine to display the error text for a while. The function that calls it also needs
        /// to pass the message that needs to be displayed
        /// </summary>
        /// <param name="message"></param>
        private IEnumerator RoutineErrorText(string message)
        {
            errorText.text = message;
            errorText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            errorText.gameObject.SetActive(false);
        }

        #endregion

        #region Lobby Type Functions

        [Header("Lobby Type Variables")]
        //Reference to the lobby type container
        [SerializeField]
        private GameObject lobbyTypeContainer;

        //Connected Logo
        [SerializeField] private GameObject connectedLogoReference;

        //GameObjects that we need to activate
        [SerializeField] private GameObject loginInformationContainer;

        //Login button reference
        [SerializeField] private GameObject loginButton;

        //Profile picture image reference
        [SerializeField] private GameObject profilePictureImage;

        //Private Lobby Code Screen
        [SerializeField] private GameObject privateLobbyCodeScreen;

        //Join Friend Screen
        [SerializeField] private GameObject joinFriendLobbyScreen;

        public void OnClickedLoginWhenGameStarts()
        {
            lobbyTypeContainer.SetActive(true);
            loginInformationContainer.SetActive(false);
            loginButton.SetActive(false);
            connectedLogoReference.SetActive(false);
        }

        public void OnClickedPrivateLobbyButton()
        {
            privateLobbyCodeScreen.SetActive(true);
            lobbyTypeContainer.SetActive(false);
        }

        public void OnClickedRandomLobbyButton()
        {
            loginInformationContainer.SetActive(true);
            loginButton.SetActive(true);
            profilePictureImage.SetActive(true);

            lobbyTypeContainer.SetActive(false);
        }

        public void OnClickedJoinFriendButton()
        {
            joinFriendLobbyScreen.SetActive(true);
            lobbyTypeContainer.SetActive(false);
        }

        #endregion

        #region Private Lobby Code Functions

        [Header("Private Lobby Code Variables")]
        //Reference to private lobby code screen
        [SerializeField]
        private GameObject privateLobbyCodeScreenRef;

        //A variable to store error text
        [SerializeField] private Text errorTextForRoomCode;

        //Random code text
        [SerializeField] private Text randomCodeText;

        //A variable to store the code
        private string _roomCode = "";

        //Stores the random code
        private int _randomCode;

        /// <summary>
        /// Stores the code that the player has input inside
        /// </summary>
        /// <param name="codeInput"></param>
        public void OnCodeInputFieldValueChanged(string codeInput)
        {
            _roomCode = codeInput;
        }

        public void OnCodeSubmitButtonClicked()
        {
            if (_roomCode.Length < 4 || string.IsNullOrEmpty(_roomCode))
            {
                StartCoroutine(Routine_ErrorTextForPrivateLobby("Please enter a 4 digit code"));
                return;
            }

            //Saving room code inside of a PlayerPrefs
            PlayerPrefs.SetString(GameConstants.PrivateRoomCode, _roomCode);

            _isPlayerOne = true;

            //Starting the game with these lines of code
            privateLobbyCodeScreenRef.SetActive(false);
            OnClickedLoginButton();
        }

        public void OnGenerateCodeButtonClicked()
        {
            _randomCode = Random.Range(1000, 10000);
            randomCodeText.text = _randomCode.ToString();
            _roomCode = randomCodeText.text;

            print("New code generated");
        }

        /// <summary>
        /// Displays the error text for the private lobby code for a while
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private IEnumerator Routine_ErrorTextForPrivateLobby(string message)
        {
            errorTextForRoomCode.text = message;
            errorTextForRoomCode.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            errorTextForRoomCode.gameObject.SetActive(false);

            yield break;
        }

        #endregion

        #region Join Friend Lobby Functions

        [Header("Join Friend Related Variables")]
        //Reference to private lobby code screen
        [SerializeField]
        private GameObject joinFriendScreenRef;

        //A variable to store error text
        [SerializeField] private Text errorTextForJoinFriend;

        //A variable to store the code
        private string _friendsCode = "";

        /// <summary>
        /// Stores the code that the friend has input
        /// </summary>
        /// <param name="codeInput"></param>
        public void OnFriendCodeInputFieldValueChanged(string codeInput)
        {
            _friendsCode = codeInput;
        }

        public void OnFriendCodeSubmitButtonClicked()
        {
            if (_friendsCode.Length < 4 || string.IsNullOrEmpty(_friendsCode))
            {
                StartCoroutine(Routine_ErrorTextForFriendCode("Please enter a 4 digit code"));
                return;
            }

            //Saving room code inside of a PlayerPrefs
            PlayerPrefs.SetString(GameConstants.FriendRoomCode, _friendsCode);

            joinFriendScreenRef.SetActive(false);
            PhotonRoom.Instance.infoText.gameObject.SetActive(false);
            OnClickedLoginButton();
        }

        /// <summary>
        /// A routine that will display the error text for a while on the join friend screen
        /// </summary>
        /// <returns></returns>
        private IEnumerator Routine_ErrorTextForFriendCode(string message)
        {
            errorTextForJoinFriend.text = message;
            errorTextForJoinFriend.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            errorTextForJoinFriend.gameObject.SetActive(false);

            yield break;
        }

        #endregion

        #region Unity Functions

        private void Start()
        {
            OnGenerateCodeButtonClicked();
            SetStoredUserNameAtStart();
        }

        #endregion
    }
}