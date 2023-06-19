using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    protected virtual void ObjectMovement()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlayerController.instance.PushBack());
        }
    }

    private void Update()
    {
        ObjectMovement();
    }
}
