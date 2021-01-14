using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    Text timeUI;
    float startTime;
    float elapsedTime;
    bool startCounter;

    int minutes;
    int seconds;

    // Start is called before the first frame update
    void Start()
    {
        startCounter = false;

        timeUI = GetComponent<Text>();
    }

    public void StartTimeCounter()
    {
        startTime = Time.time;
        startCounter = true;
    }

    public void StopTimeCounter()
    {
        startCounter = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (startCounter)
        {
            elapsedTime = Time.time - startTime;

            minutes = (int)elapsedTime / 60;

            seconds = (int)elapsedTime % 60;

            timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }
}
