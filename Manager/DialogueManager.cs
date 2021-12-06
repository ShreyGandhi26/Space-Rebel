using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public float TypingSpeed = .2f;
    public bool SoldierSpeakingFirst;

    public GameObject soldier;
    //public GameObject technician;

    public TextMeshProUGUI SoldierText;
    //public TextMeshProUGUI TechnicianText;

    //public GameObject SoldierContinue;
    //public GameObject TechnicianContinue;

    public Animator soldierAnimator;
    public Animator soldierConnectorAnimator;
    // public Animator technicianAnimator;
    // public Animator technicianConnectorAnimator;

    public string[] SoldierDialogue;
    // public string[] TechnicianDialogue;

    private int soldierIndex = 0;
    // private int technicianIndex;
    private bool dialogueStarted;

    private float speechBubbleDelay = .6f;

    public bool canTalk;

    private void Start()
    {
        //StartCoroutine(StartDialogue());
    }

    private void Update()
    {
        if (canTalk)
        {
            StartCoroutine(StartDialogue());
            //soldier.GetComponent<SoldierCharacter>().canMove = false;
            //soldier.GetComponentInChildren<Weapon>().canShoot = false;
            // technician.GetComponent<TechncianCharacter>().canMove = false;
            canTalk = false;
        }

        // if (SoldierContinue.activeSelf)
        // {
        //     if (Input.GetKeyDown(KeyCode.Return))
        //     {
        //         SoldierDialogueTrigger();
        //     }
        // }
        // if (TechnicianContinue.activeSelf)
        // {
        //     if (Input.GetKeyDown(KeyCode.Return))
        //     {
        //TechnicianDialogueTrigger();
        //     }
        // }
    }

    private IEnumerator StartDialogue()
    {
        if (SoldierSpeakingFirst)
        {
            soldierConnectorAnimator.SetTrigger("Open");
            soldierAnimator.SetTrigger("Open");

            yield return new WaitForSeconds(speechBubbleDelay);
            StartCoroutine(TypeSoldierDialogue());
        }
        // else
        // {
        //     technicianConnectorAnimator.SetTrigger("Open");
        //     technicianAnimator.SetTrigger("Open");

        //     yield return new WaitForSeconds(speechBubbleDelay);
        //     StartCoroutine(TypeTechnicianDialogue());
        // }
    }

    private IEnumerator TypeSoldierDialogue()
    {
        foreach (char letter in SoldierDialogue[soldierIndex].ToCharArray())
        {
            SoldierText.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }
        //SoldierContinue.SetActive(true);
    }
    // private IEnumerator TypeTechnicianDialogue()
    // {
    //     foreach (char letter in TechnicianDialogue[technicianIndex].ToCharArray())
    //     {
    //         TechnicianText.text += letter;
    //         yield return new WaitForSeconds(TypingSpeed);
    //     }
    //     TechnicianContinue.SetActive(true);
    // }

    public IEnumerator ContinueSoldierDialogue()
    {

        SoldierText.text = string.Empty;

        soldierConnectorAnimator.SetTrigger("Close");
        soldierAnimator.SetTrigger("Close");

        yield return new WaitForSeconds(speechBubbleDelay);

        SoldierText.text = string.Empty;

        soldierConnectorAnimator.SetTrigger("Open");
        soldierAnimator.SetTrigger("Open");

        yield return new WaitForSeconds(speechBubbleDelay);



        if (soldierIndex < SoldierDialogue.Length - 1)
        {
            soldierIndex++;
            SoldierText.text = string.Empty;
            StartCoroutine(TypeSoldierDialogue());
        }
    }

    // private IEnumerator ContinueTechnicianDialogue()
    // {
    //     TechnicianText.text = string.Empty;

    //     technicianAnimator.SetTrigger("Close");
    //     technicianConnectorAnimator.SetTrigger("Close");

    //     yield return new WaitForSeconds(speechBubbleDelay);

    //     TechnicianText.text = string.Empty;

    //     technicianAnimator.SetTrigger("Open");
    //     technicianConnectorAnimator.SetTrigger("Open");

    //     yield return new WaitForSeconds(speechBubbleDelay);

    //     if (technicianIndex < TechnicianDialogue.Length - 1)
    //     {
    //         if (dialogueStarted)
    //         {
    //             technicianIndex++;
    //         }
    //         else
    //         {
    //             dialogueStarted = true;
    //         }
    //         TechnicianText.text = string.Empty;
    //         StartCoroutine(TypeTechnicianDialogue());
    //     }
    // }

    public void SoldierDialogueTrigger()
    {
        // SoldierContinue.SetActive(false);

        if (soldierIndex >= SoldierDialogue.Length - 1)
        {
            SoldierText.text = string.Empty;
            soldierAnimator.SetTrigger("Close");
            soldierConnectorAnimator.SetTrigger("Close");
            canTalk = false;
            soldier.GetComponent<SoldierCharacter>().canMove = true;
            soldier.GetComponentInChildren<Weapon>().canShoot = true;
            // soldier.GetComponent<SoldierCharacter>().canMove = true;
            // technician.GetComponent<TechncianCharacter>().canMove = true;
        }
        else
        {
            StartCoroutine(ContinueSoldierDialogue());
        }
    }
    // public void TechnicianDialogueTrigger()
    // {
    //     SoldierContinue.SetActive(false);

    //     if (technicianIndex >= TechnicianDialogue.Length - 1)
    //     {
    //         SoldierText.text = string.Empty;
    //         soldierAnimator.SetTrigger("Close");
    //         soldierConnectorAnimator.SetTrigger("Close");
    //         soldier.GetComponent<SoldierCharacter>().canMove = true;
    //         technician.GetComponent<TechncianCharacter>().canMove = true;
    //     }
    //     StartCoroutine(ContinueTechnicianDialogue());
    // }


}
