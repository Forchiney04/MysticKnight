using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public GameObject notification;

    Text notificationText;
    Animator animator;

    private void Start()
    {
        notificationText = notification.GetComponent<Text>();
        animator = notification.GetComponent<Animator>();
    }

    public void ShowNotification(string text)
    {
        notificationText.text = text;
        animator.SetTrigger("ShowNotification");
    }
}
