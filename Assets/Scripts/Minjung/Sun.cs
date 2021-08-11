using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public GameObject[] SunIMG;
    void Start()
    {
        for(int i = 0; i < SunIMG.Length; i++)
        {
            SunIMG[i].SetActive(false);
        }
        StartCoroutine(sunMove());
    }

    IEnumerator sunMove()
    {
        while (true)
        {
            SunIMG[0].SetActive(true);
            yield return new WaitForSeconds(10);
            SunIMG[0].SetActive(false);

            yield return new WaitForSeconds(60);

            SunIMG[1].SetActive(true);
            yield return new WaitForSeconds(10);
            SunIMG[1].SetActive(false);

            SunIMG[2].SetActive(true);
            yield return new WaitForSeconds(40);
            SunIMG[2].SetActive(false);
            yield return null;
        }
    }
}
