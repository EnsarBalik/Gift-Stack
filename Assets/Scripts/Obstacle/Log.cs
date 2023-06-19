using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Obstacle
{
    public float rotationSpeed;
    public GameObject pivotObject;

    protected override void ObjectMovement()
    {
        base.ObjectMovement();
        transform.RotateAround(pivotObject.transform.position, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlayerController.instance.PushBack());
        }
    }
}