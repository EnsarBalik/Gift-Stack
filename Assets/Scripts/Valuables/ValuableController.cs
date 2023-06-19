using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class ValuableController : MonoBehaviour
{
    public static ValuableController instance;
    public List<GameObject> valuableList;
    public int value;

    public Transform santaClausHandPos;

    public bool valuesBool;

    private float yOffest;
    
    public void Start()
    {
        instance = this;
        StartCoroutine(FindHandPos());
    }

    public void StackObjet(GameObject other, int index)
    {
        valuableList.Add(other);
        other.GetComponent<Values>().ID = value;
        value++;
        Vector3 newPos = valuableList[index].transform.position;
        newPos.y = other.transform.position.y;
        newPos.z += 1.1f;
        other.transform.position = newPos;
    }

    public void RemoveValue(int Id)
    {
        if (valuableList.Count > 1)
        {
            for (int i = Id; i < valuableList.Count; i++)
            {
                Destroy(valuableList[i]);
            }

            StartCoroutine(WaitForSecond());
        }
    }

    public void RemoveListValue(int Id)
    {
        if (valuableList.Count >= 1)
        {
             for (int i = Id; i < valuableList.Count; i++)
             {
                 value--;
                 valuableList.Remove(valuableList[i]);
             }
        }
    }

    IEnumerator FindHandPos()
    {
        yield return new WaitForSeconds(1);
        santaClausHandPos = GameObject.FindWithTag("HandPos").transform;
    }

    IEnumerator WaitForSecond()
    {
        yield return new WaitForSeconds(0.05f);

        foreach (var item in valuableList.ToArray())
        {
            if (item == null)
            {
                value--;
                valuableList.Remove(item);
            }
        }
    }
    
    public void FinishStack(GameObject other)
    {
        yOffest++;
        var id = other.GetComponent<Values>().ID;
        Vector3 newPos = santaClausHandPos.position;
        newPos.y += yOffest;
        other.transform.DOJump(newPos, 1.2f, 1, 1f);
    }
    
    public void MoveObjectElement()
    {
        if (!valuesBool)
        {
            for (int i = 1; i < valuableList.Count; i++)
            {
                Vector3 pos = valuableList[i].transform.position;
                pos.x = valuableList[i - 1].transform.position.x;
                //pos.y = valuableList[i - 1].transform.position.y;
                valuableList[i].transform.DOMoveX(pos.x, 0.13f);
                //valuableList[i].transform.DOMoveY(pos.y, 0.8f);
                
                //valuableList[i].transform.DOLocalMoveY(valuableList[i].transform.localPosition.y, 0f);
                //valuableList[i].transform.DOMoveZ(finalPosition.z, 0f);
                // Vector3 targetPosX = Vector3.MoveTowards(valuableList[i].transform.position,
                //     PlayerController.instance.transform.position, (20000 * Time.deltaTime) / (1000 - (i * 5f)));


                var previousGem = valuableList[i - 1].transform;
                Vector3 previousPosition = previousGem.position;
                Vector3 finalPosition = previousPosition + previousGem.transform.forward * 1.5f;

                Vector3 targetPos = Vector3.MoveTowards(valuableList[i].transform.position, finalPosition,
                    5000 * Time.deltaTime);
                valuableList[i].transform.position =
                    new Vector3(valuableList[i].transform.position.x, valuableList[i].transform.position.y,
                        targetPos.z);
            }
        }
    }
}