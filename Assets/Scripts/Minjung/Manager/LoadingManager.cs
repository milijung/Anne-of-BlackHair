using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{   
    public string nextScene; // 로딩하려는 씬의 이름
    
    public Slider progressBar;
    

    private void Start()
    {
        progressBar.value = 0;
        StartCoroutine(LoadAsynScene());
    }
    IEnumerator LoadAsynScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;
        
        float timer = 0.0f;
        while (!operation.isDone) // 로딩이 완료될때까지 반복
        {
            yield return null;
            timer += Time.deltaTime;
            if (operation.progress >= 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1.0f, timer);

                if (progressBar.value == 1.0f)
                {
                    operation.allowSceneActivation = true;
                }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, operation.progress, timer);
                if (progressBar.value >= operation.progress)
                {
                    timer = 0.0f;
                }
            }
        }
    }
}
