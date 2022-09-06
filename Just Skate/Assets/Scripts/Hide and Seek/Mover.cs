using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : Fighter
{

    public float moveSpeed;
    private Vector2 moveDirection;
    protected Vector3 moveDelta;
    private RaycastHit2D hit;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //ProcessInput();

    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = input;

        if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(1 * Mathf.Abs(transform.localScale.x), 1 * transform.localScale.y, 1 * transform.localScale.z);
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), 1 * transform.localScale.y, 1 * transform.localScale.z);
        }


        hit = Physics2D.BoxCast(boxCollider.transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime * moveSpeed), LayerMask.GetMask( "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime * moveSpeed, 0);
        }

        hit = Physics2D.BoxCast(boxCollider.transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime * moveSpeed), LayerMask.GetMask( "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime * moveSpeed, 0, 0);
        }
    }
}
