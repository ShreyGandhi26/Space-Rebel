using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldStatus : MonoBehaviour
{
    public Shield shield;
    public Image ShieldFill;


    private void Start()
    {
        ShieldFill.fillAmount = shield.maxShieldTime / shield.maxShieldTime;
    }
    private void Update()
    {
        ShieldFill.fillAmount = shield.currentShieldTime / shield.maxShieldTime;
    }
}
