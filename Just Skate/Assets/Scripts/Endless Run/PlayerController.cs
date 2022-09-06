using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 30;
    public float playerSpeed;
    public Rigidbody2D RB;
    private Vector2 playerDirection;
    public bool isGrounded;
    // Start is called before the first frame update
    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //float directionY = Input.GetAxisRaw("Vertical");
        //playerDirection = new Vector2(0, directionY).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                RB.AddForce(Vector2.up * jumpForce);
                isGrounded = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }
   
    private void FixedUpdate()
    {
        //if (OnGround())
        //{
            //RB.AddForce(new Vector2(0, playerDirection.y * force), ForceMode2D.Impulse);
       // }
    }
}
