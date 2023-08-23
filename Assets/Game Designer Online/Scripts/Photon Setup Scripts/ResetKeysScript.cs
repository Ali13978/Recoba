using System;
using Game_Designer_Online.Scripts.Constants;
using UnityEngine;

namespace Game_Designer_Online.Scripts.Photon_Setup_Scripts
{
    /// <summary>
    /// A simple script that will reset all the keys
    /// </summary>
    public class ResetKeysScript : MonoBehaviour
    {
        private void OnEnable()
        {
            GameConstants.SetKeysAtStart();
        }
    }
}