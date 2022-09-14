using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DogHead : MonoBehaviour
{

    private void Start()
    {
        //GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }


    public void GameStart()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public void GameEnd()
    {

    }

    public void GameReset()
    {

    }


    private void Update()
    {
        if (DLGameManager.Instance.state == GameState.Running)
        {
            if (transform.position.y < -600)
            {
                Debug.Log(transform.position.y + "");
                DLGameManager.Instance.GameLost();
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bee")
        {
            StartCoroutine(EndGame());
        }
    }


    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        if(DLGameManager.Instance.state == GameState.Running)
        {
            DLGameManager.Instance.CaptureScreen();
        }
        DLGameManager.Instance.GameLost();
    }


}
