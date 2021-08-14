using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject UIEmergencyBg;



    public void AlertDanger()
    {
        UIEmergencyBg.SetActive(true);
    }

    public void ReturnDisplay()
    {
        UIEmergencyBg.SetActive(false);
    }

    
}
