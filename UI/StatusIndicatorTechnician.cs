using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIndicatorTechnician : MonoBehaviour
{
    public TechncianCharacter techncian;
    public Image healthFill;


    private void Start()
    {
        healthFill.fillAmount = techncian.Maxhealth / 100;
    }
    private void Update()
    {
        healthFill.fillAmount = techncian.CurrentHealth / 100;
    }
}
