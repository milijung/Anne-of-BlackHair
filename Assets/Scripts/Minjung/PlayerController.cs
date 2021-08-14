using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 direction;
    public GameObject gamePanel, fadeSprite;

    private int desiredLane = 1; // 0: ���ʶ���, 1: �߰�����, 2: ������ ����
    public float laneDisance = 1.5f;// ���� ������ �Ÿ�

    public GameObject BackgroundMusic;
    AudioSource backmusic;
    public GameObject stepSound;
    AudioSource stepAudio;

    AnimationController Animation_Controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        stepAudio = stepSound.GetComponent<AudioSource>();

        Animation_Controller = GameObject.Find("Animation_Controller").GetComponent<AnimationController>();
    }

    private void Update()
    {
        //transform.Translate(Vector2.up* Time.deltaTime * GameManager.instance.gameSpeed);
        // �Էµ� swipe�� ���� ���ι�ȣ ����
        if (SwipeManager.swipeRight) // ���� ���������� ���������ߴٸ�
        {
            desiredLane++; // ���ι�ȣ++
            if (desiredLane == 3) // ���ι�ȣ<=2
                desiredLane = 2;        
        }
        if (SwipeManager.swipeLeft) // ���� �������� ���������ߴٸ�
        {
            desiredLane--; // ���ι�ȣ--e;
            if (desiredLane == -1) // ���ι�ȣ>=0
                desiredLane = 0;
        }
        if (SwipeManager.swipeUp) // ���� �������� ���������ߴٸ�
        {
            Animation_Controller._ann_jump();
        }

        // ���ι�ȣ�� ���� object ��ǥ�� ���� 
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up; 
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDisance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDisance;
        
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position; // �̵��ؾ��ϴ� ��ǥ�� - ���� ��ǥ��
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;  // diff�� �������� * 25 * Time.deltaTime
        if (moveDir.sqrMagnitude < diff.magnitude)
            controller.Move(moveDir); // moveDir�� object �̵�
        else
            controller.Move(diff); // diff�� object �̵�
       
    }
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
       
        if (Input.GetKey(KeyCode.Escape)) // ����Ʈ�� �ڷΰ��� ��ư ������ ��
        {
            stepAudio.Pause();
            backmusic.Pause();
            gamePanel.SetActive(true); // �г� ���̱�
            fadeSprite.SetActive(true);
            Time.timeScale = 0; // �Ͻ�����
            GameManager.isPlay = false;
        }
    }

    
}
