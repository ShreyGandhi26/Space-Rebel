using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIndicatorEnemyMild : MonoBehaviour
{
    public EnemyMid Character;
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
