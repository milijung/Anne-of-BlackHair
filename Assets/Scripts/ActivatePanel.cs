using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePanel : MonoBehaviour
{
    public GameObject gamePanel;

    void Start()
    {
        gamePanel.SetActive(false); // ÆÐ³Î ¼û±â±â
    }

}
