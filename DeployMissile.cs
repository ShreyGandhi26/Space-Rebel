using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployMissile : MonoBehaviour
{
    public GameObject MissilePrefab;
    public float respawnTime = 1f;
    public bool startFiring;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void SpawnMissiles()
    {
        GameObject missile = Instantiate(MissilePrefab) as GameObject;
        missile.transform.position = new Vector2(Random.Range(-screenBounds.x * 2, screenBounds.x * 2), Random.Range(-screenBounds.y, screenBounds.y));

    }

    IEnumerator missileWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnMissiles();
        }
    }

    private void Update()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        if (startFiring)
        {
            startFiring = false;
            StartCoroutine(missileWave());
        }
    }

}
