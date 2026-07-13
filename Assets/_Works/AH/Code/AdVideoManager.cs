using JYG._Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;

namespace AH.Code
{
    public class AdVideoManager : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private RawImage rawImage;
        [SerializeField] private VideoClip videoClip;
        [SerializeField] private RenderTexture renderTexture;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private AudioSource audioSource;
        [ContextMenu("PlayVideo")]
        public void PlayVideo()
        {
            rawImage.texture = renderTexture;
            videoPlayer.Play();
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        [ContextMenu("PauseVideo")]
        public void StopVideo()
        {
            rawImage.texture = null;
            videoPlayer.Stop();
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}