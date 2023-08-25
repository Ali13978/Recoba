using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon.Encryption;
using Game_Designer_Online.Scripts.Photon_Setup_Scripts;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Designer_Online.Scripts.Category_Scripts
{
    /// <summary>
    /// A script that contains all the functions required for the men's language category which is the black card
    /// to function properly. Most of the RPCs and functions that are in this script will mostly be run on the master
    /// client. This script is also responsible for displaying the result on this screen and is also responsible for
    /// displaying the questions. In fact, if you wish to change questions or anything within the questions, please
    /// change the Dictionary that you find in this script. This script also displays the winner.
    /// </summary>
    public class MensLanguageScript : MonoBehaviour
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
        [SerializeField]
        private Text optionOneText,
            optionTwoText,
            optionThreeText,
            optionFourText;

        //Option Button References
        [SerializeField]
        private Button optionOneButton,
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
            new List<Dictionary<string, Dictionary<List<string>, int>>>
            {
                //First question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "When you are in bed and he asks “Did you come”, he’s secretly insecure about the answer?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "TRUE",
                                    "FALSE",
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
                        "When a guy keeps saying “I don’t like to play games” he’s lying and has a PhD (Doctor of Deceit) in mind games?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "TRUE",
                                    "FALSE",
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
                        "He’s not into you if he makes the excuse that he’s “really busy”, without giving you an explanation?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "TRUE",
                                    "FALSE",
                                    "",
                                    ""
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
                        "If a guy invites you round to “Netflix and Chill” aka \"watch a DVD\", " +
                        "it means \"Dick Via Deception\" and he actually wants to sleep with you?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "TRUE",
                                    "FALSE",
                                    "",
                                    ""
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
                        "You Know He Really Likes You When... He calls you by an affectionate/pet name when other people he knows are around?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Yes",
                                    "No",
                                    "It's complicated",
                                    ""
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
                        "You Know He Really Likes You When... He asks you to be his girlfriend and you haven't slept together?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Yes",
                                    "No",
                                    "It's complicated - e.g he may just say it so he can sleep with you #WhatAShit",
                                    ""
                                },
                                //Correct answer
                                3
                            },
                        }
                    },
                },
                //Seventh Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "You Know He Really Likes You When... He talks about his future plans with you e.g. having children or moving abroad?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Yes",
                                    "No",
                                    "It's complicated",
                                    ""
                                },
                                //Correct answer
                                1
                            },
                        }
                    },
                },
                //Eighth Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "You Know He Really Likes You When... He calls you at least once a day and not just late at night for a 'booty call'?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
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
                //Ninth Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "Why Do Guys Say... \"It’s not you; it's me\", instead of admitting the real reason they want to break up with you?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "They enjoy playing mind games",
                                    "They are very bad at expressing themselves",
                                    "So they can have a quick guilt free exit from the relationship",
                                    "So they don’t have to admit they were just in it for the sex"
                                },
                                //Correct answer
                                3
                            },
                        }
                    },
                },
                //Tenth Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "Why Do Guys Say... \"I like independent women\" but then behave in an insecure/immature way " +
                        "when they meet one?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    //"They don't realise what being an independent woman means and feel threatened when they date one",
                                    "The immature behaviour is a reaction to something the woman does",
                                    "They like playing mind games in order to feel ‘in control’ of the relationship",
                                    "They think saying it makes them sound more secure and appealing in general",
                                    "They don’t realise they’re being insecure and will stop as soon as its pointed out to them",
                                },
                                //Correct answer
                                1
                            },
                        }
                    },
                },
                //Eleventh Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "Why Do Guys Say... \"I'm not looking for a serious relationship\" and then still expect sex?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "They are genuinely not looking for a serious relationship and think their ‘honesty’ makes it ok",
                                    "They’ve said it before to a woman and she has slept with him, so he thinks \"why not try it again.\"",
                                    "Only emotionally immature men think like this",
                                    "They think women are like men and can easily 'hook up' without any emotions getting involved",
                                },
                                //Correct answer
                                1
                            },
                        }
                    },
                },
                //Twelfth Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "Why Do Guys Say... \"I’ll call you\" and then don't?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "He lost your number",
                                    "He’s not that into you, or it was an ego boost for him to confirm that he could get your number",
                                    "He already has a partner and is trying to do the right thing now because he feels guilty",
                                    //"He accidentally deleted your number",
                                    "He lost his phone",
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
                        "What He Really Thinks When... His partner asks him \"where is this going\" or \"what are we\", and he's not really into her?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Tell her the truth about how he feels",
                                    "Lie because the sex is good so he doesn't want the relationship to end",
                                    "Respond in a passive aggressive way, e.g. “Why don't you tell me?”",
                                    "Pretend he never heard the question",
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
                        "True or False/Survey... In previous relationships how often did you communicate with" +
                        " your partner over the phone?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Throughout the day via text and at least one phone call",
                                    "We don't speak on the phone during work hours but we might send a text",
                                    "We only text if its urgent but we talk at least once",
                                    "We only text/talk if its urgent or something important",
                                },
                                //Correct answer
                                2
                            },
                        }
                    },
                },
                //Fifteenth Question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "True or False/Survey... Why do men send dick pics to women they barely know/are" +
                        " not their partner?",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "They think it will turn on the recipient of the picture",
                                    "It boosts their ego/low self-esteem",
                                    "They think it will encourage the recipient to send back pictures of their" +
                                    " body parts",
                                    "The shock value, especially if they have an underlying porn/sex addiction",
                                },
                                //Correct answer
                                2
                            },
                        }
                    },
                },
            };
        private readonly List<Dictionary<string, Dictionary<List<string>, int>>> _listOfFreeQuestions =
            new List<Dictionary<string, Dictionary<List<string>, int>>>
            {
                //First question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "What do guys... Get sexual so quickly on dating apps",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "We are only on thw app to hook-up",
                                    "They dont expect anything to happen, Its all better",
                                    "A positive response means that she is not interested in serious relationship \n #ItsActuallyATest",
                                    "They can be as inapropriate as they want with very little consequences"
                                },
                                //Correct answer
                                3
                            },
                        }
                    },
                },
                //Second question
                new Dictionary<string, Dictionary<List<string>, int>>()
                {
                    {
                        "True or False/ Survey - Why do guys find to hard to answer the question: What are you thinking about",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "He is thinking about sex and is embarrased to admit it",
                                    "His mind is full of random things and is embarrased to admit it",
                                    "He is thinking about something extreemly personnal ans admitting it make it feel uncomfortable",
                                    "He is thinking about something that will cause an argument, eg his ex"
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
                        "True or False/ Survey - Why would words of affirmation be a guys' main love language",
                        new Dictionary<List<string>, int>
                        {
                            {
                                //List of options
                                new List<string>
                                {
                                    "Positive verbal reinforcement makes him feel loved and secure",
                                    "He is a harcissist and loves to hear people",
                                    "He was verbally abused as a child and has low self-esteem",
                                    "He finds it very difficult to except even this smallest amout of constructive critism"
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

                if(!isFree)
                    whoWinsObjectReference.SetActive(true);
                else
                    whoWinsObjectReference.SetActive(false);
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
        [SerializeField]
        private Sprite optionAImagePlayerOne,
            optionBImagePlayerOne,
            optionCImagePlayerOne,
            optionDImagePlayerOne;

        //Alphabet option images for Player Two
        [SerializeField]
        private Sprite optionAImagePlayerTwo,
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
        
        //User name texts on results row
        [SerializeField] private TextMeshProUGUI[] resultRowUserNameTexts;

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
                resultRowUserNameTexts[index].text = nickName;
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