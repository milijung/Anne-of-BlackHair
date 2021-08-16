using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBase : MonoBehaviour
{
    public Vector2 StartPosition;
    public GameObject way1, way2;
    private void OnEnable() // 오브젝트가 활성화되면 실행
    {
        
        if (SpawnManager.MobStartNum ==0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (way1 != null && way2 != null) { way1.SetActive(true); way2.SetActive(true); }
            if (gameObject.tag != "collider") { transform.position = StartPosition; }
            gameObject.SetActive(true);
            
        }
        
    }

    private void Update()
    {
        if (GameManager.isPlay && gameObject.tag != "way")
        {
            transform.Translate(Vector2.down * Time.deltaTime * GameManager.instance.gameSpeed * 12);
            if (transform.position.y < -8)
            {
                gameObject.SetActive(false);
                if (way1 != null && way2 != null) { way1.SetActive(false); way2.SetActive(false); }
            }
        }
    }
}