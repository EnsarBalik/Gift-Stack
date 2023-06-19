using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Collectable") && !ValuableController.instance.valuableList.Contains(other.gameObject))
        {
            //other.transform.parent = gameObject.transform;
            ValuableController.instance.StackObjet(other.gameObject,
                ValuableController.instance.valuableList.Count - 1);
        }
    }
}