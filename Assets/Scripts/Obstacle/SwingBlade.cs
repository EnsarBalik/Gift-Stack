using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBlade : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float minAngle;
    [SerializeField] float maxAngle;

    private void Update()
    {
        float rotation = Mathf.Lerp(minAngle, maxAngle, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
        transform.rotation = Quaternion.Euler(rotation, 90, -90);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlayerController.instance.PushBack());
        }
    }
}
