using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCamera;
    private bool isShake = false;
    Vector3 cameraPos;

    [SerializeField] [Range(0.01f, 0.1f)] float shakeRange = 0.03f;
    [SerializeField] [Range(0.1f,1f)] float duration = 0.3f;

    private void Update()
    {
        if (!GameManager.isPlay) return;
        else
        {
            if (BerryController.BumpOntheRoad)
                Shake();
            else
                if(!isShake) mainCamera.transform.position = new Vector3(0, 0, -10);
        }
    }
    public void Shake()
    {
        isShake = true;
        cameraPos = mainCamera.transform.position;
        InvokeRepeating("StartShake", 0f, 0.05f);
        Invoke("StopShake", duration);
        mainCamera.transform.position = new Vector3(0, 0, -10);
    }
    void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }
    void StopShake()
    {
        isShake = false;
        mainCamera.transform.position = new Vector3(0,0,-10);
        CancelInvoke("StartShake");
        
    }
}
