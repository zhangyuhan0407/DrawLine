using UnityEngine;
using UnityEngine.EventSystems;


public class DLPen : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static DLPen Instance;

    public enum LineType
    {
        Mesh,
        Render
    }

    public LineBase curline;
    Vector3 lastPoint = Vector3.zero;
    public bool inArea = true;
    float thresholdTwoPoints = 0.2f;
    
    public LineType lineType;


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        
        switch (lineType)
        {
            case LineType.Mesh:
                curline = GameObject.FindObjectOfType<LineByMesh>();
                thresholdTwoPoints = 55;
                break;
            //case LineType.Render:
            //    curline = GameObject.FindObjectOfType<LineByRenderer>();
            //    thresholdTwoPoints = 0.2f;
            //    break;
            default:
                break;
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {

        if(DLGameManager.Instance.state != GameState.Begin)
        {
            return;
        }

        curline.InitLine();
        lastPoint = Vector3.zero;
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (DLGameManager.Instance.state != GameState.Begin)
        {
            return;
        }

        Vector3 point =  Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)); ;
        if (lineType == LineType.Mesh)
        {
            point = transform.InverseTransformPoint(point);
        }
    
        if (lastPoint == Vector3.zero)
        {
            lastPoint = point;
        }

        if (curline == null )
        {
            return;
        }

        if (eventData.pointerEnter == null)
        {
            if (inArea == true)
            {
                inArea = false;
            }
        }
        else if (eventData.pointerEnter.name.Contains("Level") || eventData.pointerEnter.name.Contains("Line_Mesh"))
        {
            if (inArea == false)
            {
                if (Vector3.Distance(lastPoint, point) > thresholdTwoPoints)
                {
                    return;
                }
                else
                {
                    inArea = true;
                }
            }
            curline.AddPoint(point);
            lastPoint = point;
        }
        else
        {
            if (inArea == true)
            {
                inArea = false;
            }
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (DLGameManager.Instance.state != GameState.Begin)
        {
            return;
        }
        
        curline.StartGame();
        GameObject.FindObjectOfType<Canvas>().BroadcastMessage("GameStart");
        //DLGameManager.Instance.GameStart();
    }


    public void GameReset()
    {

    }


    public void Clear()
    {
        curline.ClearPoint();
        curline.SetAllDirty();
    }



    private void OnDestroy()
    {
       curline = null;
    }


}

