using System.Diagnostics;
using UnityEngine;

public class AndroidVolumeUI : MonoBehaviour
{
    public void ShowVolumeUI()
    {
    #if UNITY_ANDROID && !UNITY_EDITOR
            using (AndroidJavaClass unityPlayer =
                   new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject activity =
                    unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    
                AndroidJavaObject audioManager =
                    activity.Call<AndroidJavaObject>("getSystemService", "audio");
    
                const int STREAM_MUSIC = 3;
                const int FLAG_SHOW_UI = 1;
    
                audioManager.Call(
                    "adjustStreamVolume",
                    STREAM_MUSIC,
                    0,
                    FLAG_SHOW_UI);
            }
    #elif UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    try
    {
        Process.Start("sndvol.exe");
    }
    catch (System.Exception e)
    {
        UnityEngine.Debug.LogError("볼륨 UI를 열 수 없습니다: " + e.Message);
    }
    #endif
    }
}
