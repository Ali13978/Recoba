using System;
using System.Collections;
using Game_Designer_Online.Scripts.Photon_Setup_Scripts;
using UnityEngine;

namespace Game_Designer_Online.Scripts.App_Settings_Scripts
{
    /// <summary>
    /// Simple closes the splash screen when the splash screen is in transition and also requests
    /// the PhotonLobby script to try to connect to Photon
    /// </summary>
    public class SplashScreenScript : MonoBehaviour
    {
        [SerializeField] private Animator thisAnimator;
        [SerializeField] private GameObject splashAnimationObject;

        private void Start()
        {
            StartCoroutine(nameof(Routine_EnableSplashAnimation));
        }

        /*private void Update()
        {
            if (thisAnimator.IsInTransition(0))
            {
                PhotonLobby.Instance.RequestConnectionToPhoton();
            }
        }*/

        private IEnumerator Routine_EnableSplashAnimation()
        {
            yield return new WaitForSeconds(5.0f);
            PhotonLobby.Instance.RequestConnectionToPhoton();
        }
    }
}