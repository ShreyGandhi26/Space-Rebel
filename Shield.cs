using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float maxShieldTime;
    public float currentShieldTime;
    public bool isUsingShield;
    public bool canActivate;


    private void Start()
    {
        currentShieldTime = maxShieldTime;
        isUsingShield = false;
    }

    private void FixedUpdate()
    {
        if (currentShieldTime >= maxShieldTime)
        {
            canActivate = true;
            currentShieldTime = maxShieldTime;
        }

        if (currentShieldTime <= 0)
        {
            canActivate = false;
        }

        if (Input.GetMouseButton(1) && canActivate == true)
        {
            if (currentShieldTime >= 0)
            {
                isUsingShield = true;
                gameObject.GetComponentInParent<SoldierCharacter>().gameObject.GetComponentInChildren<Weapon>().canShoot = false;
                currentShieldTime -= Time.deltaTime;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            isUsingShield = false;
            gameObject.GetComponentInParent<SoldierCharacter>().gameObject.GetComponentInChildren<Weapon>().canShoot = true;
            currentShieldTime += Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
