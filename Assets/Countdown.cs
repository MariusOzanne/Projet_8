using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Countdown : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI timeEndText;
    [SerializeField] float remainingTime;
    void Update()
    {
        if (remainingTime < 11)
        {
            countdownText.color = Color.red;
        }

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (remainingTime <= 1)
        {
            if (gameManager != null)
            {
                timeEndText.gameObject.SetActive(true);
                gameManager.RelancerScene();
            }
        }
    }
}
