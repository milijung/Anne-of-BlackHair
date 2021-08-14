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

    public GameObject stepSound;
    AudioSource stepAudio;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        stepAudio = stepSound.GetComponent<AudioSource>();
        step_audio_play();
    }

    private void Update()
    {
        move_swipe();
    }


    void OnControllerColliderHit(ControllerColliderHit hit) {
        Debug.Log("Collision!!");
        if(hit.gameObject.tag == "ItemBox")
            Debug.Log("ItemBox");
        if(hit.gameObject.tag == "Dye")
            Debug.Log("Dye");
        if(hit.gameObject.tag == "Bleach")
            Debug.Log("Bleach");
        
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
        if (Input.GetKey(KeyCode.Escape)) // ����Ʈ�� �ڷΰ��� ��ư ������ ��
        {
            stepAudio.Pause();
            gamePanel.SetActive(true); // �г� ���̱�
            fadeSprite.SetActive(true);
            Time.timeScale = 0; // �Ͻ�����
        }
    }

    private void step_audio_play() 
    {
        if (MainMenu.AudioPlay)
        {
            stepAudio.Play();
        }
        else
        {
            stepAudio.Pause();
        }
    }

    private void move_swipe()
    {
        transform.Translate(Vector2.up* Time.deltaTime * GameManager.instance.gameSpeed);
        // �Էµ� swipe�� ���� ���ι�ȣ ����
        if (SwipeManager.swipeRight) // ���� ���������� ���������ߴٸ�
        {
            desiredLane++; // ���ι�ȣ++
            if (desiredLane == 3) // ���ι�ȣ<=2
                desiredLane = 2;
        }
        if (SwipeManager.swipeLeft) // ���� �������� ���������ߴٸ�
        {
            desiredLane--; // ���ι�ȣ--
            if (desiredLane == -1) // ���ι�ȣ>=0
                desiredLane = 0;
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
    

}

