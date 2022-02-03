using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;
    
    public GameObject optionsmenu;
    private static bool gameisPaused = false;
    private Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        if(this.tag != "MainMenu")
        {
            Menu.SetActive(false);
        }   
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRate + "hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        optionsmenu = this.transform.Find("OptionsMenu").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag != "MainMenu")
        {
            if (!optionsmenu.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (gameisPaused)
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
    }

    public void Return()
    {
        Menu.SetActive(false);
        optionsmenu.SetActive(false);
        Time.timeScale = 1f;
        gameisPaused = false;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Menu.SetActive(true);
        Time.timeScale = 0f;
        gameisPaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Menu.SetActive(false);
        Time.timeScale = 1f;
        gameisPaused = false;
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
        gameisPaused = false;
    }

    public void EndGame()
    {
        Debug.Log("end game");
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

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("musicVolume", volume);
        
    }

    public void SetSFXVolume(float volume)
    {
        sfxMixer.SetFloat("sfxVolume", volume);
        
    }
}

