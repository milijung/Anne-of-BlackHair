using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash_Level_Controller : MonoBehaviour
{
    [SerializeField] FlashImage _flashImage = null;
    [SerializeField] Color _newColor = Color.red;
    public SomoonGauge somoon_Gauge;

    // Update is called once per frame
    void Update()
    {
        if (somoon_Gauge.somoonGauge > 69 && somoon_Gauge.somoonGauge < 71)
        {
            _flashImage.StartFlash(1.5f, 1.0f, _newColor);
        }
        if (somoon_Gauge.somoonGauge > 79 && somoon_Gauge.somoonGauge < 81)
        {
            _flashImage.StartFlash(1.5f, 1.0f, _newColor);
        }
        if (somoon_Gauge.somoonGauge > 89 && somoon_Gauge.somoonGauge < 91)
        {
            _flashImage.StartFlash(1.5f, 1.0f, _newColor);
        }
    }
}
