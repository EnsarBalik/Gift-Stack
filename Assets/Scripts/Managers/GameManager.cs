using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using GameAnalyticsSDK;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int coin;
    [SerializeField] private TextMeshProUGUI coinText;

    public PlayerController player;

    private GameObject _currentLevel;
    public List<GameObject> levels;
    public TextMeshProUGUI levelCount;
    public GameObject shopButton;
    public GameObject tutorialGameObject;

    private void Awake()
    {
        GameAnalyticsSDK.GameAnalytics.Initialize();
    }

    private void Start()
    {
        TinySauce.OnGameStarted();


        LevelLoader();
        levelCount.text = $"level {LevelManager.Level}";
    }

    private void Update()
    {
        if (player.isGameStarted)
        {
            shopButton.SetActive(false);
            tutorialGameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        coin = PlayerPrefs.GetInt("myCoin", coin);
        coinText.text = "" + coin;
    }

    public void CollectCoin(int coinCollect)
    {
        coin += coinCollect * (PlayerController.instance.miniGameLevelCounter);
        StartCoroutine(_coinText());
        PlayerPrefs.SetInt("myCoin", coin);
    }

    private IEnumerator _coinText()
    {
        float totalTime = 2f;
        float elapsedTime = 0;
        int coinAmount = PlayerPrefs.GetInt("myCoin");
        float step = (coin - coinAmount) / totalTime;

        ClaimAnimation.instance.CountCoins();

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            int amount = Mathf.Clamp(coinAmount + Mathf.RoundToInt(step * elapsedTime), 0, coin);
            coinText.text = amount.ToString();
            yield return null;
        }
    }

    private IEnumerator LevelSystem()
    {
        LevelManager.Level++;
        yield return new WaitForSeconds(1f);
    }

    public void NextLevel()
    {
        TinySauce.OnGameFinished(50);
        StartCoroutine(LevelSystem());
        StartCoroutine(LoadAll());
    }

    private void LevelLoader()
    {
        var level = PlayerPrefs.GetInt("level", 1);
        var test = (level - 1) % levels.Count;

        _currentLevel = Instantiate(levels[test].gameObject);
    }

    private IEnumerator LoadAll()
    {
        yield return new WaitForSeconds(3f);
        //
        // player.isGameEnd = false;
        //
        // if(_currentLevel != null)
        //     Destroy(_currentLevel);
        // player.transform.position = _playerFirstPos;
        // UiManager.instance.CloseUI();
        // player.playerCamUp.gameObject.SetActive(false);
        // player.playerCam.gameObject.SetActive(true);
        // ValuableController.instance.test = false;
        SceneManager.LoadScene("SampleScene");
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}