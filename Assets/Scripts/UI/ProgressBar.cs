using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Transform Player;
    public Transform endLine;
    public Slider Slider;

    private float maxDistance;

    void Start()
    {
        maxDistance = getDistance();
    }

    void Update()
    {
        if (Player.position.z <= maxDistance && Player.position.z <= new Vector3(0,0,250).z)
        {
            float distance = 1 - (getDistance() / maxDistance);
            setProgress(distance);
        }
    }

    float getDistance()
    {
        return Vector3.Distance(Player.position, new Vector3(0, 0, 300));
    }

    void setProgress(float p)
    {
        Slider.value = p;
    }
}