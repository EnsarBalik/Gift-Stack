using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalLog : Obstacle
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlayerController.instance.PushBack());
        }
    }
}
