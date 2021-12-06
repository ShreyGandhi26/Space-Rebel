using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageFree : MonoBehaviour
{
    public GameObject portalEffect;
    public Animator hostage;
    public HostageDoor door;
    private bool doOnce;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        doOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (door.Free)
        {
            hostage.SetBool("Free", true);
            Invoke("teleport", .2f);
            currentTime += Time.deltaTime;

        }
        if (currentTime > 2)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 3);
        }
        if (transform.localScale.x <= .1f)
        {
            Destroy(gameObject);
        }
    }
    private void teleport()
    {
        if (doOnce == true)
        {
            Instantiate(portalEffect, transform.position + new Vector3(0, .1f, 0), Quaternion.identity);
            FindObjectOfType<AudioManager>().playSound("Thanks");
            doOnce = false;
        }
    }
}
