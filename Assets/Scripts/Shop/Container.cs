using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class Container : MonoBehaviour
{
    public string id;
    public float money;
    public bool purcahesd;
    public GameObject el;
    public GameObject glow;

    private void Start()
    {
        if (id == "El1")
        {
            if (!PlayerPrefs.HasKey("selected"))
            {
                PlayerPrefs.SetString("selected", id);
            }

            if (PlayerPrefs.GetFloat(id) == 0)
            {
                PlayerPrefs.SetFloat(id, 1f);
            }
        }
    }

    void LateUpdate()
    {
        if (PlayerPrefs.GetFloat(id) == 1f)
            purcahesd = true;
        else
            purcahesd = false;

        if (!purcahesd)
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{(purcahesd ? "Select" : money)}";
        }
        
        if (purcahesd)
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Select";
            glow.SetActive(false);
        }

        if (PlayerPrefs.GetString("selected") == id)
        {
            if (purcahesd)
            {
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Selected";
                el.SetActive(true);
                glow.SetActive(true);
            }
        }
        else
        {
            if (purcahesd)
            {
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Select";
                glow.SetActive(false);
            }
        }
    }
}