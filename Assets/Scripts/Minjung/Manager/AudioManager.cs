using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioSource ButtonAudio;
    public static AudioSource SwitchSceneAudio;
    public static AudioSource river;
    public static AudioSource wigHatAudio;
    public static AudioSource eraserAudio;
    public static AudioSource deathBerryAudio;
    public static AudioSource SomoonEndingAudio;
    public static AudioSource BerryEndingAudio;
    private void Awake()
    {
        if(GameObject.Find("SomoonEndingAudio") != null) { SomoonEndingAudio = GameObject.Find("SomoonEndingAudio").GetComponent<AudioSource>(); }
        if(GameObject.Find("BerryEndingAudio") != null) { BerryEndingAudio = GameObject.Find("BerryEndingAudio").GetComponent<AudioSource>(); }

        ButtonAudio = GameObject.Find("ButtonAudio").GetComponent<AudioSource>();
        SwitchSceneAudio = GameObject.Find("SceneAudio").GetComponent<AudioSource>();
        river = GameObject.Find("riverAudio").GetComponent<AudioSource>();
        wigHatAudio = GameObject.Find("wigHatAudio").GetComponent<AudioSource>();
        eraserAudio = GameObject.Find("eraserAudio").GetComponent<AudioSource>();
        deathBerryAudio = GameObject.Find("deathBerryAudio").GetComponent<AudioSource>();
    }
    
}
