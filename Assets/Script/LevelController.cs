using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] FlashImage _flashImage = null;
    [SerializeField] Color _newColor = Color.red;
    public Player player;

    // Update is called once per frame
    void Update()
    {
        if (player.SomoonGauge>69 && player.SomoonGauge<71)
        {
            _flashImage.StartFlash(1.5f, 1.0f, _newColor);
        }
        if (player.SomoonGauge > 79 && player.SomoonGauge < 81)
        {
            _flashImage.StartFlash(1.5f, 1.0f, _newColor);
        }
        if (player.SomoonGauge > 89 && player.SomoonGauge < 91)
        {
            _flashImage.StartFlash(1.5f, 1.0f, _newColor);
        }
    }
}
