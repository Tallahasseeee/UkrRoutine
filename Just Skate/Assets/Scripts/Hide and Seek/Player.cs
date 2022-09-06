using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class Player : Collidable
{
    public GameObject takeButtonGameObject;

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset defaultAnimation, movingAnimation;
    private string currentState;
    private string currentAnimation;
    
    public float rotationSpeed;
    public GameObject PointLight;
    public float moveSpeed;
    public Rigidbody2D RB;
    public Joystick joystick;
    private Vector2 moveDirection;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    public Vector3 dir;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        boxCollider = GetComponent<BoxCollider2D>();
        currentState = "Idle";
        currentAnimation = "";
        SetCharacterState(currentState);
    }

    // Update is called once per frame
    private void Update()
    {
        base.Update();
        //ProcessInput();

    }

    private void FixedUpdate()
    {
        moveDelta = Vector3.zero;
        moveDelta = ProcessInput();
        ManageTakeButton();
        if (moveDelta != Vector3.zero)
        {
            dir = moveDelta;
            currentState = "Moving";
            
        }
        else
        {
            currentState = "Idle";
        }
        SetCharacterState(currentState);

        PointLight.transform.rotation = Quaternion.RotateTowards(PointLight.transform.rotation, Quaternion.LookRotation(Vector3.forward, dir),rotationSpeed * Time.deltaTime);

        if (dir.x > 0)
        {
            transform.localScale = new Vector3(1 * Mathf.Abs(transform.localScale.x), 1 * transform.localScale.y, 1 * transform.localScale.z);
        }
        else if (dir.x < 0) {
           transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), 1 * transform.localScale.y, 1 * transform.localScale.z);
        }


        hit = Physics2D.BoxCast(boxCollider.transform.position, boxCollider.size * 1.5f, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime*moveSpeed), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime * moveSpeed, 0);
        }

        hit = Physics2D.BoxCast(boxCollider.transform.position, boxCollider.size * 1.5f, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime*moveSpeed), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime * moveSpeed, 0, 0);
        }
    }

    private Vector3 ProcessInput()
    {
        float moveX = 0, moveY = 0;
        if (joystick.Horizontal > 0.2 || joystick.Horizontal < -0.2 || joystick.Vertical > 0.2 || joystick.Vertical < -0.2)
        {
            moveX = joystick.Horizontal;      
            moveY = joystick.Vertical;
        }
        else{
            if(joystick.Horizontal != 0 && joystick.Vertical != 0)
            dir = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
        }
        moveDirection = new Vector3(moveX, moveY,0).normalized;
        //moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        return moveDirection;
    }

    public void ManageTakeButton()
    {
        bool overlapItem = false;
        boxCollider.OverlapCollider(filter, hits);
        foreach(var hit in hits)
        {
            if(hit == null)
            {
                continue;
            }
            if(hit.gameObject.CompareTag("ItemWorld")|| hit.gameObject.CompareTag("Chest"))
            {
                overlapItem = true;
            }
        }
        if (overlapItem)
        {
            takeButtonGameObject.SetActive(true);
        }
        else
        {
            takeButtonGameObject.SetActive(false);
        }
    }
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("ItemWorld"))
        {
            GameManager.instance.selectedItemGameObject = coll.gameObject;
            takeButtonGameObject.SetActive(true);
        }
        //else if(coll.gameObject.CompareTag("Chest"))
        //{
        //    takeButtonGameObject.SetActive(true);
        //}
        
        if(coll.name == "Gad")
        {
            Death();
        }
    }

    protected override void NonCollide()
    {
        takeButtonGameObject.SetActive(false);
    }

    public void Death()
    {
        Destroy(gameObject);
        GameManager.instance.gameOverPanel.SetActive(true);
    }

    public void SetCharacterState(string state)
    {
        if (state.Equals("Idle"))
        {
            SetAnimation(defaultAnimation, true, 1);
        }
        else if (state.Equals("Moving"))
        {
            SetAnimation(movingAnimation, true, 1.3f);
        }
    }

    private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (currentAnimation.Equals(currentState))
            return;
        skeletonAnimation.state.SetAnimation(0, animation,loop).TimeScale = timeScale;
        currentAnimation = currentState;
    }

}


