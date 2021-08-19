using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioSource ButtonAudio;
    public static AudioSource river;
    public static AudioSource wigHatAudio;
    public static AudioSource eraserAudio;
    public static AudioSource deathBerryAudio;
    public static AudioSource BackgroundAudio;
    private void Awake()
    {

        BackgroundAudio = GameObject.Find("BackgroundAudio").GetComponent<AudioSource>();
        ButtonAudio = GameObject.Find("ButtonAudio").GetComponent<AudioSource>();
        river = GameObject.Find("riverAudio").GetComponent<AudioSource>();
        wigHatAudio = GameObject.Find("wigHatAudio").GetComponent<AudioSource>();
        eraserAudio = GameObject.Find("eraserAudio").GetComponent<AudioSource>();
        deathBerryAudio = GameObject.Find("deathBerryAudio").GetComponent<AudioSource>();
    }
    
}
