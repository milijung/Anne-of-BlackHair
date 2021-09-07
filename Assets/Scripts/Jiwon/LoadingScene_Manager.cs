using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene_Manager : MonoBehaviour
{
    static string nextScene;
    [SerializeField]
    public bool fin;
    public Slider LoadingBar;
    public GameObject LoadingAudio;
    public Text tip;
    string[][] tips = new string[7][];

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
        tips[0] = new string[] {"Tip. 고라니는 터치해서","재울 수 있어요"};
        tips[1] = new string[] {"Tip. 강은 다리를 이용해서", "건널 수 있어요" };
        tips[2] = new string[] {"Tip.가장자리에 있으면","밤고양이와 부딪힐 확률이 줄어들어요" };
        tips[3] = new string[] {"Tip.빨간머리로 염색하면", "사람들의 시야에 들어가도 괜찮아요" };
        tips[4] = new string[] {"Tip.과수원에 도착하면","열매 5개를 얻을 수 있어요" };
        tips[5] = new string[] {"Tip.낮에는 고양이들이 자고있으니","밟지않도록 조심해야해요" };
        tips[6] = new string[] {"Tip.망원경을 든 린드부인은","시야가 넓으니 주의하세요" };
        int tipNum = Random.Range(0, 7);
        LoadingAudio = GameObject.Find("LoadingAudio");
        tip.text = tips[tipNum][0] + System.Environment.NewLine + tips[tipNum][1];
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
