using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private CharacterController controller;
    public Transform AttackPos;
    public float Attackrange;
    public float damage;
    public LayerMask AttackingLayer;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            controller.animator.SetTrigger("Fire");
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(AttackPos.position, Attackrange, AttackingLayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyBasic>().TakeDamage(damage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, Attackrange);
    }
}
