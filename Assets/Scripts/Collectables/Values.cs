using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class Values : Collectables
{
    public static Values instance;
    public int valueIndex = 0;

    public int ID;

    private bool _isPackaged;
    private bool _isCollected;
    private bool test = true;

    public ParticleSystem smoke;

    public ParticleSystem ipadDeadEffect;
    public ParticleSystem bicycleDeadEffect;
    public ParticleSystem bearDeadEffect;
    public ParticleSystem sheepDeadEffect;

    private void Start()
    {
        instance = this;
        ValueLevelController();
    }

    protected override void hitFeedBack()
    {
        base.hitFeedBack();
        transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f)
            .OnComplete(() => transform.DOScale(new Vector3(1, 1, 1), 0.1f));
    }

    protected override void destroyFeedBack()
    {
        base.destroyFeedBack();

        if (valueIndex == 0)
        {
            Instantiate(bearDeadEffect, gameObject.transform.position, Quaternion.identity);
        }

        if (valueIndex == 1)
        {
            Instantiate(sheepDeadEffect, gameObject.transform.position, Quaternion.identity);
        }

        if (valueIndex == 2)
        {
            Instantiate(bicycleDeadEffect, gameObject.transform.position, Quaternion.identity);
        }

        if (valueIndex >= 3)
        {
            Instantiate(ipadDeadEffect, gameObject.transform.position, Quaternion.identity);
        }


        ValuableController.instance.RemoveValue(ID + 1);
        _isCollected = false;
    }

    protected override void FallFeedBack()
    {
        base.FallFeedBack();

        if (ValuableController.instance.valuableList.Count >= 1)
        {
            gameObject.tag = "Untagged";
            test = false;
            ValuableController.instance.RemoveListValue(ID + 1);
            _isCollected = false;
            var rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(0, 0, 600);
        }
    }

    private void Update()
    {
        if (_isCollected)
        {
            var xPos = -PlayerController.instance.transform.position / 4.5f;
            var rotation = gameObject.transform.rotation;
            rotation = new Quaternion(rotation.x, xPos.x / 4.5f, rotation.z, 1);
            transform.rotation = rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Test"))
        {
            FallFeedBack();
        }

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Collectable"))
        {
            if (test)
            {
                if (!_isCollected)
                {
                    hitFeedBack();
                    ValuableController.instance.StackObjet(gameObject,
                        ValuableController.instance.valuableList.Count - 1);
                }

                _isCollected = true;
            }
        }

        if (other.gameObject.CompareTag("Passage") || other.gameObject.CompareTag("FirstPassage"))
        {
            ValuableController.instance.valuesBool = true;
            //var valueList = ValuableController.instance.valuableList;
            //valueList.Remove(valueList[valueList.Count - 1]);
            transform.tag = "Untagged";
            gameObject.transform.DOMoveX(4.3f, 0.5f);
            ValuableController.instance.valuableList.Remove(gameObject);
            transform.parent = null;
            Destroy(other.gameObject.GetComponent<Collider>());
        }

        if (other.gameObject.CompareTag("Passage10x"))
        {
            // var handPos = ValuableController.instance.santaClausHandPos.position;
            // handPos.y += 1.1f;
            // var test = new Vector3(handPos.x, handPos.y, handPos.z);
            // Vector3 newPos = ValuableController.instance.santaClausHandPos.position;
            // newPos.y += 1.1f;
            // transform.DOJump(newPos, 1.2f, 1, 1f);
            ValuableController.instance.FinishStack(gameObject);
            transform.parent = null;
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            destroyFeedBack();
            // float tes2 = 0;
            // var test = ValuableController.instance.valuableList;
            // // foreach (var VARIABLE in test)
            // // {
            // //     if (ID > tes2)
            // //     {
            // //         ValuableController.instance.value = VARIABLE.GetComponent<Values>().ID;
            // //         Destroy(VARIABLE);
            // //         test.Remove(VARIABLE);
            // //     }
            // // }
            //
            // for (int i = ID; i < test.Count + 1; i++)
            // {
            //     Destroy(test[i]);
            //     test[i].transform.parent = null;
            //     test.Remove(test[i]);
            // }
        }

        if (!other.TryGetComponent(out Iinteractable interactable)) return;
        switch (interactable)
        {
            case ValueGate valueGate:

                smoke.Play();
                hitFeedBack();
                switch (valueGate.OperatorType)
                {
                    case OperatorType.positive:
                        if (!_isPackaged)
                        {
                            valueIndex++;
                        }

                        break;
                    case OperatorType.negative:
                        if (!_isPackaged)
                        {
                            if (valueIndex > 0)
                                valueIndex--;
                        }

                        break;
                }

                ValueLevelController();
                break;
        }


        if (other.gameObject.CompareTag("PackageDoor"))
        {
            hitFeedBack();
            smoke.Play();
            _isPackaged = true;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(true);
        }
    }

    void ValueLevelController()
    {
        // for (int i = 0; i < ValueIndex; i++)
        // {
        //     if (ValueIndex < 4)
        //     {
        //         transform.GetChild(ValueIndex).gameObject.SetActive(true);
        //         transform.GetChild(ValueIndex - 1).gameObject.SetActive(false);
        //     }
        // }

        if (valueIndex == 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
        }

        if (valueIndex == 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
        }

        if (valueIndex == 2)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(3).gameObject.SetActive(false);
        }

        if (valueIndex == 3)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // switch (OperatorType)
        // {
        //     case OperatorTypeValue.Teddy:
        //         
        //
        //         break;
        //     case OperatorTypeValue.SuperheroToy:
        //         
        //
        //         break;
        //     case OperatorTypeValue.Bicycles:
        //         
        //         break;
        //     case OperatorTypeValue.Ipad:
        //         
        //         break;
        // }
        //Todo switch the values in value list
    }
}