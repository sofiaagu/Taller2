using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    //#region sonidos
    //[SerializeField]
    //private AudioClip stop;
    //[SerializeField]
    //private AudioSource respuestaAudio;
    ////Reloj objReloj;
    //#endregion

    public TextMeshProUGUI timerMinutes;
    public TextMeshProUGUI timerSeconds;
    public TextMeshProUGUI timerSeconds100;

    private float startTime;
    public float stopTime;
    private float timerTime;
    private bool isRunning = false;

    // Use this for initialization
    void Start()
    {
        TimerStart();
        //TimerReset();
    }

    public void TimerStart()
    {
        if (!isRunning)
        {
            print("START");
            isRunning = true;
            startTime = Time.time;
        }
    }

    public void TimerStop()
    {
        if (isRunning)
        {
            print("STOP");
            isRunning = false;
            stopTime = timerTime;
            Debug.Log(stopTime.ToString());
            ///
            //if (stopTime >= 30)
            //{
            //    respuestaAudio.clip = stop;
            //    respuestaAudio.Play();
            //}

        }
    }

    public void TimerReset()
    {
        print("RESET");
        stopTime = 0;
        isRunning = false;
        timerMinutes.text = timerSeconds.text = timerSeconds100.text = "00";
    }

    // Update is called once per frame
    void Update()
    {
        timerTime = stopTime + (Time.time - startTime);

        if (isRunning)
        {
            string formatted = FormatTime(timerTime);
            string[] parts = formatted.Split(':');
            timerMinutes.text = parts[0];
            timerSeconds.text = parts[1];
            timerSeconds100.text = parts[2];
        }
    }


    public static string FormatTime(float t)
    {
        int minutesInt = (int)t / 60;
        int secondsInt = (int)t % 60;
        int seconds100Int = (int)((t - (minutesInt * 60 + secondsInt)) * 100);

        return
            (minutesInt < 10 ? "0" + minutesInt : minutesInt.ToString()) + ":" +
            (secondsInt < 10 ? "0" + secondsInt : secondsInt.ToString()) + ":" +
            (seconds100Int < 10 ? "0" + seconds100Int : seconds100Int.ToString());
    }
    public void ResetTimer()
    {
        stopTime = 0f;
        // Si tienes otras variables en tu Timer, resetéalas aquí también
        // Por ejemplo:
        // currentTime = 0f;
        // isRunning = true;
    }
}
