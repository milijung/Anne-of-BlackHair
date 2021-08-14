using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBase : MonoBehaviour
{
    public Vector2 StartPosition;
    private void OnEnable() // 오브젝트가 활성화되면 실행
    {
        if (SpawnManager.MobStartNum ==0)
        {
            gameObject.SetActive(false); // SpawnManager 실행 전에 Mob이 등장하는 것 방지
        }
        else
        {
            gameObject.SetActive(true);
        }
        transform.position = StartPosition;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag != "Player")
            collision.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.isPlay)
        {
            transform.Translate(Vector2.down * Time.deltaTime * GameManager.instance.gameSpeed * 12);
            if (transform.position.y < -8) // 화면 끝까지 Mob이 이동하면 해당 Mob 비활성화
            {
                gameObject.SetActive(false);
            }
        }
    }
}