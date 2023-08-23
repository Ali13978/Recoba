using System;
using UnityEngine;
using UnityEngine.Video;

namespace Game_Designer_Online.Scripts.Animation_Scripts
{
    /// <summary>
    /// A script that will check when the video has ended and it will then simply display
    /// the check results animation video
    /// </summary>
    public class WhoWinsObjectKeyFrameScript : MonoBehaviour
    {
        [SerializeField] private GameObject questionContainerReference;
        [SerializeField] private VideoPlayer videoPlayer;
        
        public void DeactivateGameObjectOnLastFrame(VideoPlayer vp)
        {
            questionContainerReference.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }

        private void Start()
        {
            videoPlayer.loopPointReached += DeactivateGameObjectOnLastFrame;
        }
    }
}