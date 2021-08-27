using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationTrigger : MonoBehaviour
{
    public GameObject notification;
    public string textToDisplay;

    Text notificationText;
    Animator animator;

    private void Start()
    {
        notificationText = notification.GetComponent<Text>();
        animator = notification.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            notificationText.text = textToDisplay;
            animator.SetTrigger("ShowNotification");
        }
    }
}
