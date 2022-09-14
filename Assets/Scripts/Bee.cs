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
    public float impactForce = 10000;


    private void Awake()
    {
        dog = GameObject.FindWithTag(dogTag).GetComponent<DogHead>();
        rig = GetComponent<Rigidbody2D>();

        StartCoroutine(Move());
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

        while (true)
        {
            // 转向
            dir = dog.transform.position - transform.position;
            transform.right = -dir;

            // 移动
            rig.AddForce(2 * dir);
            rig.velocity = Vector3.ClampMagnitude(rig.velocity, 3);
            yield return new WaitForFixedUpdate();
        }

        //rig.velocity = Vector2.zero;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Rigidbody2D>() != null)
        {
            Vector3 normal = collision.contacts[0].normal;
            Vector3 dir = -transform.right;
            Vector3 v1 = Vector3.Project(dir, normal);
            Vector3 tangent = dir - v1;
            rig.AddForce(tangent * impactForce);
            //Debug.DrawLine(v1, dir);
        }
    }


    public Vector3 RotateVector(Vector3 vector, Vector3 axis, float angle)
    {
        Vector3 dir = Quaternion.AngleAxis(angle, axis) * vector;
        return dir;
    }


}
