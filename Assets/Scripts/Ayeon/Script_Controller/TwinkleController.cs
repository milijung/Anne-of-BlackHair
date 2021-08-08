using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinkleController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void twinkle_on()
    {
        gameObject.SetActive(true);
    }

    public void twinkle_off()
    {
        gameObject.SetActive(false);
    }

    public void twinkle_play()
    {
        gameObject.GetComponent<Animator>().Play("twinkle_anim",-1,0.0f);
    }
}
