using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Designer_Online.Scripts.Photon_Setup_Scripts
{
    /// <summary>
    /// A simple script for the input fields which the player will use to enter the code
    /// of the room that they either create or want to join
    /// </summary>
    public class CodeInputFieldScript : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputFieldText;
        
        /// <summary>
        /// This function will make sure that the input text is reset so that the
        /// placeholder text will show again
        /// </summary>
        private void ResetInputText()
        {
            inputFieldText.text = "";
        }

        private void OnEnable()
        {
            ResetInputText();
        }
    }
}