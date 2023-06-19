using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class ValueGate : MonoBehaviour, Iinteractable
{
    public static ValueGate instance;
    
    [field: SerializeField] public OperatorType OperatorType { get; private set; }
    [field: SerializeField] public float Amount { get; private set; }
    public string Type;

    [FormerlySerializedAs("textMeshPro")] [SerializeField]
    private TextMeshPro doorText;

    public ParticleSystem doorEffect;

    private void Start()
    {
        var doorEffectMain = doorEffect.main;
        switch (OperatorType)
        {
            case OperatorType.positive:
                doorText.text = Type;
                doorEffectMain.startColor = Color.green;
                // bool isNegative = Amount < 0;
                // textMeshPro.text = $"{(isNegative ? "-" : "+")}{Amount}";
                break;
            case OperatorType.negative:
                doorText.text = $"- {Type}";
                doorEffectMain.startColor = Color.red;
                //bool isMultiplication = Amount > 1;
                //textMeshPro.text = $"{(isMultiplication ? "x" : "/")}{(isMultiplication ? Amount : 1 / Amount)}";
                break;
        }
    }

    public void Interact()
    {
        //todo you can add something (effect...)
    }
}

public enum OperatorType
{
    positive,
    negative,
}