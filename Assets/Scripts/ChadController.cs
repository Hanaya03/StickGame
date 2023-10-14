using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChadController : MonoBehaviour
{
    private float attackCoolDown = 2f;
    private float jumpForce = 2f;
    private float coolDownEnd;
    private Collider2D[] colliders = new Collider2D[2];
    int numOfColliders;
    
    private Vector2 pos;
    private CircleCollider2D attackCollider;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackCollider = GetComponent<CircleCollider2D>();
        attackCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCollider.IsTouchingLayers(3) && Input.GetKey(KeyCode.Z) && isCool()){
            attack();
        }

        float horizontal = Input.GetAxis("Horizontal");
        if (rb != null)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        pos = transform.position;
        pos.x = pos.x + 3f * horizontal * Time.deltaTime;
        transform.position = pos;
    }

    void attack()
    {
        
        Debug.Log("Hit!");
        cooling();
        Debug.Log(numOfColliders);
        for (int i = 0; i < numOfColliders; i++)
        {
            Debug.Log(colliders[i].transform.parent);
        }
    }

    bool isCool()
    {
        return Time.time >= coolDownEnd;
    }

    void cooling()
    {
        coolDownEnd = Time.time + attackCoolDown;
    }

    void getTouchingColliders()
    {
        numOfColliders = attackCollider.OverlapCollider(new ContactFilter2D(), colliders);
    }
}
