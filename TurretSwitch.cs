using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSwitch : MonoBehaviour
{
    public Transform technician;
    public float range;
    public Sprite OffSprite;
    public Sprite OnSprite;
    public static bool switchIsOn;
    public GameObject[] TurretToDisable;
    private SpriteRenderer SR;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        SR.sprite = OnSprite;
    }

    private void Update()
    {
        if (Vector2.Distance(technician.position, transform.position) < range && Input.GetKeyDown(KeyCode.E))
        {
            ChangeSprite();
        }
    }
    void ChangeSprite()
    {
        SR.sprite = OffSprite;
        foreach (GameObject turret in TurretToDisable)
        {
            turret.GetComponentInChildren<CCTV>().Disabled = true;
        }
    }
}
