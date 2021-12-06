using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObjects : MonoBehaviour
{
    public float distance;
    public Vector3 offset;
    public Transform boxHolder;
    public Transform grabDetect;
    GameObject box;
    public bool isLiftingBox = false;
    public LayerMask toHitLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(grabDetect.position, Vector2.right, distance, toHitLayer);

        if (hit.collider != null && hit.collider.gameObject.tag == "PushableBox")
        {
            box = hit.collider.gameObject;

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isLiftingBox == false)
                {
                    box.transform.SetParent(boxHolder);
                    box.transform.position = boxHolder.position;
                    box.GetComponent<BoxCollider2D>().isTrigger = true;
                    box.GetComponent<Rigidbody2D>().isKinematic = true;
                    box.layer = LayerMask.NameToLayer("Soldier");
                    isLiftingBox = true;
                }
                else
                {
                    box.gameObject.transform.parent = null;
                    box.GetComponent<Rigidbody2D>().isKinematic = false;
                    box.GetComponent<BoxCollider2D>().isTrigger = false;
                    box.layer = LayerMask.NameToLayer("MovableBox");
                    isLiftingBox = false;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(grabDetect.position, (Vector2)grabDetect.position + Vector2.right * distance);
    }
}
