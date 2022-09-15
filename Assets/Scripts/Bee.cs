using System.Collections;
using UnityEngine;

public class Bee : MonoBehaviour
{
    
    [HideInInspector]
    public string dogTag = "Dog";
    [HideInInspector]
    public DogHead dog;
    public float speed = 5;
    private Rigidbody2D rig;
    public float impactForce = 1000000;

    public bool isAI;


    private void Awake()
    {
        dog = GameObject.FindWithTag(dogTag).GetComponent<DogHead>();
        rig = GetComponent<Rigidbody2D>();

        StartCoroutine(Move());
        isAI = true;
    }


    public void GameStart()
    {
        
    }

    public void GameEnd()
    {
        rig.velocity = Vector2.zero;
    }

    public void GameReset()
    {

    }

    

    IEnumerator Move()
    {
        Vector3 dir;
        yield return new WaitForFixedUpdate();
        // 出生减速阶段
        while (rig.velocity.magnitude > 1.25f)
        {
            yield return new WaitForFixedUpdate();
        }
        rig.drag = 0;

        while (isAI)
        {
            // 转向
            dir = dog.transform.position - transform.position;
            transform.right = -dir;

            // 移动
            rig.AddForce(6 * dir);
            rig.velocity = Vector3.ClampMagnitude(rig.velocity, 3);
            yield return new WaitForFixedUpdate();
        }

        //rig.velocity = Vector2.zero;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        string tag = collision.collider.tag;

        if (tag == "Ground" || tag == "Dog")
        {
            GoBack(1000);
        }

        if (tag == "Line")
        {
            GoBack(500);
        }

        if (tag == "Bee")
        {
            GoBack(50);
        }


        if (collision.collider.GetComponent<Rigidbody2D>() != null)
        {
            //GoBack();

            //Vector3 normal = collision.contacts[0].normal;
            //Vector3 dir = -transform.right;
            //Vector3 v1 = Vector3.Project(dir, normal);
            //Vector3 tangent = dir - v1;
            //Vector3 force = Vector3.ClampMagnitude(tangent * impactForce, 100);

            //rig.AddForce(tangent * impactForce);

            //Debug.Log("AddForce: " + tangent * impactForce);
            ////Debug.Log("Rig: " + rig.gameObject.name);
        }

    }


    public Vector3 RotateVector(Vector3 vector, Vector3 axis, float angle)
    {
        Vector3 dir = Quaternion.AngleAxis(angle, axis) * vector;
        return dir;
    }


    private void GoBack(float force)
    {
        Vector2 dir = new Vector2(-rig.velocity.x, -rig.velocity.y);
        Vector2 dir2 = Vector2.ClampMagnitude(dir * 10000, force);

        //rig.velocity = dir2;
        rig.AddForce(dir2);
    }


    public void Rerotate()
    {
        if (dog.transform.position.y < transform.position.y)
        {
            rig.velocity = new Vector2(rig.velocity.x + 5, rig.velocity.y - 5);
        }
        else
        {
            rig.velocity = new Vector2(rig.velocity.x + 5, rig.velocity.y + 5);
        }
    }


    private void HandleAI()
    {
        isAI = true;
    }


}
