using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIndicatorEnemyBasic : MonoBehaviour
{
    public EnemyBasic Character;
    public Image healthFill;


    private void Start()
    {
        healthFill.fillAmount = Character.Maxhealth / Character.Maxhealth;
    }
    private void Update()
    {
        healthFill.fillAmount = Character.CurrentHealth / Character.Maxhealth;
    }
}
