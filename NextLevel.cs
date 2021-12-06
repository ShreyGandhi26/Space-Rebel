using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string LevelName;

    private void Update()
    {
        if (GameManager.canChnageLevel == true)
        {
            GameManager.canChnageLevel = false;
            GameManager.timeLeft = Timer.timePaused;
            //print(GameManager.timeLeft);
            SceneManager.LoadScene(LevelName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SoldierCharacter>() != null)
        {
            GameManager.canChnageLevel = true;
        }
        // if (other.GetComponent<TechncianCharacter>() != null)
        // {
        //     Destroy(other.GetComponent<TechncianCharacter>().gameObject);
        // }
    }
}
