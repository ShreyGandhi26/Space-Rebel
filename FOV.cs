using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    Collider2D[] PlayerInRadius;
    public LayerMask obstacleMask, PlayerMask;
    public List<GameObject> visiblePlayer = new List<GameObject>();

    void Update()
    {
        FindVisiblePlayer();
    }

    void FindVisiblePlayer()
    {
        PlayerInRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius);
        visiblePlayer.Clear();
        for (int i = 0; i < PlayerInRadius.Length; i++)
        {
            GameObject player = PlayerInRadius[i].gameObject;
            Vector2 dirTarget = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            if (Vector2.Angle(dirTarget, transform.right) < viewAngle / 2)
            {
                float distancePlayer = Vector2.Distance(transform.position, player.transform.position);
                if (!Physics2D.Raycast(transform.position, dirTarget, distancePlayer, obstacleMask))
                {
                    if (player.GetComponent<CharacterController>() != null)
                    {
                        visiblePlayer.Add(player);
                    }
                }
            }
        }
    }

    public Vector2 DirFromAngle(float angleDegree, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegree += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Cos(angleDegree * Mathf.Deg2Rad), Mathf.Sin(angleDegree * Mathf.Deg2Rad));
    }


}
