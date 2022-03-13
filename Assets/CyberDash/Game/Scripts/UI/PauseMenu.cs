using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public Animator transition;
    public float transitionTIme = 1f;

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
        inventoryUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        inventoryUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        clickOnButtonSound();
        FindObjectOfType<AudioManager>().Stop("inGameTheme");
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("gates");
        StartCoroutine(LoadScene(0));
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

    // animation on scene loading
    IEnumerator LoadScene(int sceneIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTIme);
        SceneManager.LoadScene(sceneIndex);
    }
}
