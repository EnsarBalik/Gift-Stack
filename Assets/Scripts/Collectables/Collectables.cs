using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [field: SerializeField] public OperatorTypeValue OperatorTypee { get; private set; }
    
    public virtual void Collectable()
    {
        Debug.Log("Collected");
    }

    protected virtual void hitFeedBack()
    {
        Debug.Log("Hit feedback");
    }

    protected virtual void destroyFeedBack()
    {
        
    }

    protected virtual void FallFeedBack()
    {
        
    }

}

public enum OperatorTypeValue
{
    Teddy = 0,
    SuperheroToy = 1,
    Bicycles = 2,
    Ipad =3,
}