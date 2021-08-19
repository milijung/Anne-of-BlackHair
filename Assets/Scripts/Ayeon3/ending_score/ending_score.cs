using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ending_score : MonoBehaviour
{
    public GameManager _game_manager;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
