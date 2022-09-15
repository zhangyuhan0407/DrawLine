using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveDogController : MonoBehaviour
{

    public static SaveDogController instance;

    public Text labelGold;

    public Text labelLevel;


    private void Awake()
    {
        instance = this;
    }



    private void Start()
    {
        labelGold.text = DLPlayerManager.Instance.gold + "";
        labelLevel.text = DLPlayerManager.Instance.level + "/100";
    }


    public void UpdateUI()
    {
        labelGold.text = DLPlayerManager.Instance.gold + "";
        labelLevel.text = DLPlayerManager.Instance.level + "/100";
    }


    public void ResetGame()
    {
        //DLGameManager.Instance.GameReset();
        DLGameManager.Instance.GameLost();
    }

    public void GoToMainPage()
    {
        SceneManager.LoadScene("Scenes/MainScene");
    }

}
