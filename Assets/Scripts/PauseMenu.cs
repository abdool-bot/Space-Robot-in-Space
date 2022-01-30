using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public GameObject Menu, OptionsMenu;
    public static bool GameisPaused = false;
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        if(this.tag != "MainMenu")
        {
            Menu.SetActive(false);
        }     
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag != "MainMenu")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameisPaused)
                {
                    Return();
                }
                else
                {
                    Pause();
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        } 
    }

    public void Return()
    {
        Menu.SetActive(false);
        OptionsMenu.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Menu.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Menu.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    public void Level()
    {
        
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("MAINJ 1");
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void Level1()
    {
        SceneManager.LoadScene("MAINJ 1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("MAINJ 2");
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }
}

