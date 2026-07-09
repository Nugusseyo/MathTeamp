using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class AndroidVolumeUI : MonoBehaviour
{
    public void ShowVolumeUI()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                try
                {
                    AndroidJavaObject audioManager =
                        activity.Call<AndroidJavaObject>("getSystemService", "audio");

                    const int STREAM_MUSIC = 3;
                    const int ADJUST_SAME = 0;
                    const int FLAG_SHOW_UI = 1;

                    audioManager.Call("adjustStreamVolume",
                        STREAM_MUSIC, ADJUST_SAME, FLAG_SHOW_UI);

                    audioManager.Dispose();
                }
                catch (System.Exception innerEx)
                {
                    Debug.LogError("볼륨 UI 표시 실패(UI 스레드): " + innerEx);
                }
                finally
                {
                    activity.Dispose();
                    unityPlayer.Dispose();
                }
            }));
        }
        catch (System.Exception e)
        {
            Debug.LogError("볼륨 UI를 열 수 없습니다: " + e);
        }
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        try
        {
            Process.Start("sndvol.exe");
        }
        catch (System.Exception e)
        {
            Debug.LogError("볼륨 UI를 열 수 없습니다: " + e.Message);
        }
#endif
    }
}