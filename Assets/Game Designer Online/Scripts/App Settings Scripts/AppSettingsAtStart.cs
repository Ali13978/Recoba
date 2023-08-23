using System;
using UnityEngine;

namespace Game_Designer_Online.Scripts.App_Settings_Scripts
{
    /// <summary>
    /// Sets the application settings at the very start. This script will prevent the game from sleeping
    /// and also will set the framerate of the game at the very start
    /// </summary>
    public class AppSettingsAtStart : MonoBehaviour
    {
        private void Start()
        {
            Screen.SetResolution(1280, 720, FullScreenMode.ExclusiveFullScreen);
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 30;
        }
    }
}