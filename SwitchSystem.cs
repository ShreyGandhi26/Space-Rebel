using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSystem : MonoBehaviour
{
    public Transform technician;
    public float range;
    public Sprite OnSprite;
    public Sprite OffSprite;
    public static bool canTurnSwitch;
    public GameObject[] DoorToOpen;
    public GameObject alertMessage;
    public AudioSource audioS;
    private SpriteRenderer SR;

    // Start is called before the first frame update
    void Start()
    {
        canTurnSwitch = false;
        SR = GetComponent<SpriteRenderer>();
        SR.sprite = OffSprite;
    }

    private void Update()
    {
        if (Vector2.Distance(technician.position, transform.position) < range && Input.GetKeyDown(KeyCode.F))
        {
            if (canTurnSwitch)
                ChangeSprite();
            if (canTurnSwitch == false)
            {
                alertMessage.SetActive(true);
                audioS.Play();
                Invoke("Disable", 2f);
            }
        }
    }

    void ChangeSprite()
    {
        SR.sprite = OnSprite;
        //canTurnSwitch = false;
        foreach (GameObject door in DoorToOpen)
        {
            door.GetComponentInChildren<Animator>().enabled = true;
            door.GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
    }

    void Disable()
    {
        alertMessage.SetActive(false);
    }

}
