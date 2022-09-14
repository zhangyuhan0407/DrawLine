using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public enum GameState
{
    Begin,
    Running,
    End
}


public class DLGameManager : MonoBehaviour
{
    public static DLGameManager Instance;


    public GameObject panelWin;
    public GameObject panelLost;
    
    public GameObject PanelLevel;
    
    public GameState state;
    
    public Image img;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        panelWin = GameObject.Find("/Canvas/SaveDogWin/PanelWin");
        panelLost = GameObject.Find("/Canvas/SaveDogWin/PanelLost");
        //PanelLevel = GameObject.Find("/Canvas/SaveDogWin/Level" + 1);

        DLPlayerManager.Instance.Load();
        if (DLPlayerManager.Instance.level == 0)
        {
            DLPlayerManager.Instance.Create();
        }

        state = GameState.Begin;

        DLPlayerManager.Instance.level = 3;

        LoadCurrentLevel();

    }


    public void GameStart()
    {
        EnterState(GameState.Running);
    }


    public void GameReset()
    {
        EnterState(GameState.Begin);
        DLClockController.Instance.GameReset();
        DLPen.Instance.GameReset();
    }

    public void GameEnd()
    {
        EnterState(GameState.End);
    }


    public void GameWin()
    {
        if (state == GameState.End)
        {
            return;
        }

        CaptureScreen();

        Invoke("aaa", 1);
        
    }
    

    public void aaa()
    {
        EnterState(GameState.End);

        panelWin.SetActive(true);

        byte[] bytes = ConvertImageToByte("Assets/Resources/Screenshot.png");
        Sprite s = ConvertTextureToSprite(bytes);
        img.sprite = s;
        img.SetNativeSize();
        img.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        img.transform.localPosition = new Vector3(0, 75, 0);

    }

    

    public void GameLost()
    {
        if (state == GameState.End)
        {
            return;
        }

        EnterState(GameState.End);

        panelLost.SetActive(true);
    }

    
    public void UpdateUI()
    {
        if (state == GameState.End)
        {

        }
        else
        {
            panelWin.SetActive(false);
            panelLost.SetActive(false);
        }
    }

    
    public void EnterState(GameState s)
    {
        state = s;
        if (s == GameState.Begin)
        {

        }
        else if (s == GameState.Begin)
        {

        }
        else if (s == GameState.End)
        {
            DLPen.Instance.Clear();
            ClearCurrentLevel();
        }
    }



    /// <summary>
    /// Level
    /// </summary>


    public void LoadLevel(int level)
    {
        ClearCurrentLevel();

        Debug.Log("Level: " + level);

        string s = "Levels/Level" + level;
        GameObject l = Resources.Load(s) as GameObject;
        PanelLevel = Instantiate(l) as GameObject;

        GameObject parent = GameObject.Find("/Canvas/SaveDogWin");
        PanelLevel.transform.SetParent(parent.transform, false);
        PanelLevel.transform.SetSiblingIndex(1);

        state = GameState.Begin;

        UpdateUI();
    }
    

    public void LoadCurrentLevel()
    {
        LoadLevel(DLPlayerManager.Instance.level);
    }
    

    public void LoadNextLevel()
    {
        DLPlayerManager.Instance.level += 1;
        LoadCurrentLevel();
    }
    

    public void ClearCurrentLevel()
    {
        if (PanelLevel != null)
        {
            Destroy(PanelLevel);
            PanelLevel = null;
        }
    }


    public void CaptureScreen()
    {
        Debug.Log("CaptureScreen");
        if (File.Exists("Assets/Resources/Screenshot.png"))
        {
            Debug.Log("File Exists");
            File.Delete("Assets/Resources/Screenshot.png");
        }
        else
        {
            Debug.Log("File Not Exists");
        }
        
        ScreenCapture.CaptureScreenshot("Assets/Resources/Screenshot.png", 1);
    }


    //public Texture2D CaptureScreen()
    //{
        

    //    Rect r = new Rect(Vector2.zero, new Vector2(300, 300));
    //    Texture2D ret = new Texture2D((int)r.width, (int)r.height, TextureFormat.RGB24, false);
    //    ret.ReadPixels(r, 0, 0);
    //    ret.Apply();

    //    return ret;
    //}



    private Sprite ConvertTextureToSprite(byte[] bytes)
    {
        Texture2D ret = new Texture2D(1080, 1920);

        ret.LoadImage(bytes);
        Sprite s = Sprite.Create(ret, new Rect(0, 0, ret.width, ret.height), new Vector2(0.5f, 0.5f));
        return s;
    }



    private byte[] ConvertImageToByte(string s)
    {
        FileStream fs = new FileStream(s, FileMode.Open);

        byte[] ret = new byte[fs.Length];
        fs.Read(ret, 0, ret.Length);
        fs.Close();

        return ret;
    }


}
