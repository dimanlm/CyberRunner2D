using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }

    public void Resume()
    {
        clickOnButtonSound();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        clickOnButtonSound();
        FindObjectOfType<AudioManager>().Stop("inGameTheme");
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("gates");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        clickOnButtonSound();
        Application.Quit();
    }

    public void clickOnButtonSound()
    {
        FindObjectOfType<AudioManager>().Play("click");
    }
}
