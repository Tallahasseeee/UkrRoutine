using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.Experimental.Rendering.Universal;

public class Enemy : Mover
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset defaultAnimation, movingAnimation;
    private string currentState;
    private string currentAnimation;

    public LayerMask targetLayer;
    public LayerMask blockingLayer;
    public GameObject light;
    private Light2D pointLight;

    public bool canSeePlayer;
    public float angle;
    public Vector3 dir;
    public float triggerLength = 3;
    public float chaseLength = 100;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;
    private float rotationSpeed = 720;
    private int counter = 0;
    public float deltaTime;
    private float realTime;

    public Vector3[] mainGadDir;

    public bool mainGad;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerTransform = GameObject.Find("Player").transform;
        startingPosition = transform.position;
        //hitbox = GetComponent<BoxCollider2D>();
        pointLight = light.GetComponent<Light2D>();
        currentState = "Idle";
        currentAnimation = "";
        SetCharacterState(currentState);
    }


    private void FixedUpdate()
    {

        FOV();
        SetLightning();
        if (mainGad && Time.time - realTime > deltaTime)
            MainGadFunction();

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

        if (playerTransform != null)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
            {
                if (canSeePlayer)
                    chasing = true;

                if (chasing)
                {
                    light.SetActive(false);

                    if (!collidingWithPlayer)
                    {
                        UpdateMotor((playerTransform.position - transform.position).normalized);
                    }
                    else
                    {
                        GameManager.instance.playerGameObject.GetComponent<Player>().Death();
                        UpdateMotor((startingPosition - transform.position).normalized);
                    }
                }
            }
            else
            {
                UpdateMotor((startingPosition - transform.position).normalized);
                chasing = false;
            }
        }

        CollideMotor();
        
    }
    // Update is called once per frame
    private void CollideMotor()
    {
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;
            Debug.Log(hits[i].name);
            if (hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            hits[i] = null;
        }
    }

    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, triggerLength, targetLayer);
        if (rangeCheck.Length > 0 && playerTransform != null)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            
            if (Vector2.Angle(dir, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, blockingLayer))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;

            }
            else
                canSeePlayer = false;

        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    private void MainGadFunction()
    {
        if (counter < mainGadDir.Length - 1)
            counter++;
        else
            counter = 0;

        realTime = Time.time;
        dir = mainGadDir[counter];
    }

    private void SetLightning()
    {
        pointLight.transform.rotation = Quaternion.RotateTowards(pointLight.transform.rotation, Quaternion.LookRotation(Vector3.forward, dir), rotationSpeed * Time.deltaTime);
        pointLight.pointLightOuterAngle = angle;
        pointLight.pointLightInnerAngle = angle;
        pointLight.pointLightOuterRadius = triggerLength;
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
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = currentState;
    }
}
