using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject stopPanel;
    public GameObject GameOverPanel;
    public GameObject fadeSprite;
    public GameObject AnneCry;
    public GameObject[] Count;
    public GameObject[] Berry;

    void Start()
    {
        stopPanel.SetActive(false); // ÆÐ³Î ¼û±â±â
        GameOverPanel.SetActive(false);
        fadeSprite.SetActive(false);
        AnneCry.SetActive(false);
        for(int i = 0; i < Count.Length; i++)
        {
            Count[i].SetActive(false);
        }
        for(int i = 0; i < Berry.Length; i++)
        {
            Berry[i].SetActive(false);
        }
    }

}
