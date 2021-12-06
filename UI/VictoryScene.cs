using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryScene : MonoBehaviour
{
    public string ToLoadScene;
    public TextMeshProUGUI score;
    public TextMeshProUGUI HostageFree;
    public TextMeshProUGUI EnemiesKilled;
    public float ScoreCounter;
    bool counted;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        counted = false;
        //FindObjectOfType<AudioManager>().StopSound("Alarm");
    }

    // Update is called once per frame
    void Update()
    {
        var name = SceneManager.GetActiveScene().name;

        GameManager.timeLeft = GameManager.timeLeft - Time.deltaTime * 50;
        if (GameManager.timeLeft <= 0)
        {
            GameManager.timeLeft = 0;
        }
        else
        {
            ScoreCounter = ScoreCounter + Time.deltaTime * 50;
            FindObjectOfType<AudioManager>().playSound("ScoreCounter");
        }
        if (counted == false && name != "GameOver")
        {
            FindObjectOfType<AudioManager>().playSound("ScoreCounter");
            ScoreCounter += GameManager.totalHostageFree * 3;
            ScoreCounter += GameManager.EnemiesKilled * 2;
            counted = true;
        }

        HostageFree.text = "X " + GameManager.totalHostageFree;
        EnemiesKilled.text = "X " + GameManager.EnemiesKilled;
        score.text = "Your Score : " + (int)ScoreCounter * 100;
    }
    public void loadNext()
    {
        SceneManager.LoadScene(ToLoadScene);
        GameManager.EnemiesKilled = 0;
        GameManager.totalHostageFree = 0;
    }
    public void Menu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void replay()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }
}
