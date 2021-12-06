using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public GameObject Barrel;
    bool canSpawn;

    private void Start()
    {
        canSpawn = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PushableBox"))
        {
            if (canSpawn == false)
            {
                Invoke("Respawn", 3f);
                canSpawn = true;
            }
        }
    }
    void Respawn()
    {
        Instantiate(Barrel, transform.position, transform.rotation);
        canSpawn = false;
    }
}
