using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SomoonGaugeBar : MonoBehaviour
{
    public Player player;
    [SerializeField]
    private Slider somoonBar;

    void Start()
    {
        somoonBar.value = player.SomoonGauge;
    }

    // Update is called once per frame
    void Update()
    {
        HandleSomoonBar();
    }

    void HandleSomoonBar()
    {
        somoonBar.value = player.SomoonGauge;
    }
}
