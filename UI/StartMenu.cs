using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string currentLevel;

    public GameObject ControlView;
    public GameObject OptionView;
    bool ControlViewOpen = false;
    bool OptionMenuOpen = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

    public void Play()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public void Controls()
    {
        ControlView.SetActive(true);
        ControlViewOpen = true;
    }

    public void home()
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
    public void Option()
    {
        OptionView.SetActive(true);
        OptionMenuOpen = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
