using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cellular : MonoBehaviour
{

    public Transform spawnPos;
    public Bee beePre;
    public float launchForce = 13500f;
    public string dogTag = "Dog";

    public int beesCount = 1;
    public float interval = 3;


    private void Start()
    {
        //GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        beesCount = 5;
        
        if(transform.position.x > 0)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            spawnPos.localPosition = new Vector3(21.5f, 0f, 0);
        }
    }
    

    IEnumerator SpawnBees()
    {
        int[] tmp = new int[beesCount];
        float sum = 0;
        for (int i = 0; i < beesCount; i++)
        {
            tmp[i] = Random.Range(1, 10);
            sum += tmp[i];
        }
        float timer = 0;
        int index = 0;
        while (index < beesCount)
        {
            timer += Time.deltaTime;
            if (timer >= (interval * (tmp[index] / sum)))
            {
                timer = 0;
                index++;
                Bee bee = GameObject.Instantiate(beePre, spawnPos.position, Quaternion.identity, transform);
                bee.dogTag = dogTag;
                Rigidbody2D beeRig = bee.GetComponent<Rigidbody2D>();

                beeRig.AddForce((Vector3.left + Vector3.up * Random.Range(-1f, 1f)).normalized * launchForce);
            }
            yield return null;
        }
        yield return null;
    }


    public void GameStart()
    {
        StartCoroutine(SpawnBees());

        //GetComponent<Rigidbody2D>().posi
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Line")
        {
            StartCoroutine(EndGame());
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }


    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3);

        if (DLGameManager.Instance.state == GameState.Running)
        {
            DLGameManager.Instance.CaptureScreen();
        }

        DLGameManager.Instance.GameLost();
    }

}

