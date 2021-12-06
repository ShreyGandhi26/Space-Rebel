using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDialogue : MonoBehaviour
{
    public GameObject ToPlay;
    public GameObject ToDialogueManager;
    private bool isPlaying = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SoldierCharacter>() != null)
        {
            if (isPlaying == false)
            {
                ToPlay.GetComponent<DialogueManager>().canTalk = true;
                isPlaying = true;
            }
            //gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<SoldierCharacter>() != null)
        {
            ToPlay.GetComponent<DialogueManager>().canTalk = false;
            Invoke("remove", 2);
        }
    }
    void remove()
    {
        gameObject.SetActive(false);
        ToPlay.SetActive(false);
        ToDialogueManager.SetActive(false);
    }
}
