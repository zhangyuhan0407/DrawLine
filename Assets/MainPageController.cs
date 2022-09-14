using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MainPageController : MonoBehaviour
{
    public Text labelGold;


    private void Start()
    {
        labelGold.text = DLPlayerManager.Instance.gold + "";
    }


    public void LoadSaveDog()
    {
        SceneManager.LoadScene("Scenes/SaveDog");
    }


}
