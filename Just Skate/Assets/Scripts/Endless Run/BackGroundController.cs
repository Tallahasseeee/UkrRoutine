using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public float lastX;
    public float firstX;
    public float deltaTime;
    public float moveTime;
    public float XPosition;
    public float YPosition;
    public float ZPosition;
    public float deltaX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > moveTime)
        {   
            if(XPosition < lastX)
            {
                XPosition = firstX;
            }
            XPosition -= deltaX;
            transform.position = new Vector3(XPosition, YPosition, ZPosition);
            moveTime = Time.time + deltaTime;
        }
    }
}
