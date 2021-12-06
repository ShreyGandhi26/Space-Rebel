using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public string currentLevel;
    public GameObject pauseMenu;
    public GameObject pauseImage;
    public GameObject pauseButton;
    public GameObject ControlView;
    public GameObject OptionView;
    bool ControlViewOpen = false;
    bool PauseMenuOpen = false;
    bool OptionMenuOpen = false;

    private void Start()
    {
        currentLevel = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenuOpen)
            {
                Resume();
            }
            else
            {
                pause();
            }
        }

        // if (PauseMenuOpen == false)
        // {
        //     //Cursor.visible = false;
        //     if (Input.GetKeyDown(KeyCode.Escape))
        //     {
        //         pause();
        //     }
        // }
        // if (PauseMenuOpen == true)
        // {
        //     //Cursor.visible = true;
        //     if (Input.GetKeyDown(KeyCode.Escape))
        //     {
        //         Resume();
        //     }
        // }

        if (ControlViewOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ControlView.SetActive(false);
                ControlViewOpen = false;
            }
        }
        if (OptionMenuOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OptionView.SetActive(false);
                OptionMenuOpen = false;
            }
        }
    }

    public void pause()
    {
        pauseMenu.SetActive(true);
        //pauseButton.GetComponent<Image>().enabled = false;
        //pauseImage.GetComponent<Image>().enabled = false;
        FindObjectOfType<SoldierCharacter>().GetComponentInChildren<Weapon>().canShoot = false;
        PauseMenuOpen = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        FindObjectOfType<SoldierCharacter>().GetComponentInChildren<Weapon>().canShoot = true;
        //pauseButton.GetComponent<Image>().enabled = true;
        //pauseImage.GetComponent<Image>().enabled = true;
        PauseMenuOpen = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        pauseButton.GetComponent<Image>().enabled = true;
        pauseImage.GetComponent<Image>().enabled = true;
        SceneManager.LoadScene(currentLevel);
    }

    public void Controls()
    {
        ControlView.SetActive(true);
        ControlViewOpen = true;
    }

    public void Option()
    {
        OptionView.SetActive(true);
        OptionMenuOpen = true;
    }

    public void Back()
    {
        if (ControlViewOpen == true)
        {
            ControlView.SetActive(false);
            ControlViewOpen = false;
        }
        if (OptionMenuOpen == true)
        {
            OptionView.SetActive(false);
            OptionMenuOpen = false;
        }
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
