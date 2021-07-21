using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 direction;
    public GameObject gamePanel, fadeSprite;

    private int desiredLane = 1; // 0: 왼쪽라인, 1: 중간라인, 2: 오른쪽 라인
    public float laneDisance = 1.5f;// 라인 사이의 거리

    public GameObject BackgroundMusic;
    AudioSource backmusic;
    public GameObject stepSound;
    AudioSource stepAudio;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        stepAudio = stepSound.GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up* Time.deltaTime * GameManager.instance.gameSpeed);
        // 입력된 swipe에 따라 라인번호 결정
        if (SwipeManager.swipeRight) // 만약 오른쪽으로 스와이프했다면
        {
            desiredLane++; // 라인번호++
            if (desiredLane == 3) // 라인번호<=2
                desiredLane = 2;
        }
        if (SwipeManager.swipeLeft) // 만약 왼쪽으로 스와이프했다면
        {
            desiredLane--; // 라인번호--
            if (desiredLane == -1) // 라인번호>=0
                desiredLane = 0;
        }

        // 라인번호에 따른 object 좌표값 설정 
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up; 
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDisance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDisance;
        
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position; // 이동해야하는 좌표값 - 현재 좌표값
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;  // diff의 단위벡터 * 25 * Time.deltaTime
        if (moveDir.sqrMagnitude < diff.magnitude)
            controller.Move(moveDir); // moveDir로 object 이동
        else
            controller.Move(diff); // diff로 object 이동
       
    }
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
        if (Input.GetKey(KeyCode.Escape)) // 스마트폰 뒤로가기 버튼 눌렀을 때
        {
            stepAudio.Pause();
            backmusic.Pause();
            gamePanel.SetActive(true); // 패널 보이기
            fadeSprite.SetActive(true);
            Time.timeScale = 0; // 일시정지
        }
    }
}
