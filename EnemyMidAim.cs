using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMidAim : MonoBehaviour
{
    public GameObject Player;
    public float LookSpeed;
    public Transform firePos;
    public GameObject Bullet;
    public GameObject MuzzleFalsh;
    public Vector3 diffrence;
    public float timeBtwShots;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Head");
    }

    // Update is called once per frame
    void Update()
    {
        diffrence = (Player.transform.position - transform.position).normalized;
        diffrence.z = 0;
        var offset = firePos.transform.position - transform.position;
        offset.z = 0;
        var localOffset = transform.InverseTransformVector(offset);
        localOffset.x = 0;
        localOffset.z = 0;
        Debug.DrawLine(Vector3.zero, localOffset * transform.lossyScale.x, Color.yellow);
        var worldOffset = transform.TransformVector(localOffset) - transform.right * 5 * Mathf.Sign(diffrence.x);
        var weaponDir = (Player.transform.position - (transform.position + worldOffset)).normalized;
        var socketRotation = Quaternion.LookRotation(Vector3.forward, Mathf.Sign(diffrence.x) * Vector3.Cross(Vector3.forward, weaponDir));
        float rotZ = Mathf.Atan2(diffrence.y, diffrence.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, socketRotation, Time.deltaTime * LookSpeed);
        firePos.rotation = Quaternion.Euler(0, 0, rotZ);

        var rot = transform.rotation;
        const float z = 0.35f;

        if (GetComponentInParent<EnemyMid>().isFacingRight && transform.rotation.z < -z)
        {
            rot.z = -z;
            transform.rotation = rot;
        }
        else if (!GetComponentInParent<EnemyMid>().isFacingRight && transform.rotation.z > z)
        {
            rot.z = z;
            transform.rotation = rot;
        }

        if (diffrence.x > 0 && !GetComponentInParent<EnemyMid>().isFacingRight)
        {
            firePos.rotation = Quaternion.Euler(0, 0, 0);
            GetComponentInParent<EnemyMid>().flip();
        }
        else if (diffrence.x < 0 && GetComponentInParent<EnemyMid>().isFacingRight)
        {
            firePos.rotation = Quaternion.Euler(0, 180, 0);
            GetComponentInParent<EnemyMid>().flip();
        }

        if (canShoot)
        {
            StartCoroutine("Fire");
        }
    }

    IEnumerator Fire()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBtwShots);
        GameObject newBullet = Instantiate(Bullet, firePos.position, firePos.rotation);
        Instantiate(MuzzleFalsh, firePos.position, firePos.rotation);
        FindObjectOfType<AudioManager>().playSound("EnemyFire");
        canShoot = true;
    }
}
