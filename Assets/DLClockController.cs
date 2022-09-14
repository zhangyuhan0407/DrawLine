using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DLClockController : MonoBehaviour
{

    public static DLClockController Instance;
    
    public int time;

    public Text label;

    public bool isPaused;

    private void Awake()
    {
        Instance = this;
        time = 10;
    }


    public void GameStart()
    {
        StartCoroutine(Countdown());
    }


    public void GameEnd()
    {
        StopCoroutine(Countdown());
        //gameObject.SetActive(false);
    }


    public void GameReset()
    {
        time = 10;
    }


    public void Clear()
    {

    }


    public void Play()
    {
        isPaused = false;
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Stop()
    {
        StopCoroutine(Countdown());
    }



    IEnumerator Countdown()
    {
        if (isPaused)
        {
            yield return null;
        }

        while (time-- > 0)
        {
            label.text = "" + time;
            yield return new WaitForSeconds(1);
        }
        //StartCoroutine(EndGame());

        DLGameManager.Instance.GameWin();
    }

    
}
