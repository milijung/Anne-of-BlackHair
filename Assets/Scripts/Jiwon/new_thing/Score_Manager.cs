using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Manager : MonoBehaviour
{
    public TextMeshProUGUI Best_score;
    public TextMeshProUGUI Second_score;
    public TextMeshProUGUI Third_score;
    public GameObject[] jums;
    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("BestScore") || (PlayerPrefs.GetInt("BestScore")==0))
        {
            Best_score.text = "000";
        }
        else
        {
            Best_score.text = PlayerPrefs.GetInt("BestScore").ToString();
        }

        if (!PlayerPrefs.HasKey("SecondScore") || (PlayerPrefs.GetInt("SecondScore") == 0))
        {
            Second_score.text = "000";
        }
        else
        {
            Second_score.text = PlayerPrefs.GetInt("SecondScore").ToString();
        }
        if (!PlayerPrefs.HasKey("ThirdScore") || PlayerPrefs.GetInt("ThirdScore") == 0)
        {
            Third_score.text = "000";
        }
        else
        {
            Third_score.text = PlayerPrefs.GetInt("ThirdScore").ToString();
        }

        
        
    }
}
