using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Load_Title", 6f);
    }

    private void Load_Title()
    {
        LoadingScene_Manager.LoadScene("Title");
    }
}
