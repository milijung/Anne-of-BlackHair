using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioSource ButtonAudio;
    public static AudioSource SwitchSceneAudio;
    public static AudioSource river;
    private void Awake()
    {
        ButtonAudio = GameObject.Find("ButtonAudio").GetComponent<AudioSource>();
        SwitchSceneAudio = GameObject.Find("SceneAudio").GetComponent<AudioSource>();
        river = GameObject.Find("riverAudio").GetComponent<AudioSource>();
    }
    
}
