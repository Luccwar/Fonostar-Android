using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirection : MonoBehaviour
{
    public float moveSpeed;

    Rigidbody2D rb;

    Player target;
    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>();
        if(target!=null)
        {
            moveDirection = (target.transform.position - transform.position).normalized * moveSpeed * GameController.instance.GameSpeed;
            rb.velocity = new Vector2 (moveDirection.x * GameController.instance.GameSpeed, moveDirection.y * GameController.instance.GameSpeed);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        target = GameObject.FindObjectOfType<Player>();
        rb.velocity = new Vector2 (moveDirection.x * GameController.instance.GameSpeed, moveDirection.y * GameController.instance.GameSpeed);
        transform.Rotate (0,0,500*Time.deltaTime*GameController.instance.GameSpeed);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Player")
        {
            if(!col.isTrigger)
            {
                Destroy(this.gameObject);
            }
        }

        else if(col.gameObject.tag == "Shield")
        {
            if(!col.isTrigger)
            {
                Destroy(this.gameObject);
            }
        }
        
    }

    private void OnBecameInvisible() {
        Destroy(this.gameObject);
    }
}
