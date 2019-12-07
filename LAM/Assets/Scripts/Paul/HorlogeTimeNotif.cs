using UnityEngine;
using System;
using Assets.SimpleAndroidNotifications;

public class HorlogeTimeNotif : MonoBehaviour
{
    public void OnEnable()
    {
        Debug.Log("la notification affiche : " + DateTime.Now.ToString("HH:mm"));

        var notificationParams = new NotificationParams
        {
            Id = UnityEngine.Random.Range(0, int.MaxValue),
            Delay = TimeSpan.FromSeconds(1),
            Title = "Quelque chose ne tourne pas rond ici...",
            Message = DateTime.Now.ToString("HH:mm"),
            Ticker = "Ticker",
            Sound = true,
            Vibrate = true,
            Light = true,
            SmallIcon = NotificationIcon.Clock,
            SmallIconColor = Color.black,
            LargeIcon = "app_icon"
        };

        NotificationManager.SendCustom(notificationParams);
    }
}
