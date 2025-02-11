using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopUp : MonoBehaviour
{
    public Image[] background;
    public Image[] bubbles;
    public Text[] speech;
    public GameObject BackAudio;
    public Text skip;
    Color[] background_colors = new Color[3];
    Color[] bubble_colors = new Color[3];
    Color[] text_colors = new Color[3];
    private void Awake()
    {
        for (int i = 0; i < background.Length; i++)
        {
            background_colors[i] = background[i].color;
            bubble_colors[i] = bubbles[i].color;
            text_colors[i] = speech[i].color;

        }

        for (int i = 0; i < background.Length; i++)
        {
            background_colors[i].a = 0.0f;
            bubble_colors[i].a = 0.0f;
            text_colors[i].a = 0.0f;

            background[i].color = background_colors[i];
            bubbles[i].color = bubble_colors[i];
            speech[i].color = text_colors[i];
        }
    }
    private void OnEnable()
    {
        StartCoroutine(Pop_Up());
        StartCoroutine(skipMove());
    }
    IEnumerator skipMove()
    {
        while (true)
        {
            skip.color = new Color(skip.color.r, skip.color.g, skip.color.b, 0.5f);
            yield return new WaitForSeconds(0.5f);
            skip.color = new Color(skip.color.r, skip.color.g, skip.color.b, 1f);
            yield return new WaitForSeconds(0.5f);
            yield return null;
        }
    }
    IEnumerator Pop_Up()
    {
        if (MainMenu.AudioPlay) BackAudio.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.2f);
        //�׸� 1,2
        for(int i = 0; i < 25; i++)
        {
            background_colors[0].a += 0.04f;
            background_colors[1].a += 0.04f;
            background[0].color = background_colors[0];
            background[1].color = background_colors[1];
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.2f);
        //��ǳ�� 1,2
        for (int j = 0; j < background.Length-1; j++)
        {
            for(int i = 0; i < 25; i++)
            {
                text_colors[j].a += 0.04f;
                bubble_colors[j].a += 0.04f;
                bubbles[j].color = bubble_colors[j];
                speech[j].color = text_colors[j];
                yield return new WaitForSeconds(0.01f);
            }
            
            yield return new WaitForSeconds(0.25f);
        }
        
        yield return new WaitForSeconds(0.2f);
        //�׸� 3
        for (int i = 0; i < 25; i++)
        {
            background_colors[2].a += 0.04f;
            background[2].color = background_colors[2];
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.2f);
        //��ǳ�� 3
        for (int i = 0; i < 25; i++)
        {
            text_colors[2].a += 0.04f;
            bubble_colors[2].a += 0.04f;
            bubbles[2].color = bubble_colors[2];
            speech[2].color = text_colors[2];
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(4.15f);
        StopCoroutine(skipMove());
        LoadingScene_Manager.LoadScene("Title");
    }
}
