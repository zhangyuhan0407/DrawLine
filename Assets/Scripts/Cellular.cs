using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cellular : MonoBehaviour
{

    public Transform spawnPos;
    public Bee beePre;
    public float launchForce = 13500f;
    public string dogTag = "Dog";

    public int beesCount = 5;
    public float interval = 3;


    private void Start()
    {
        //GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
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
    }


}

