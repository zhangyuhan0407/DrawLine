using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLPlayerManager : MonoBehaviour
{
    public static DLPlayerManager Instance;

    public int gold;

    public int level;

    bool isModeDebug;

    private void Awake()
    {
        Instance = this;
        isModeDebug = true;;
        gold = 0;
        level = 0;
    }

    public void Create()
    {
        gold = 0;
        level = 3;
        Save();
    }


    public void Save()
    {
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("Level", level);
    }

    public void Load()
    {
        gold = PlayerPrefs.GetInt("Gold");
        level = PlayerPrefs.GetInt("Level");
    }

    public void Clear()
    {
        gold = 0;
        level = 0;

        Save();
    }


}
