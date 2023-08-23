using System.Collections.Generic;
using System.Linq;
using Game_Designer_Online.Scripts.Photon_Setup_Scripts;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Designer_Online.Scripts.Category_Scripts
{
    /// <summary>
    /// A script that contains all the functions required for when you are out category which is the blue card
    /// to function properly. Most of the RPCs and functions that are in this script will mostly be run on the master
    /// client. This script is also responsible for displaying the result on this screen and is also responsible for
    /// displaying the questions. In fact, if you wish to change questions or anything within the questions, please
    /// change the Dictionary that you find in this script. This script also displays the winner.
    /// </summary>
    public class WhenYouAreOutTogetherScript : MonoBehaviour
    {
        private bool isFree;
        [SerializeField] private GameObject resultsQuestionHolders;
        [SerializeField] private GameObject resultQuestionPrefab;
        #region References to Components, Objects and Scripts

        [Header("Reference to Components, Objects and Scripts")]
        //Photon View
        private PhotonView _myPhotonView;

        [SerializeField] private GameObject categoryCanvasGameObject;

        //Question text reference
        [SerializeField] private Text questionTextRef;

        //Reference to option text
        [SerializeField] private Text optionOneText,
            optionTwoText,
            optionThreeText,
            optionFourText;

        //Option Button References
        [SerializeField] private Button optionOneButton,
            optionTwoButton,
            optionThreeButton,
            optionFourButton;

        //Checking results button
        [SerializeField] private Button checkResultsButton;

        //Select new category button
        [SerializeField] private Button selectNewCategoryButton;

        //Skip button reference
        [SerializeField] private Button skipButton;

        /// <summary>
        /// This is the mark that is displayed on the screen when one of the player answers a question. It basically
        /// tells the other player that the other player has answered the question and is waiting for the player
        /// to answer the question. It appears as a green box next to the username of the player that answered the
        /// question once it is active
        /// </summary>
        [SerializeField] private GameObject[] questionAnsweredMark;

        //Question container
        [SerializeField] private GameObject questionsContainer;

        //Reference to result screen
        [SerializeField] private GameObject resultsScreen;

        //Winner text references
        [SerializeField] private GameObject[] winnerTextReferences;
        
        //Reference to who win's object
        [SerializeField] private GameObject whoWinsObjectReference;

        #endregion

        #region Functions for Displaying the Questions

        [Header("Questions Related Variables")]
        //List of questions with options list and correct answer
        private readonly List<Dictionary<string, Dictionary<List<string>, int>>> _listOfPaidQuestions =
            new()
            {
                //First question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "You Know He Really Likes You When... He remembers your shoe or ring size when you're out shopping?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Yes",
                                    "No, he may just have a good memory for those kind of things",
                                    "",
                                    ""
                                },
                                //Correct answer
                                1
                            },
                        }
                    },
                },
                //Second question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "You Know He Really Likes You When... After you go out on a date he always calls/texts to make sure you got home ok?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    //"Yes, this guy definitely cares about your wellbeing and want to make sure you're always safe",
                                    //"No, it could be a sign of controlling behavior",
                                    "Yes",
                                    "No",
                                    "",
                                    ""
                                },
                                //Correct answer
                                1
                            },
                        }
                    },
                },
                //Third question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "What He Really Thinks When... He's been seeing a woman for several months but has never invited her over to his place?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    //"My place is too untidy and I don't want her to know about how messy I am",
                                    "I can't risk her knowing just how untidy my home really is",
                                    "I can't risk her turning up unexpected and meeting my wife and kids",
                                    "I'm still waiting to see how serious this is before letting her come to my home",
                                    "I still live with my parents and I'm too embarrassed to admit it"
                                },
                                //Correct answer
                                1
                            },
                        }
                    },
                },
                //Forth question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "What He Really Thinks When... You're on a date and constantly dropping F-bombs or using the C word? #AndWeDontMeanCovid",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "He’s ok with it as long as you want to sleep with him at the end of the night",
                                    "He thinks it is rude/disrespectful and will want the date to be over ASAP",
                                    "He’s ok with it as long as you swear quietly",
                                    "He finds it a real turn on"
                                },
                                //Correct answer
                                1
                            },
                        }
                    },
                },
                //Fifth Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "True or False/Survey... When he goes to the bar and returns to see a good looking man talking to you, most men think...",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Cool, she probably knows him",
                                    "I will play it cool for a minute and then go over and let him know she's with me!",
                                    "Not cool - The relationship is over, it's a wrap!",
                                    "Great, I really want to talk to other people when we're out too!"
                                },
                                //Correct answer
                                3
                            },
                        }
                    },
                },
                //Sixth Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "True or False/Survey... When you're preparing for a night out by wearing something quite revealing, do most men...",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Feel very lucky to have such a confident and attractive partner",
                                    "Feel uncomfortable because they don't want other men staring at you all night",
                                    "Desperately want you to change but don't want to come across as insecure or controlling",
                                    "Don't care either way as long you have sex when you get back home"
                                },
                                //Correct answer
                                1
                            },
                        }
                    },
                },
                //Seventh Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "What He Really Thinks When... You bump into your friends/work colleagues and don't introduce him to them?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "He thinks you’re ashamed or embarrassed to be seen out with him",
                                    "He's ok with it because he would do the same if the roles were reversed",
                                    "He's suspicious and thinks you're secretly attracted to one of your friends/colleagues",
                                    "He's ok with it because he's shy/socially awkward",
                                },
                                //Correct answer
                                1
                            },
                        }
                    },
                },
                //Eight Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "Why Do Guys... Mainly talk about themselves and never ask their date any questions?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "They genuinely think that they're very interesting",
                                    "They genuinely don't realise that's what they're doing",
                                    "They want to impress you by detailing their life highlights",
                                    "They really don't care about what's going on in your life",
                                },
                                //Correct answer
                                2
                            },
                        }
                    },
                },
                //Ninth Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "What's the worst thing a guy can do on a first date?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Show you a picture of their ex-partner",
                                    "Expect you to pay",
                                    "Act rude and condescending to the waiter",
                                    "Expect sex on the first date",
                                },
                                //Correct answer
                                2
                            },
                        }
                    },
                },
                //Tenth Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "Where would most guys like to meet on a first date?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Over a coffee/at a bar",
                                    "Over Zoom/Skype",
                                    "At his place so he can cook for you",
                                    "In a hotel room",
                                },
                                //Correct answer
                                2
                            },
                        }
                    },
                },
                //Eleventh Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "What He Really Thinks When... You've been dating for several months and called him by your ex's name on more than one occasion?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "It’s all good you just have a bad memory #nothingtoseehere",
                                    "You're secretly still seeing your ex or want to get back with him",
                                    "Maybe you're not completely over your ex and we should talk about it",
                                    "You genuinely think we have the same name",
                                },
                                //Correct answer
                                2
                            },
                        }
                    },
                },
                //Twelfth question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "Why Do Guys... Stare at women without actually going up to them and saying, \"Hi\"?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Men are very visual and it takes them far longer to fully scope out a woman's physical appearance than vice versa",
                                    "Men are very basic, i.e. they don't even realise they are staring and will continue to passively appreciate a woman's looks",
                                    "They're scared of approaching her and being rejected",
                                    "They know they're staring but waiting for the woman to give some 'come and talk to me' eye contact",
                                },
                                //Correct answer
                                2
                            },
                        }
                    },
                },
                //Thirteenth Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "What He Really Thinks When... You’re constantly taking selfies or on your phone during a date?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "It's fine, this type of 'multi tasking' is what all attractive/busy people do",
                                    "It's ok as long as you send me some intimate pictures of your body after the date",
                                    "It's cool, it gives me a chance to see who else i've matched with on my dating app!",
                                    "It's rude and makes you look like a a narcissist",
                                },
                                //Correct answer
                                2
                            },
                        }
                    },
                },
                //Fourteenth Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "True or False/Survey... What's the most annoying thing on a dating profile?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Bad pictures e.g. poor lighting",
                                    "Fake or overly sexual photographs",
                                    "Limited info - no info on what they want from a partner, kids, profession etc.",
                                    "Misogynistic or rude comments about the opposite sex"
                                },
                                //Correct answer
                                3
                            },
                        }
                    },
                },
                //Fifteenth Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "True or False/Survey... What is the best way for a woman to make the first move?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Just smile he'll do the rest",
                                    "No, never, how!",
                                    "ask for his opinion or compliment him",
                                    "Make physical contact"
                                },
                                //Correct answer
                                3
                            },
                        }
                    },
                },
            };
        private readonly List<Dictionary<string, Dictionary<List<string>, int>>> _listOfFreeQuestions =
            new()
            {
                //First question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "True or False/ Survey The best thing a women can do on a first date is...?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "To laugh at his jokes",
                                    "To pay for the date",
                                    "Turn up wearing a revealing outfit",
                                    "Show a high level of emotional intelligence"
                                },
                                //Correct answer
                                4
                            },
                        }
                    },
                },
                //Second question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "True or False/ Survey - On a first date, which of these do guys think is not a red flag",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    //"Yes, this guy definitely cares about your wellbeing and want to make sure you're always safe",
                                    //"No, it could be a sign of controlling behavior",
                                    "Expecting sex because he paid for the date",
                                    "Calling before hand to ask what you are going to wear on the date",
                                    "Making negative comments about your appearance",
                                    "Commenting: dont you think you were on social media very late the other day"
                                },
                                //Correct answer
                                4
                            },
                        }
                    },
                },
                //Third question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "True or False/ Survey - What do guys think is most romantic think to do for their partner",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    //"My place is too untidy and I don't want her to know about how messy I am",
                                    "Write her an edible love letter made of chocolate? #ItsAThing",
                                    "Plan a romantic weekend getaway",
                                    "Get her name tattooed to his body",
                                    "Take her to male strip club"
                                },
                                //Correct answer
                                1
                            },
                        }
                    },
                },
            };

        private List<Dictionary<string, Dictionary<List<string>, int>>> _listOfQuestions = new List<Dictionary<string, Dictionary<List<string>, int>>>();
        private List<GameObject> resultQuestionsList;

        //Stores the current question number
        private int _currentQuestionNumber = 0;

        /// <summary>
        /// Displays the question with all the options
        /// </summary>
        /// <param name="questionNumber"></param>
        [PunRPC]
        private void RPC_DisplayQuestionWithOptions(int questionNumber)
        {
            //Telling the game that the player can answer questions again
            _hasAnsweredQuestion = false;

            print($"Current question number is {questionNumber}");

            //Turning question answered mark to false
            questionAnsweredMark[0].SetActive(false);
            questionAnsweredMark[1].SetActive(false);

            //When we are at the last question, we will activate the checkResults button to allow player 1
            //to display the results
            if ((_currentQuestionNumber > 14 && !isFree) || (_currentQuestionNumber > 2 && isFree))
            {
                print("Questions Finished. Prepare to Show Results Screen!");

                //Telling the players that they need to use Check Results now by displaying it on the screen
                questionTextRef.text = "Well done for completing the category. Click Below to check results!";

                //Telling the game that the player has answered questions and also deactivating the green mark
                //as now we are ready to display the results
                _hasAnsweredQuestion = true;
                questionAnsweredMark[0].SetActive(false);
                questionAnsweredMark[0].SetActive(false);

                //Taking off all the text from the buttons
                optionOneText.text = "";
                optionTwoText.text = "";
                optionThreeText.text = "";
                optionFourText.text = "";

                //Handling question buttons
                HandleInteractionPropertyOfButtons();

                //Setting up buttons to be displayed
                checkResultsButton.gameObject.SetActive(true);
                skipButton.gameObject.SetActive(false);
                whoWinsObjectReference.SetActive(true);
                return;
            }


            if (!isFree)
            {
                _listOfQuestions = _listOfPaidQuestions;
            }

            else
            {
                _listOfQuestions = _listOfFreeQuestions;
            }

            //Displaying the question here
            questionTextRef.text = _listOfQuestions[questionNumber].ElementAt(0).Key;

            //Displaying the options here
            optionOneText.text = _listOfQuestions[questionNumber].ElementAt(0).Value
                .ElementAt(0).Key[0];
            optionTwoText.text = _listOfQuestions[questionNumber].ElementAt(0).Value
                .ElementAt(0).Key[1];
            optionThreeText.text = _listOfQuestions[questionNumber].ElementAt(0).Value
                .ElementAt(0).Key[2];
            optionFourText.text = _listOfQuestions[questionNumber].ElementAt(0).Value
                .ElementAt(0).Key[3];

            HandleInteractionPropertyOfButtons();

            //Getting the correct option value
            _correctOption = _listOfQuestions[questionNumber].ElementAt(0).Value
                .ElementAt(0).Value;

            print($"Correct Option for current question is {_correctOption}");
        }

        /// <summary>
        /// This function will turn buttons grey if their text is null
        /// </summary>
        private void TurnButtonsGreyBasedOnWhetherTheyAreEmptyOrNot()
        {
            //Checking button one
            optionOneButton.GetComponent<Image>().color =
                string.IsNullOrEmpty(optionOneText.text) ? new Color(255, 255, 255, 0.3f) : new Color(255, 255, 255, 1.0f);

            //Checking button two
            optionTwoButton.GetComponent<Image>().color =
                string.IsNullOrEmpty(optionTwoText.text) ? new Color(255, 255, 255, 0.3f) : new Color(255, 255, 255, 1);

            //Checking button three
            optionThreeButton.GetComponent<Image>().color =
                string.IsNullOrEmpty(optionThreeText.text)
                    ? new Color(255, 255, 255, 0.3f)
                    : new Color(255, 255, 255, 1);
            //Checking button four
            optionFourButton.GetComponent<Image>().color =
                string.IsNullOrEmpty(optionFourText.text)
                    ? new Color(255, 255, 255, 0.3f)
                    : new Color(255, 255, 255, 1);
        }

        /// <summary>
        /// Turns the interaction properly on or off based on whether the text inserted is null or has some data
        /// </summary>
        private void HandleInteractionPropertyOfButtons()
        {
            optionOneButton.interactable = !string.IsNullOrEmpty(optionOneText.text);
            optionTwoButton.interactable = !string.IsNullOrEmpty(optionTwoText.text);
            optionThreeButton.interactable = !string.IsNullOrEmpty(optionThreeText.text);
            optionFourButton.interactable = !string.IsNullOrEmpty(optionFourText.text);

            TurnButtonsGreyBasedOnWhetherTheyAreEmptyOrNot();
        }

        /// <summary>
        /// Handles the loading of the next question
        /// </summary>
        private void CheckIfNextQuestionShouldBeLoaded()
        {
            //We will return if one of the question marks is not turned on
            if (questionAnsweredMark[0].activeSelf == false || questionAnsweredMark[1].activeSelf == false)
                return;

            //Telling the game to increase the question number
            _currentQuestionNumber++;

            //Master client will attempt to change the question on screen
            if (PhotonNetwork.IsMasterClient)
            {
                _myPhotonView.RPC(
                    "RPC_DisplayQuestionWithOptions",
                    RpcTarget.All,
                    _currentQuestionNumber
                );
            }
        }

        #endregion

        #region All Functions Required for Option Buttons + Answers

        [Header("Variables for Options Buttons")]
        //When true, tells the game that this player has answered the question
        private bool _hasAnsweredQuestion = false;

        //Stores the correct option
        private int _correctOption = 0;

        //Stores the option that the player selected
        private int _optionSelected = 0;

        private void OnFirstOptionButtonClicked()
        {
            if (_hasAnsweredQuestion == true) return;

            if (PhotonNetwork.IsMasterClient)
            {
                print("Master client clicked option 1");
                _hasAnsweredQuestion = true;
                _optionSelected = 1;

                _myPhotonView.RPC("RPC_HandleQuestionAnsweredMark", RpcTarget.All, 0);
                _myPhotonView.RPC("RPC_StoreTheOptionThatThePlayerSelected", RpcTarget.All, 1,
                    _optionSelected);

                return;
            }

            print("Foreign player clicked option 1");
            _hasAnsweredQuestion = true;
            _optionSelected = 1;

            _myPhotonView.RPC("RPC_HandleQuestionAnsweredMark", RpcTarget.All, 1);
            _myPhotonView.RPC("RPC_StoreTheOptionThatThePlayerSelected", RpcTarget.All, 2,
                _optionSelected);
        }

        private void OnSecondOptionButtonClicked()
        {
            if (_hasAnsweredQuestion == true) return;

            if (PhotonNetwork.IsMasterClient)
            {
                print("Master client clicked option 2");
                _hasAnsweredQuestion = true;
                _optionSelected = 2;

                _myPhotonView.RPC("RPC_HandleQuestionAnsweredMark", RpcTarget.All, 0);
                _myPhotonView.RPC("RPC_StoreTheOptionThatThePlayerSelected", RpcTarget.All, 1,
                    _optionSelected);

                return;
            }

            print("Foreign player clicked option 2");
            _hasAnsweredQuestion = true;
            _optionSelected = 2;

            _myPhotonView.RPC("RPC_HandleQuestionAnsweredMark", RpcTarget.All, 1);
            _myPhotonView.RPC("RPC_StoreTheOptionThatThePlayerSelected", RpcTarget.All, 2,
                _optionSelected);
        }

        private void OnThirdOptionButtonClicked()
        {
            if (_hasAnsweredQuestion == true) return;

            if (PhotonNetwork.IsMasterClient)
            {
                print("Master client clicked option 3");
                _hasAnsweredQuestion = true;
                _optionSelected = 3;

                _myPhotonView.RPC("RPC_HandleQuestionAnsweredMark", RpcTarget.All, 0);
                _myPhotonView.RPC("RPC_StoreTheOptionThatThePlayerSelected", RpcTarget.All, 1,
                    _optionSelected);

                return;
            }

            print("Foreign player clicked option 3");
            _hasAnsweredQuestion = true;
            _optionSelected = 3;

            _myPhotonView.RPC("RPC_HandleQuestionAnsweredMark", RpcTarget.All, 1);
            _myPhotonView.RPC("RPC_StoreTheOptionThatThePlayerSelected", RpcTarget.All, 2,
                _optionSelected);
        }

        private void OnForthOptionButtonClicked()
        {
            if (_hasAnsweredQuestion == true) return;

            if (PhotonNetwork.IsMasterClient)
            {
                print("Master client clicked option 4");
                _hasAnsweredQuestion = true;
                _optionSelected = 4;

                _myPhotonView.RPC("RPC_HandleQuestionAnsweredMark", RpcTarget.All, 0);
                _myPhotonView.RPC("RPC_StoreTheOptionThatThePlayerSelected", RpcTarget.All, 1,
                    _optionSelected);

                return;
            }

            print("Foreign player clicked option 4");
            _hasAnsweredQuestion = true;
            _optionSelected = 4;

            _myPhotonView.RPC("RPC_HandleQuestionAnsweredMark", RpcTarget.All, 1);
            _myPhotonView.RPC("RPC_StoreTheOptionThatThePlayerSelected", RpcTarget.All, 2,
                _optionSelected);
        }

        private void OnSkipOptionButtonClicked()
        {
            if (_hasAnsweredQuestion == true) return;

            if (PhotonNetwork.IsMasterClient)
            {
                print("Master client skipped question");
                _hasAnsweredQuestion = true;
                _optionSelected = 0;

                _myPhotonView.RPC("RPC_HandleQuestionAnsweredMark", RpcTarget.All, 0);
                _myPhotonView.RPC("RPC_StoreTheOptionThatThePlayerSelected", RpcTarget.All, 1,
                    _optionSelected);

                return;
            }

            print("Foreign client skipped question");
            _hasAnsweredQuestion = true;
            _optionSelected = 0;

            _myPhotonView.RPC("RPC_HandleQuestionAnsweredMark", RpcTarget.All, 1);
            _myPhotonView.RPC("RPC_StoreTheOptionThatThePlayerSelected", RpcTarget.All, 2,
                _optionSelected);
        }

        /// <summary>
        /// A function that will setup all the listeners for the button
        /// </summary>
        private void SetUpOptionButtonListeners()
        {
            optionOneButton.onClick.AddListener(OnFirstOptionButtonClicked);
            optionTwoButton.onClick.AddListener(OnSecondOptionButtonClicked);
            optionThreeButton.onClick.AddListener(OnThirdOptionButtonClicked);
            optionFourButton.onClick.AddListener(OnForthOptionButtonClicked);

            skipButton.onClick.AddListener(OnSkipOptionButtonClicked);

            checkResultsButton.onClick.AddListener(OnCheckResultsButtonClicked);
            selectNewCategoryButton.onClick.AddListener(OnClickedSelectNewCategoryButtonClicked);
        }

        /// <summary>
        /// Turns on the question answered mark based on the index passed to it across the network
        /// </summary>
        /// <param name="index"></param>
        [PunRPC]
        private void RPC_HandleQuestionAnsweredMark(int index)
        {
            questionAnsweredMark[index].SetActive(true);
            //Only the master client checks whether both players have answered the question or not
            CheckIfNextQuestionShouldBeLoaded();
        }

        #endregion

        #region Functions for Storing Results and Displaying results

        [Header("Variables for Storing Results")]
        //Options for player one
        [SerializeField]
        private List<int> _optionsThatPlayerOneClicked = new List<int>();

        //Option for player two
        [SerializeField] private List<int> _optionsThatPlayerTwoClicked = new List<int>();

        //Lists that will tell the game whether the player answered the question correctly or not
        [SerializeField] private List<bool> _playerOneScoreList, _playerTwoScoreList;

        //List of player image answer texts. This will be used to display with option the player
        //actually selected when answering the questions
        [SerializeField] private List<Image> playerOneAnswerImages, playerTwoAnswerImages;

        //True false images for player one
        [SerializeField] private Sprite trueImagePlayerOne, falseImagePlayerOne;

        //True false images for player two
        [SerializeField] private Sprite trueImagePlayerTwo, falseImagePlayerTwo;

        //Alphabet option images for Player One
        [SerializeField] private Sprite optionAImagePlayerOne,
            optionBImagePlayerOne,
            optionCImagePlayerOne,
            optionDImagePlayerOne;

        //Alphabet option images for Player Two
        [SerializeField] private Sprite optionAImagePlayerTwo,
            optionBImagePlayerTwo,
            optionCImagePlayerTwo,
            optionDImagePlayerTwo;

        //A list of winner texts. This will be used to tell the game which player won the question
        [SerializeField] private List<TextMeshProUGUI> winnerTextList;

        //Tells the game that the result was processed when true
        private bool _resultWasProcessed = false;

        //Stores player one correct answers
        private int _playerOneCorrectAnswers = 0;

        private void OnCheckResultsButtonClicked()
        {
            //If we are not the master client, we will not run the code
            if (PhotonNetwork.IsMasterClient == false)
            {
                return;
            }

            _myPhotonView.RPC(
                "RPC_DisplayTheResultsScreen",
                RpcTarget.All
            );
        }

        private void OnClickedSelectNewCategoryButtonClicked()
        {
            //If we are not the master client, we will not run the code
            if (PhotonNetwork.IsMasterClient == false)
            {
                return;
            }

            print("New Category button clicked");

            _myPhotonView.RPC(
                "RPC_SelectNewCategoryProcess",
                RpcTarget.All
            );
        }

        /// <summary>
        /// This function will 
        /// </summary>
        /// <param name="questionIndex"></param>
        /// <param name="userNumber"></param>
        /// <param name="usersSelection"></param>
        private void SetAnswerImageForUser(int questionIndex, int userNumber, int usersSelection)
        {
            //Trying to find out whether this question is a true, false question or not. If the first key's value
            //is "True", then it is a true false question
            var firstKeyValue =
                _listOfQuestions[questionIndex].ElementAt(0).Value.ElementAt(0).Key[0];

            if (firstKeyValue == "TRUE")
            {
                //If the user skipped the question
                if (usersSelection == 0)
                {
                    //If the first user is currently being processed
                    if (userNumber == 1)
                    {
                        playerOneAnswerImages[questionIndex].sprite = null;
                    }
                    else
                    {
                        playerTwoAnswerImages[questionIndex].sprite = null;
                    }
                }

                //If user selected true
                if (usersSelection == 1)
                {
                    //If the first user is currently being processed
                    if (userNumber == 1)
                    {
                        playerOneAnswerImages[questionIndex].sprite = trueImagePlayerOne;
                        playerOneAnswerImages[questionIndex].rectTransform.sizeDelta
                            = new Vector2(35, 18.5f);
                    }
                    else
                    {
                        playerTwoAnswerImages[questionIndex].sprite = trueImagePlayerTwo;
                        playerTwoAnswerImages[questionIndex].rectTransform.sizeDelta
                            = new Vector2(35, 18.5f);
                    }
                }

                //If user selected false
                if (usersSelection == 2)
                {
                    //If the first user is currently being processed
                    if (userNumber == 1)
                    {
                        playerOneAnswerImages[questionIndex].sprite = falseImagePlayerOne;
                        playerOneAnswerImages[questionIndex].rectTransform.sizeDelta
                            = new Vector2(35, 18.5f);
                    }
                    else
                    {
                        playerTwoAnswerImages[questionIndex].sprite = falseImagePlayerTwo;
                        playerTwoAnswerImages[questionIndex].rectTransform.sizeDelta
                            = new Vector2(35, 18.5f);
                    }
                }
            }
            else
            {
                //If the user skipped the question
                if (usersSelection == 0)
                {
                    //If the first user is currently being processed
                    if (userNumber == 1)
                    {
                        playerOneAnswerImages[questionIndex].sprite = null;
                    }
                    else
                    {
                        playerTwoAnswerImages[questionIndex].sprite = null;
                    }
                }

                //If user selected option one
                if (usersSelection == 1)
                {
                    //If the first user is currently being processed
                    if (userNumber == 1)
                    {
                        playerOneAnswerImages[questionIndex].sprite = optionAImagePlayerOne;
                        playerOneAnswerImages[questionIndex].rectTransform.sizeDelta
                            = new Vector2(16.5f, 16.0f);
                    }
                    else
                    {
                        playerTwoAnswerImages[questionIndex].sprite = optionAImagePlayerTwo;
                        playerTwoAnswerImages[questionIndex].rectTransform.sizeDelta
                            = new Vector2(16.5f, 16.0f);
                    }
                }

                //If user selected option two
                if (usersSelection == 2)
                {
                    //If the first user is currently being processed
                    if (userNumber == 1)
                    {
                        playerOneAnswerImages[questionIndex].sprite = optionBImagePlayerOne;
                        playerOneAnswerImages[questionIndex].rectTransform.sizeDelta
                            = new Vector2(16.5f, 16.0f);
                    }
                    else
                    {
                        playerTwoAnswerImages[questionIndex].sprite = optionBImagePlayerTwo;
                        playerTwoAnswerImages[questionIndex].rectTransform.sizeDelta
                            = new Vector2(16.5f, 16.0f);
                    }
                }

                //If user selected option three
                if (usersSelection == 3)
                {
                    //If the first user is currently being processed
                    if (userNumber == 1)
                    {
                        playerOneAnswerImages[questionIndex].sprite = optionCImagePlayerOne;
                        playerOneAnswerImages[questionIndex].rectTransform.sizeDelta
                            = new Vector2(16.5f, 16.0f);
                    }
                    else
                    {
                        playerTwoAnswerImages[questionIndex].sprite = optionCImagePlayerTwo;
                        playerTwoAnswerImages[questionIndex].rectTransform.sizeDelta
                            = new Vector2(16.5f, 16.0f);
                    }
                }

                //If user selection option four
                if (usersSelection == 4)
                {
                    //If the first user is currently being processed
                    if (userNumber == 1)
                    {
                        playerOneAnswerImages[questionIndex].sprite = optionDImagePlayerOne;
                        playerOneAnswerImages[questionIndex].rectTransform.sizeDelta
                            = new Vector2(16.5f, 16.0f);
                    }
                    else
                    {
                        playerTwoAnswerImages[questionIndex].sprite = optionDImagePlayerTwo;
                        playerTwoAnswerImages[questionIndex].rectTransform.sizeDelta
                            = new Vector2(16.5f, 16.0f);
                    }
                }
            }
        }

        public void DestroyChildren(GameObject targetObject)
        {
            int childCount = targetObject.transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                Transform child = targetObject.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// RPC displays the result screen on both clients. This will compare the answers that both players have given
        /// and it will only store an answer as true if the first player manages to guess what the second player
        /// selected 
        /// </summary>
        [PunRPC]
        private void RPC_DisplayTheResultsScreen()
        {
            //We wil not run this again if we have already processed the results
            if (_resultWasProcessed == true) return;

            //Telling the game that the results were processed
            _resultWasProcessed = true;

            print("Processing Result Screen Questions");

            DestroyChildren(resultsQuestionHolders);
            resultQuestionsList = new List<GameObject>();

            for (int i = 0; i < _listOfQuestions.Count; i++)
            {
                GameObject resultObject = Instantiate(resultQuestionPrefab, resultsQuestionHolders.transform);

                TMP_Text questionText = resultObject.GetComponent<TMP_Text>();
                questionText.text = _listOfQuestions[i].Keys.FirstOrDefault();

                resultQuestionsList.Add(resultObject);
            }

            RefillPlayerOneAnswerImagesList();
            RefillPlayerTwoAnswerImagesList();
            RefillWinnerTextList();

            resultsScreen.SetActive(true);
            questionsContainer.SetActive(false);

            //Printing the result of player one
            for (int i = 0; i < _optionsThatPlayerOneClicked.Count; i++)
            {
                //Getting the correct answer from the correct question
                int correctAnswer = _listOfQuestions[i].ElementAt(0).Value
                    .ElementAt(0).Value;

                //If player two skipped the answer, player one automatically wins.
                //This is the code for that
                /*if (_optionsThatPlayerTwoClicked[i] == 0)
                {
                    _playerOneScoreList.Add(true);
                    SetAnswerImageForUser(i, 1,
                        _optionsThatPlayerOneClicked[i]);
                    
                    _playerOneCorrectAnswers++;
                    
                    //Telling the game which player has won
                    winnerTextList[i].text = userNameTexts[0].text;
                    continue;
                }*/

                //Setting the element in Player One Score List to True if the player
                //managed to match the answer that player two gave. We will also add various items to the
                //list required for proper functionality and also display the option selected by the player
                if (_optionsThatPlayerOneClicked[i] ==
                    _optionsThatPlayerTwoClicked[i])
                {
                    _playerOneScoreList.Add(true);
                    SetAnswerImageForUser(i, 1,
                        _optionsThatPlayerOneClicked[i]);

                    _playerOneCorrectAnswers++;

                    //Telling the game which player has won
                    winnerTextList[i].text = userNameTexts[0].text;
                    continue;
                }

                _playerOneScoreList.Add(false);
                SetAnswerImageForUser(i, 1,
                    _optionsThatPlayerOneClicked[i]);

                //Telling the game which player has won
                winnerTextList[i].text = userNameTexts[1].text;
            }

            //Printing the result of player two using this loop. Determining who won this question and answer
            //battle is done in the loop above this one
            for (int i = 0; i < _optionsThatPlayerTwoClicked.Count; i++)
            {
                //Getting the correct answer from the correct question
                int correctAnswer = _listOfQuestions[i].ElementAt(0).Value
                    .ElementAt(0).Value;

                if (_optionsThatPlayerTwoClicked[i] == correctAnswer)
                {
                    _playerTwoScoreList.Add(true);
                    SetAnswerImageForUser(i, 2,
                        _optionsThatPlayerTwoClicked[i]);
                    continue;
                }

                _playerTwoScoreList.Add(false);
                SetAnswerImageForUser(i, 2,
                    _optionsThatPlayerTwoClicked[i]);
            }

            DeclaringWinnerTextBasedOnWhoGotTheMostAnswersCorrectly(
                _listOfQuestions.Count,
                _playerOneCorrectAnswers
            );
        }

        /// <summary>
        /// RPC that will disable the current results screen and return the game back to the category selection
        /// menu so that the player can select a brand new category
        /// </summary>
        [PunRPC]
        private void RPC_SelectNewCategoryProcess()
        {
            resultsScreen.SetActive(false);
            questionsContainer.SetActive(true);
            gameObject.SetActive(false);
            categoryCanvasGameObject.SetActive(true);
        }

        /// <summary>
        /// This will simply compare the number of correct Player One Answers with the number of questions
        /// If player one has answered half of the questions correctly, player one wins
        /// </summary>
        private void DeclaringWinnerTextBasedOnWhoGotTheMostAnswersCorrectly(int numberOfQuestions,
            int numberOfCorrectAnswersPlayerOneGave)
        {
            //Player one wins
            if (numberOfCorrectAnswersPlayerOneGave >= numberOfQuestions / 2)
            {
                winnerTextReferences[0].SetActive(true);
                return;
            }

            //Player 2 wins
            winnerTextReferences[1].SetActive(true);
        }

        /// <summary>
        /// Stores the option that the player has selected and stores it across the network
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="optionSelected"></param>
        [PunRPC]
        private void RPC_StoreTheOptionThatThePlayerSelected(int playerId, int optionSelected)
        {
            if (playerId == 1)
            {
                _optionsThatPlayerOneClicked.Add(optionSelected);
                print("Player one answered " + optionSelected);
            }
            else
            {
                _optionsThatPlayerTwoClicked.Add(optionSelected);
                print("Player two answered " + optionSelected);
            }
        }

        #endregion

        #region User Name Text Functions

        //Stores the user name texts
        [SerializeField] private Text[] userNameTexts;

        //Result screen usernames
        [SerializeField] private Text[] resultUserNameTexts;
        
        //result row username
        [SerializeField] private TextMeshProUGUI[] resultRowUserNameText;

        /// <summary>
        /// Sets the user name for both players when the screen is set active
        /// </summary>
        private void SetUserNamesOnScreenActivation()
        {
            //For loop to check which player is the local player and then setting the user name
            //according to the index for the local player
            for (var index = 0; index < MasterClientGameController.Instance.playerList.Count; index++)
            {
                //Getting the player from the player list and also getting its Photon View.
                //We will then find the nickname of the player
                GameObject player = MasterClientGameController.Instance.playerList[index];
                PhotonView playerPhotonView = player.GetComponent<PhotonView>();
                string nickName = playerPhotonView.Owner.NickName;

                //Setting up nicknames on the category screen
                userNameTexts[index].text = nickName;
                resultUserNameTexts[index].text = nickName;
                resultRowUserNameText[index].text = nickName;
            }
        }

        #endregion

        [PunRPC]
        private void SyncIsFreeValue(bool newValue)
        {
            isFree = newValue;
            Debug.Log($"Host is{(isFree ? " " : " not ")}on free version");
        }


        #region Unity Functions

        private void OnEnable()
        {
            _myPhotonView = GetComponent<PhotonView>();

            //Resetting variables or initializing them
            questionsContainer.SetActive(true);
            resultsScreen.SetActive(false);
            skipButton.gameObject.SetActive(false);
            checkResultsButton.gameObject.SetActive(false);
            _resultWasProcessed = false;
            _optionsThatPlayerOneClicked = new List<int>();
            _optionsThatPlayerTwoClicked = new List<int>();
            _playerOneScoreList = new List<bool>();
            _playerTwoScoreList = new List<bool>();
            winnerTextReferences[0].SetActive(false);
            winnerTextReferences[1].SetActive(false);


            isFree = VersionHandler.Instance.isFree;

            if (PhotonNetwork.IsMasterClient)
            {
                _myPhotonView.RPC("SyncIsFreeValue", RpcTarget.OthersBuffered, isFree);
            }

            Debug.Log($"Host is{(isFree ? " " : " not ")}on free version");

            //Setting all winner text lists back to their original value
            foreach (TextMeshProUGUI winnerTexts in winnerTextList)
            {
                winnerTexts.text = "";
            }

            SetUserNamesOnScreenActivation();

            //Setting up question number
            _currentQuestionNumber = 0;

            //Resetting player one answers
            _playerOneCorrectAnswers = 0;

            //Functions for the master client only
            if (PhotonNetwork.IsMasterClient)
            {
                _myPhotonView.RPC(
                    "RPC_DisplayQuestionWithOptions",
                    RpcTarget.All,
                    _currentQuestionNumber
                );
            }
        }

        private void Awake()
        {
            _myPhotonView = GetComponent<PhotonView>();
        }

        private void Start()
        {
            SetUpOptionButtonListeners();
        }

        #endregion

        private void RefillWinnerTextList()
        {
            winnerTextList.Clear();
            foreach (GameObject i in resultQuestionsList)
            {
                Transform Object = i.transform.GetChild(2);

                winnerTextList.Add(Object.GetComponent<TextMeshProUGUI>());
            }
        }

        private void RefillPlayerOneAnswerImagesList()
        {
            playerOneAnswerImages.Clear();
            foreach (GameObject i in resultQuestionsList)
            {
                Transform Object = i.transform.GetChild(0);

                playerOneAnswerImages.Add(Object.GetComponent<Image>());
            }
        }

        private void RefillPlayerTwoAnswerImagesList()
        {
            playerTwoAnswerImages.Clear();
            foreach (GameObject i in resultQuestionsList)
            {
                Transform Object = i.transform.GetChild(1);

                playerTwoAnswerImages.Add(Object.GetComponent<Image>());
            }
        }
    }
}
