using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Load_next", 2.5f);
    }

    private void Load_next()
    {
        SceneManager.LoadScene("Start_Cartoon");
    }
}
