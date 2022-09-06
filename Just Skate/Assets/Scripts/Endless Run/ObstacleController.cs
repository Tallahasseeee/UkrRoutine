using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public Rigidbody2D RB;
    public float obstacleSpeed;
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();    
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector3(-1 * obstacleSpeed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("left"))
        {
            Destroy(this.gameObject);
        }
    }
}
