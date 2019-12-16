using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class AndroidNotificationManager : MonoBehaviour
{
    public AndroidNotificationChannel defaultNotificationChannel;

    private int identifier;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultNotificationChannel = new AndroidNotificationChannel()
        {
            Id = "channel_channel",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(defaultNotificationChannel);

        AndroidNotification notification = new AndroidNotification()
        {
            Title = "test Notification !PLZ",
            Text = "on est là pour fausse piste",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = System.DateTime.Now.AddSeconds(10),
        };

        identifier = AndroidNotificationCenter.SendNotification(notification, "default_channel");
    }

    public void NotificationOnPush(string notifText)
    {
        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Notif On Push",
            Text = notifText,
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = System.DateTime.Now,
        };

        identifier = AndroidNotificationCenter.SendNotification(notification, "default_channel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
