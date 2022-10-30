using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoaderManager : MonoBehaviour
{
    int levelsUnlcocked;
    public Button[] buttons;
    public static void Load(string sceneName)
    {
        SceneLoader.Load(sceneName);
    }
    public static void ProgressLoad(string sceneName)
    {
        SceneLoader.ProgressLoad(sceneName);
    }
    public static void ReloadLevel()
    {
        SceneLoader.ReloadLevel();
    }
    public static void LoadNextLevel()
    {
        SceneLoader.LoadNextLevel();
    }

    private void Start() {
        levelsUnlcocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < levelsUnlcocked; i++)
        {
            buttons[i].interactable = true;
        }
    }
}
