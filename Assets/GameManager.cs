using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] PlayerController player;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] Hole hole;
    [SerializeField] AudioSource CelebrateAudio;
    private void Start()
    {
        gameOverPanel.SetActive(false);
    }
    private void Update()
    {
        if (hole.Entered && gameOverPanel.activeInHierarchy == false)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Finished! Shoot Count : " + player.ShootCount;
            passLevels();
            CelebrateAudio.Play();
            if(CelebrateAudio.isPlaying)
            {
                player.enabled = false;
            }
        }
    }
    public void passLevels(){
        int currentLevels = SceneManager.GetActiveScene().buildIndex;
        if (currentLevels >= PlayerPrefs.GetInt("levelsUnlocked", 1))
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevels + 1);
        }
    }

    public void BackToMainMenu()
    {
        SceneLoader.Load("MainMenu");
        resumeGame();
    }

    public void Replay()
    {
        SceneLoader.ReloadLevel();
        resumeGame();
    }

    public void PlayNext()
    {
        SceneLoader.LoadNextLevel();
        resumeGame();
    }

    public void pauseGame(){
        Time.timeScale = 0;
        Debug.Log("Game Paused");
    }

    public void resumeGame(){
        Time.timeScale = 1;
    }


}
