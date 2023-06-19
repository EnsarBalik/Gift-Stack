using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public UiManager uiManager;

    Vector2 _previousPosition;
    public float speed = 10f;

    private float _dpc;

    public Rigidbody rb;

    private float _deltaX;
    private Vector3 _targetPos;

    [Header("Finish")] private Vector3 _startPos;
    private float _desiredDuration = 3f;
    private float _elapsedTime;

    [Header("Path")] [SerializeField] public bool isGameStarted;
    [SerializeField] public bool isGameEnd;

    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera playerCamUp;

    public int miniGameLevelCounter;

    private void Awake()
    {
        _dpc = Screen.dpi / 2.54f;
        instance = this;
    }

    private void FixedUpdate()
    {
        if (!isGameEnd)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            {
                isGameStarted = true;
                _previousPosition = Input.mousePosition;
            }
            else if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(0))
            {
                Vector2 currentPos = Input.mousePosition;
                _deltaX = currentPos.x - _previousPosition.x;
                _previousPosition = currentPos;
            }
            else if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp(0))
            {
                _deltaX = 0;
            }
        }

        if (isGameStarted)
        {
            var rbPos = rb.transform.position;
            rbPos += rb.transform.right * (1.5f * _deltaX / _dpc);
            rbPos.x = Mathf.Clamp(rbPos.x, -4.3f, 4.3f);
            rbPos = new Vector3(rbPos.x, rbPos.y, rbPos.z + speed * Time.deltaTime);
            rb.transform.position = rbPos;
            ValuableController.instance.MoveObjectElement();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Time.timeScale = 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Time.timeScale = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            transform.DOMoveX(0, 0.05f).OnComplete(() => isGameEnd = true);
            playerCam.gameObject.SetActive(false);
            playerCamUp.gameObject.SetActive(true);
            for (int i = 0; i < ValuableController.instance.valuableList.Count; i++)
            {
                ValuableController.instance.valuableList[i].transform.parent = gameObject.transform;
            }

            var listCount = ValuableController.instance.valuableList.Count;
            miniGameLevelCounter = listCount;
        }

        if (other.gameObject.CompareTag("FirstPassage"))
        {
            isGameStarted = false;
            uiManager.LoseUI();
        }
        if (other.gameObject.CompareTag("Passage10x") ||
            other.gameObject.CompareTag("Passage"))
        {
            isGameStarted = false;
            uiManager.FinishUI();
        }

        if (other.gameObject.CompareTag("Test"))
        {
            StartCoroutine(PushBack());
        }
    }

    public IEnumerator PushBack()
    {
        rb.isKinematic = false;
        rb.AddForce(transform.forward * -25, ForceMode.Impulse);

        yield return new WaitForSeconds(0.5f);

        rb.isKinematic = true;
    }
}