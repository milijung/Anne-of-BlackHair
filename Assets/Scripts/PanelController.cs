using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject gamePanel;
    public GameObject fadeSprite;

    void Start()
    {
        gamePanel.SetActive(false); // ÆÐ³Î ¼û±â±â
        fadeSprite.SetActive(false);
    }

}
