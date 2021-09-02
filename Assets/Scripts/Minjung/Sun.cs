using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public static bool sunRise;
    public GameObject[] SunIMG;
    public GameObject light;
    void Start()
    {
        for(int i = 1; i < SunIMG.Length; i++)
        {
            SunIMG[i].SetActive(false);
        }
        SunIMG[0].SetActive(true);
        sunRise = true;
        StartCoroutine(sunMove());
    }

    IEnumerator sunMove()
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                SunIMG[0].SetActive(true);
                sunRise = true;
                yield return new WaitForSeconds(10);
                SunIMG[0].SetActive(false);

                yield return new WaitForSeconds(40);
                sunRise = false;

                SunIMG[1].SetActive(true);
                yield return new WaitForSeconds(10);
                SunIMG[1].SetActive(false);

                SunIMG[2].SetActive(true);
                yield return new WaitForSeconds(40);
                SunIMG[2].SetActive(false);
                
            }
            yield return null;
        }
    }
    void Update()
    {
        if (!Sun.sunRise)
        {
            light.SetActive(true);
            if (RoadBase.jump)
                StartCoroutine(JumpLight());
        }
        else
            light.SetActive(false);
    }
    IEnumerator JumpLight()
    {
        light.transform.Translate(Vector2.up*0.014f);
        if (light.transform.position.y > -3.5f)
            light.transform.position = new Vector2(light.transform.position.x, -3.5f);
        yield return new WaitForSeconds(0.5f);

        light.transform.Translate(Vector2.down*0.014f);
        if (light.transform.position.y < -4f)
            light.transform.position = new Vector2(light.transform.position.x, -4f);
        StopCoroutine(JumpLight());
    }
}
