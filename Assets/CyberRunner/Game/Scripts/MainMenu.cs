using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTIme = 1f;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("mainMenuTheme");
    }

    public void PlayGame()
    {
        clickOnButtonSound();
        FindObjectOfType<AudioManager>().Play("gates");
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex +1 ));
    }

    public void QuitGame()
    {
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


