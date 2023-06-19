using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    
    [Header("Win UI")]
    public GameObject background;
    public GameObject shine;
    public GameObject completeText;
    public GameObject collectButton;
    public GameObject collectedGiftTxt;
    public TextMeshProUGUI collectedGiftCount;
    


    [Header("Lose UI")] 
    public GameObject failedText;
    public GameObject yourScore;
    public GameObject scoreText;
    public GameObject giftText;
    public GameObject loseButton;
    public GameObject loseBackground;


    public void FinishUI()
    {
        background.SetActive(true);
        shine.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack);
        shine.transform.DORotate(new Vector3(0, 0, 600), 60);
        completeText.transform.DOScale(1, 0.45f).SetEase(Ease.OutBack);
        collectedGiftTxt.transform.DOScale(1, 0.6f).SetEase(Ease.OutBack);
        collectedGiftCount.gameObject.transform.DOScale(1, 0.8f).SetEase(Ease.OutBack);
        collectButton.transform.DOScale(1, 1f).SetEase(Ease.OutBack);
        int instanceGiftCounter = PlayerController.instance.miniGameLevelCounter - 1;
        collectedGiftCount.text = instanceGiftCounter.ToString();
    }

    public void LoseUI()
    {
        loseBackground.SetActive(true);
        failedText.transform.DOScale(1, 0.4f).SetEase(Ease.OutBack);
        yourScore.transform.DOScale(1, 0.6f).SetEase(Ease.OutBack);
        scoreText.transform.DOScale(1, 0.8f).SetEase(Ease.OutBack);
        giftText.transform.DOScale(1, 1f).SetEase(Ease.OutBack);
        loseButton.transform.DOScale(1, 1.1f).SetEase(Ease.OutBack);
    }
}