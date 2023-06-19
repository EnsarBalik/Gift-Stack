using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int Level
    {
        get => _level;
        set
        {
            _level = value;
            PlayerPrefs.SetInt("level", _level);
        }
    }

    private static int _level;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        _level = PlayerPrefs.GetInt("level", 1);
    }
}
