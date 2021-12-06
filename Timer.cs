using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public float currentTime;
    public bool startTimer;
    public TextMeshProUGUI timeText;
    public int startMin;
    bool canPlaySound;
    public static float timePaused;
    public static bool canIncreaseTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMin * 60;
        canIncreaseTime = true;
        canPlaySound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer == true)
        {
            currentTime = currentTime - Time.deltaTime;
            if (currentTime <= 15)
            {
                timeText.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1f));
                //canPlaySound = true;
            }
            if (currentTime <= 0)
            {
                FindObjectOfType<DeployMissile>().startFiring = true;
                timeText.color = Color.red;
                canIncreaseTime = false;
                startTimer = false;
            }
            if (canPlaySound == false && currentTime < 15)
            {
                FindObjectOfType<AudioManager>().playSound("Alarm");
                canPlaySound = true;
            }
            if (currentTime >= 15)
            {
                FindObjectOfType<AudioManager>().StopSound("Alarm");
                canPlaySound = false;
                timeText.color = Color.white;
            }
        }
        timePaused = currentTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        string timePlayingStr = time.ToString("mm':'ss");
        timeText.text = timePlayingStr;

    }
}
