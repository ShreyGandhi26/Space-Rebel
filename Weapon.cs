//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    //public float offset;
    public GameObject bulletPrefab;
    public GameObject Muzzleflash;
    public Vector3 diffrence;
    public bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        // if (canShoot)
        // {
        diffrence = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        diffrence.z = 0;
        var offset = firePoint.transform.position - transform.position;
        offset.z = 0;
        var localOffset = transform.InverseTransformVector(offset);
        localOffset.x = 0;
        localOffset.z = 0;
        Debug.DrawLine(Vector3.zero, localOffset * transform.lossyScale.x, Color.yellow);
        var worldOffset = transform.TransformVector(localOffset) - transform.right * 5 * Mathf.Sign(diffrence.x);
        var weaponDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - (transform.position + worldOffset)).normalized;
        var socketRotation = Quaternion.LookRotation(Vector3.forward, Mathf.Sign(diffrence.x) * Vector3.Cross(Vector3.forward, weaponDir));
        float rotZ = Mathf.Atan2(diffrence.y, diffrence.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, socketRotation, Time.deltaTime * 22);
        //transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        firePoint.rotation = Quaternion.Euler(0, 0, rotZ);


        var rot = transform.rotation;
        const float z = 0.35f;


        if (GetComponentInParent<PushableObjects>().isLiftingBox == false)
        {
            if (Input.GetButtonDown("Action") && canShoot == true)
            {
                shoot();
            }
        }

        if (GetComponentInParent<CharacterController>().FacingRight && transform.rotation.z < -z)
        {
            rot.z = -z;
            transform.rotation = rot;
        }
        else if (!GetComponentInParent<CharacterController>().FacingRight && transform.rotation.z > z)
        {
            rot.z = z;
            transform.rotation = rot;
        }

        if (diffrence.x > 0 && !GetComponentInParent<CharacterController>().FacingRight)
        {
            firePoint.rotation = Quaternion.Euler(0, 0, 0);
            GetComponentInParent<CharacterController>().Flip();
        }
        else if (diffrence.x < 0 && GetComponentInParent<CharacterController>().FacingRight)
        {
            firePoint.rotation = Quaternion.Euler(0, 180, 0);
            GetComponentInParent<CharacterController>().Flip();
        }
        //}
    }

    void shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Instantiate(Muzzleflash, firePoint.position, firePoint.rotation);
        FindObjectOfType<AudioManager>().playSound("Fire");
    }

}
