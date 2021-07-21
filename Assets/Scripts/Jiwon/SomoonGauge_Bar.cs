using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SomoonGauge_Bar : MonoBehaviour
{
    public SomoonGauge somoon_Gauge;
    [SerializeField] private Slider somoonBar;
    void Start()
    {
        somoonBar.value = somoon_Gauge.somoonGauge;
    }

    // Update is called once per frame
    void Update()
    {
        HandleSomoonBar();
    }

    void HandleSomoonBar()
    {
        somoonBar.value = somoon_Gauge.somoonGauge;
    }
}
