using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (MainMenu.AudioPlay)
            AudioManager.BackgroundAudio.Play();
    }
}
