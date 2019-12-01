using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotificationSamples;
using System;

public class NotifScript : MonoBehaviour
{
    [SerializeField]
    private GameNotificationsManager notificationsManager;

    private void StartNotification()
    {
        GameNotificationChannel channel = new GameNotificationChannel("Fausse piste",
            "putain c'est fausse piste ou l'ile au maléfice ?!",
            "dites le moi plz");
        notificationsManager.Initialize(channel);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartNotification();
    }

    public void ShowNotificationAfterDelay(int sec)
    {
        ShowNotificationAfterDelay("Fausse piste", "putain c'est fausse piste ou l'ile au maléfice ?!",
            DateTime.Now.AddSeconds(sec));
        Debug.Log("notification envoyé !!");
    }

    private void ShowNotificationAfterDelay(string title, string body, DateTime time)
    {
        IGameNotification createNotification = notificationsManager.CreateNotification();
        if(createNotification != null)
        {
            createNotification.Title = title;
            createNotification.Body = body;
            createNotification.DeliveryTime = time;

            //je le fais à contre coeur
            var notificationToDisplay = notificationsManager.ScheduleNotification(createNotification);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
