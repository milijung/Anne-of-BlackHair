using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash_Image : MonoBehaviour
{
    Image image;
    Color _color;
    private void OnEnable()
    {
        image = GetComponent<Image>();
        _color = image.GetComponent<Image>().color;
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        while (true)
        {
            _color.a = 0.8f;
            image.GetComponent<Image>().color = _color;
            yield return new WaitForSeconds(0.4f);
            _color.a = 0.2f;
            image.GetComponent<Image>().color = _color;
            yield return new WaitForSeconds(0.4f);
        }
    }
}
