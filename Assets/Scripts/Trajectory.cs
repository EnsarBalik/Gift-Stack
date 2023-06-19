using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public static Trajectory instance;

    public Transform target;

    public float h = 25;
    public float gravity = -15;

    public bool debugPath;

    private float time;

    private float simulationTime;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        drawPath();
    }

    public void Launch(Rigidbody rb)
    {
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;
        rb.velocity = CalculateLaunchData(rb).initialVelocity;
    }

    private LaunchData CalculateLaunchData(Rigidbody rb)
    {
        float displacementY = target.position.y - rb.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - rb.position.x, 0, target.position.z - rb.position.z);
        time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    private void drawPath()
    {
        LaunchData launchData = CalculateLaunchData(transform.GetComponent<Rigidbody>());
        Vector3 previousDrawPoint = transform.position;

        int resolation = 30;
        for (int i = 1; i <= resolation; i++)
        {
            simulationTime = i / (float)resolation * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime +
                                   Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = transform.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.red);
            previousDrawPoint = drawPoint;
        }
    }

    private struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
    
    IEnumerator ObjectSettings()
    {
        yield return new WaitForSeconds(time);
        PlayerController.instance.speed = 12f;
        PlayerController.instance.GetComponent<Rigidbody>().isKinematic = true;
    }
    
    IEnumerator ValueSettings(GameObject other)
    {
        yield return new WaitForSeconds(simulationTime);
        var rb = other.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        yield return new WaitForSeconds(0.05f);
        other.transform.position = new Vector3(other.transform.position.x, 1.35f, other.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().speed = 0;
            var playerRB = other.gameObject.GetComponent<Rigidbody>();
            playerRB.isKinematic = false;
            Launch(playerRB);
            StartCoroutine(ObjectSettings());
        }

        if (other.gameObject.CompareTag("Collectable"))
        {
            StartCoroutine(ValueSettings(other.gameObject));
            var valueRB = other.gameObject.GetComponent<Rigidbody>();
            valueRB.isKinematic = false;
            Launch(valueRB);
        }
    }
}