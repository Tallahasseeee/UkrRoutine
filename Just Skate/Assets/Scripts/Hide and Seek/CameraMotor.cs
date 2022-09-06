using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class CameraMotor : MonoBehaviour
{
    public GameObject txt;

    public GameObject[] sceneObjects;
    public Transform LookAt;
    public float boundX = 2;
    public float boundY = 2;

    public float deltaTime;
    private float realTime;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        realTime = Time.time;
    }

    // Update is called once per frame
    private void LateUpdate()
    {


        Vector3 delta = Vector3.zero;
        if (LookAt != null)
        {
            float deltaX = LookAt.position.x - transform.position.x;

            if ((deltaX > boundX || deltaX < -boundX))
            {
                if (transform.position.x < LookAt.position.x)
                {
                    delta.x = deltaX - boundX;
                }
                else
                {
                    delta.x = deltaX + boundX;
                }
            }

            float deltaY = LookAt.position.y - transform.position.y;

            if ((deltaY > boundY || deltaY < -boundY))
            {
                if (transform.position.y < LookAt.position.y)
                {
                    delta.y = deltaY - boundY;
                }
                else
                {
                    delta.y = deltaY + boundY;
                }
            }
        }
        transform.position += new Vector3(delta.x, delta.y, 0);

        if (Time.time - realTime > deltaTime)
        {
            counter = 0;
            realTime = Time.time;
            ManageObjectActiveness();
        }

        counter++;
        //if(realTime != Time.time)
        //txt.GetComponent<TMP_Text>().text = Convert.ToString(counter/(Time.time - realTime));

    }

    public void ManageObjectActiveness()
    {
        foreach(var obj in sceneObjects){
            if ((obj.transform.position.x < transform.position.x - 13 || obj.transform.position.x > transform.position.x + 13) && ((obj.transform.position.y < transform.position.y - 9 || obj.transform.position.y > transform.position.y + 9)))
                obj.SetActive(false);
            else
                obj.SetActive(true);
        }
    }
}


/*
  && LookAt.position.x > -157 && LookAt.position.x < -108.5
 && LookAt.position.y > 63 && LookAt.position.y < 126
 */