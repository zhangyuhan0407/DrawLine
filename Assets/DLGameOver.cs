using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" || collision.collider.tag == "Dog")
        {
            Debug.Log("OnCollisionEnter2D: " + gameObject.name);
            DLGameManager.Instance.GameLost();
        }    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Dog")
        {
            Debug.Log("OnTriggerEnter2D: " + gameObject.name);
            DLGameManager.Instance.GameLost();
        }
    }



}
