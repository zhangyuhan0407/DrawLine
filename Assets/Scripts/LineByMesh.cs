using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineByMesh : LineBase
{
    
    public Color lineColor;
    public float lineThickness = 5;
    public Rigidbody2D rig;
    List<Vector2> points = new List<Vector2>();
    PolygonCollider2D pCollider;
    List<Vector2> cPahtPoint = new List<Vector2>();


    protected override void Start()
    {
        pCollider = GetComponent<PolygonCollider2D>();
        rig = GetComponent<Rigidbody2D>();
    }


    public override void InitLine()
    {
        points.Clear();
        rig.simulated = false;
        GetComponent<Transform>().localPosition = Vector3.zero;
        GetComponent<Transform>().rotation = Quaternion.identity;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        SetAllDirty();
    }


    public override void AddPoint(Vector3 point)
    {
        points.Add(point);
        SetAllDirty();
    }


    public override void ClearPoint()
    {
        points.Clear();
    }


    protected override void OnPopulateMesh(VertexHelper vh)
    {
        pCollider.enabled = false;
        vh.Clear();
        pCollider.pathCount = points.Count * 2;
        for (int i = 0; i < points.Count - 2; i++)
        {
            Vector2 curLeft, curRight, nextLeft, nextRight;
            Vector2 dir = points[i + 1] - points[i];
            curLeft = points[i] + RotateVector(dir, Vector3.forward, 90).normalized * lineThickness;
            curRight = points[i] + RotateVector(dir, Vector3.forward, -90).normalized * lineThickness;
            nextLeft = points[i + 1] + RotateVector(dir, Vector3.forward, 90).normalized * lineThickness;
            nextRight = points[i + 1] + RotateVector(dir, Vector3.forward, -90).normalized * lineThickness;

            vh.AddVert(curLeft, lineColor, Vector2.zero);
            vh.AddVert(curRight, lineColor, Vector2.zero);
            vh.AddVert(nextLeft, lineColor, Vector2.zero);
            vh.AddVert(nextRight, lineColor, Vector2.zero);

            // 拼接碰撞器
            cPahtPoint.Clear();
            cPahtPoint.Add(curLeft);
            cPahtPoint.Add(curRight);
            cPahtPoint.Add(nextLeft);
            pCollider.SetPath(i, cPahtPoint);
            cPahtPoint.Clear();
            cPahtPoint.Add(curRight);
            cPahtPoint.Add(nextLeft);
            cPahtPoint.Add(nextRight);
            pCollider.SetPath(i + points.Count, cPahtPoint);
        }
        for (int i = 0; i < vh.currentVertCount - 2; i++)
        {
            vh.AddTriangle(i, i + 1, i + 2);
        }
        pCollider.enabled = true;
    }


    public Vector2 RotateVector(Vector2 vector, Vector3 axis, float angle)
    {
        Vector2 dir = Quaternion.AngleAxis(angle, axis) * vector;
        return dir;
    }


    public override void GameStart()
    {
        rig.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rig.simulated = true;
    }


}
