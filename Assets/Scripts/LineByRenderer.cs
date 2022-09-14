//using System.Collections.Generic;
//using UnityEngine;


//public class LineByRenderer : LineBase
//{
    
//    public Color lineColor;
//    public float lineThickness = 1;
//    LineRenderer line;
//    Rigidbody2D rig;
//    List<Vector3> points = new List<Vector3>();
//    PolygonCollider2D pCollider;
//    List<Vector2> pColliderPath = new List<Vector2>();


//    protected override void Start()
//    {
//        pCollider = GetComponent<PolygonCollider2D>();
//        rig = GetComponent<Rigidbody2D>();
//        line = GetComponent<LineRenderer>();
//        InitLine();
//    }


//    public override void InitLine()
//    {
//        points.Clear();
//        line.positionCount = 0;
//        line.SetPositions(points.ToArray());
//        rig.simulated = false;
//        GetComponent<Transform>().position = Vector3.zero;
//        GetComponent<Transform>().rotation = Quaternion.identity;
//        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
//        line.startColor = line.endColor = lineColor;
//        line.startWidth = line.endWidth = lineThickness;
//    }


//    public override void AddPoint(Vector3 point)
//    {
//        points.Add(point);
//        line.positionCount = points.Count;
//        line.SetPositions(points.ToArray());
//    }


//    public override void ClearPoint()
//    {
//        Debug.Log("ClearPoint");
//        points.Clear();
//    }


//    void CreateColliderLine()
//    {
//        pCollider.SetPath(0, GetColliderPath(points).ToArray());
//    }


//    //计算碰撞体轮廓
//    List<Vector2> GetColliderPath(List<Vector3> pointList3)
//    {
//        //碰撞体宽度
//        float colliderWidth = line.startWidth;
//        //Vector3转Vector2
//        pColliderPath.Clear();
//        for (int i = 0; i < pointList3.Count; i++)
//        {
//            pColliderPath.Add(pointList3[i]);
//        }
//        //碰撞体轮廓点位
//        List<Vector2> edgePointList = new List<Vector2>();
//        //以LineRenderer的点位为中心, 沿法线方向与法线反方向各偏移一定距离, 形成一个闭合且不交叉的折线
//        for (int j = 1; j < pColliderPath.Count; j++)
//        {
//            //当前点指向前一点的向量
//            Vector2 distanceVector = pColliderPath[j - 1] - pColliderPath[j];
//            //法线向量
//            Vector3 crossVector = Vector3.Cross(distanceVector, Vector3.forward);
//            //标准化, 单位向量
//            Vector2 offectVector = crossVector.normalized;
//            //沿法线方向与法线反方向各偏移一定距离
//            Vector2 up = pColliderPath[j - 1] + 0.5f * colliderWidth * offectVector;
//            Vector2 down = pColliderPath[j - 1] - 0.5f * colliderWidth * offectVector;
//            //分别加到List的首位和末尾, 保证List中的点位可以围成一个闭合且不交叉的折线
//            edgePointList.Insert(0, down);
//            edgePointList.Add(up);
//            //加入最后一点
//            if (j == pColliderPath.Count - 1)
//            {
//                up = pColliderPath[j] + 0.5f * colliderWidth * offectVector;
//                down = pColliderPath[j] - 0.5f * colliderWidth * offectVector;
//                edgePointList.Insert(0, down);
//                edgePointList.Add(up);
//            }
//        }
//        return edgePointList;
//    }


//    public override void StartGame()
//    {
//        // line.Simplify(0.01f);
//        CreateColliderLine();
//        rig.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
//        rig.simulated = true;
//    }


//}
