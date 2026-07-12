using System;
using UnityEngine;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
namespace AH.Code
{
    public class NotificationManager : MonoBehaviour
    {
        private void Awake()
        {
            InitializeNotification();
        }

        public void Hello()
        {
            SendLocalNotification($"{gameObject.name}이 눌렸습니다!", "어쩌라구요ㅋ",3);
        }

        private void InitializeNotification()
        {
#if UNITY_ANDROID
            var channel = new AndroidNotificationChannel()
            {
                Id = "channel_hello",
                Name = "Default Channel",
                Importance = Importance.High,
                Description = "Generic notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
#endif
        }
        private void SendLocalNotification(string title, string text, int delaySeconds)
        {
#if UNITY_ANDROID
            var androidNotification = new AndroidNotification
            {
                Title = title,
                Text = text,
                FireTime = DateTime.Now.AddSeconds(delaySeconds),
                Style = NotificationStyle.BigTextStyle
            };
            AndroidNotificationCenter.SendNotification(androidNotification, "channel_hello");
#endif
        }
    }
}
