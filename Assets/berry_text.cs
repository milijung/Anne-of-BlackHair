using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class berry_text : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.text = PlayerPrefs.GetInt("Berry").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
