using System;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK.Setup;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ClaimAnimation : MonoBehaviour
{
    public static ClaimAnimation instance;
    // public List<GameObject> coins;
    // public Transform target;
    //
    //
    // public void Play()
    // {
    //     for (int i = 0; i < coins.Count; i++)
    //     {
    //         coins[i].SetActive(true);
    //         coins[i].transform.DOScale(1, 0.6f);
    //         coins[i].transform.DOMove(transform.position, 0.7f);
    //     }
    //     gameObject.SetActive(true);
    //     //TODO Haptic?
    // }

    [SerializeField] private GameObject pileOfCoins;
    [SerializeField] private Vector2[] initialPos;
    [SerializeField] private Quaternion[] initialRotation;
    [SerializeField] private int coinsAmount;
    public GameObject target;
    void Start()
    {
        instance = this;
        
        if (coinsAmount == 0) 
            coinsAmount = 10; // you need to change this value based on the number of coins in the inspector
        
        initialPos = new Vector2[coinsAmount];
        initialRotation = new Quaternion[coinsAmount];
        
        for (int i = 0; i < pileOfCoins.transform.childCount; i++)
        {
            initialPos[i] = pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
            initialRotation[i] = pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().rotation;
        }
    }


   public void CountCoins()
    {
        pileOfCoins.SetActive(true);
        var delay = 0f;
        
        for (int i = 0; i < pileOfCoins.transform.childCount; i++)
        {
            pileOfCoins.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

            pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(target.transform.position, 0.8f)
                .SetDelay(delay + 0.5f).SetEase(Ease.InBack);
             

            pileOfCoins.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f)
                .SetEase(Ease.Flash);
            
            
            pileOfCoins.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);

            delay += 0.1f;
        }
    }
}