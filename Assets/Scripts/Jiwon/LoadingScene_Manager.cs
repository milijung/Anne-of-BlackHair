using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene_Manager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField]
    public bool fin;
    public Slider LoadingBar;
    public GameObject LoadingAudio;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }
    void Update()
    {
        if (fin)
            LoadingBar.value = 1;
        
    }

    IEnumerator LoadSceneProcess()
    {
        LoadingAudio = GameObject.Find("LoadingAudio");
        if (MainMenu.AudioPlay) LoadingAudio.GetComponent<AudioSource>().Play();
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = fin = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                LoadingBar.value = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                LoadingBar.value = Mathf.Lerp(0.9f, 1f, timer);
                if(LoadingBar.value >= 1f)
                {
                    fin = true;
                    yield return new WaitForSeconds(1f);
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
        yield return new WaitForSeconds(1f);
        op.allowSceneActivation = true;
    }

}
