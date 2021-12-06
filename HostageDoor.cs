using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HostageDoor : MonoBehaviour
{
    //public Transform technician;
    public Transform soldier;
    public float range;
    public static float HostageFreeCount;
    public bool Free;
    public GameObject FloatingText;
    public GameObject heal;
    //public TextMeshProUGUI text;
    private Animator animator;
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        timer = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Free == false)
        {
            if (Vector2.Distance(soldier.position, transform.position) < range)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Free = true;
                    animator.enabled = true;
                    Instantiate(FloatingText, transform.position, Quaternion.identity);
                    Instantiate(heal, transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
                    GameManager.noOfCageInLevel--;
                    GameManager.totalHostageFree++;
                    //text.text = "X" + HostageFreeCount;
                    if (Timer.canIncreaseTime)
                    {
                        timer.currentTime += 30;
                    }
                }
            }
        }
    }
}
