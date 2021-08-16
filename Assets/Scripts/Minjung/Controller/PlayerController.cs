using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 direction;
    public GameObject gamePanel, fadeSprite;
    Sprite Background;
    private int desiredLane = 1; // 0: ���ʶ���, 1: �߰�����, 2: ������ ����
    public float laneDisance = 1.5f;// ���� ������ �Ÿ�

    public GameObject BackgroundMusic;
    AudioSource backmusic;
    public GameObject stepSound;
    AudioSource stepAudio;
    public GameObject toForest, toTown;

    AnimationController _animation_controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        stepAudio = stepSound.GetComponent<AudioSource>();
        _animation_controller = GameObject.Find("Animation_Controller").GetComponent<AnimationController>();
    }

    private void Update() 
    {
        Background = GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite;
        if (Background.name == "threeWay") // 마을
        {
            if (toTown.activeSelf && toTown.transform.position.y >= -4)
                OneWay();
            else
                ThreeWay();
        }
        else if (Background.name == "oneWay") // 숲
        {
            if (toForest.activeSelf && toForest.transform.position.y >= -4)
                ThreeWay();
            else
            {
                if (desiredLane != 1)
                {
                    BerryController.BumpOntheRoad = true;
                    OneWay();
                }
                else
                    OneWay();

            }
                
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up; 
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDisance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDisance;
        
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position; 
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime; 
        if (moveDir.sqrMagnitude < diff.magnitude)
            controller.Move(moveDir); 
        else
            controller.Move(diff);
       
    }
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.Escape))
        {
            stepAudio.Pause();
            backmusic.Pause();
            gamePanel.SetActive(true);
            fadeSprite.SetActive(true);
            Time.timeScale = 0;
            GameManager.isPlay = false;
        }
    } 
    void OneWay()
    {
        desiredLane = 1;
        if (SwipeManager.swipeUp)
        {
            _animation_controller._ann_jump();
        }
    }
    void ThreeWay()
    {
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
        if (SwipeManager.swipeUp)
        {
            _animation_controller._ann_jump();
        }
    }
}
