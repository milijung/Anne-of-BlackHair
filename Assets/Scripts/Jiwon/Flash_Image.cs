using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash_Image : MonoBehaviour
{
    public SomoonGauge Somoon;
    public GameObject FlashImage;
    public bool setFlash;
    void SetFlash(bool setFlash)
    {
        if(setFlash == true)
        {
            FlashImage.SetActive(true);
            Destroy(FlashImage, 2);
        }
        else
        {
            FlashImage.SetActive(setFlash);
        }
        
    }
    void Start()
    {

        setFlash = true;
        SetFlash(setFlash);
    }

    // Update is called once per frame
    void Update()
    {
        if(Somoon.somoonGauge >= 85 && Somoon.somoonGauge < 88)
        {
            setFlash = true;
            SetFlash(setFlash);
        }

        else
        {
            setFlash = false;
            SetFlash(setFlash);
        }
        
    }
}
