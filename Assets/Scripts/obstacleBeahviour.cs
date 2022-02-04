using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleBeahviour : MonoBehaviour
{
    public float rotationValue = 2;
    public float platSpeed = 0.1f;

    private float max,min;

    public float maxPlatR;
    [SerializeField] private float minPlatR,minPlatL;
    [SerializeField] private GameObject flingerPlatform = null;

    private bool isCoroutineRunning;

    private void Start()
    {
        isCoroutineRunning = false;
        max = 8;
        min = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (this.tag)
        {
            case "rotatingBlock":
                transform.Rotate(new Vector3(0, rotationValue, 0));
                break;
            case "pusher":
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.PingPong(Time.time*10,max-min)+min);
                break;
            case "smasher":
                transform.Rotate(new Vector3(rotationValue, 0, 0));
                break;
            case "stomper":
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.time*20,40-10)+10, transform.localPosition.z);
                break;
            case "leftplatformer":
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.time*platSpeed,minPlatR-minPlatL)+minPlatL, transform.localPosition.z);
                break;
            case "rightplatformer":
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.time*platSpeed,maxPlatR-minPlatR)+minPlatR, transform.localPosition.z);
                break;
            case "flinger":
                if (isCoroutineRunning || flingerPlatform == null) break;
                flingerPlatform.tag = "Activator";
                StartCoroutine(Fling());
                break;

        }
    }

    private IEnumerator Fling()
    {
        isCoroutineRunning = true;

        while (transform.localEulerAngles.z < 45)
        {
            transform.Rotate(new Vector3(0, 0, rotationValue));
            yield return new WaitForSeconds(0.005f);
        }

        yield return new WaitForSeconds(0.5f);
        flingerPlatform.tag = "Untagged";
        
        while (transform.localEulerAngles.z > 0.05f)
        {
            transform.Rotate(new Vector3(0, 0, -rotationValue/10f));
            Debug.Log(transform.localEulerAngles.z + " | "+transform.eulerAngles.z);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);

        isCoroutineRunning = false;
        
        yield return null;
    }
}