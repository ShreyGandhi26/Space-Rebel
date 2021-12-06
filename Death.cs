using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private string currentLevel;

    private void Awake()
    {
        currentLevel = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            SceneManager.LoadScene("GameOver");
        }

    }
}
