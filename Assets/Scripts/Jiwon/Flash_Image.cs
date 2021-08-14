using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash_Image : MonoBehaviour
{
    public GameObject FlashImage;
    private void Start()
    {
        StartCoroutine(Flash());
    }

    
    IEnumerator Flash()
    {
        while (true)
        {
            FlashImage.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            FlashImage.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }

    }

    private void Update()
    {
        
    }
}
