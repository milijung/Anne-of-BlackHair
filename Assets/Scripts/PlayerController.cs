using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 direction;
    public GameObject gamePanel, fadeSprite;

    private int desiredLane = 1; // 0: ¿ÞÂÊ¶óÀÎ, 1: Áß°£¶óÀÎ, 2: ¿À¸¥ÂÊ ¶óÀÎ
    public float laneDisance = 1.5f;// ¶óÀÎ »çÀÌÀÇ °Å¸®

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
<<<<<<< HEAD
        
=======

>>>>>>> parent of a89b197 (ì´ˆê¸°í™” -merge ì „)
        if(hit.gameObject.tag == "ItemBox")
            Debug.Log("ItemBox");
        if(hit.gameObject.tag == "Dye")
            Debug.Log("Dye");
        if(hit.gameObject.tag == "Bleach")
            Debug.Log("Bleach");
        
    }

<<<<<<< HEAD

=======
>>>>>>> parent of a89b197 (ì´ˆê¸°í™” -merge ì „)
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
        if (Input.GetKey(KeyCode.Escape)) // ½º¸¶Æ®Æù µÚ·Î°¡±â ¹öÆ° ´­·¶À» ¶§
        {
            stepAudio.Pause();
            gamePanel.SetActive(true); // ÆÐ³Î º¸ÀÌ±â
            fadeSprite.SetActive(true);
            Time.timeScale = 0; // ÀÏ½ÃÁ¤Áö
        }
    }

<<<<<<< HEAD
    private void step_audio_play()
=======
    private void step_audio_play() 
>>>>>>> parent of a89b197 (ì´ˆê¸°í™” -merge ì „)
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
        // ÀÔ·ÂµÈ swipe¿¡ µû¶ó ¶óÀÎ¹øÈ£ °áÁ¤
        if (SwipeManager.swipeRight) // ¸¸¾à ¿À¸¥ÂÊÀ¸·Î ½º¿ÍÀÌÇÁÇß´Ù¸é
        {
            desiredLane++; // ¶óÀÎ¹øÈ£++
            if (desiredLane == 3) // ¶óÀÎ¹øÈ£<=2
                desiredLane = 2;
        }
        if (SwipeManager.swipeLeft) // ¸¸¾à ¿ÞÂÊÀ¸·Î ½º¿ÍÀÌÇÁÇß´Ù¸é
        {
            desiredLane--; // ¶óÀÎ¹øÈ£--
            if (desiredLane == -1) // ¶óÀÎ¹øÈ£>=0
                desiredLane = 0;
        }

        // ¶óÀÎ¹øÈ£¿¡ µû¸¥ object ÁÂÇ¥°ª ¼³Á¤ 
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up; 
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDisance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDisance;
        
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position; // ÀÌµ¿ÇØ¾ßÇÏ´Â ÁÂÇ¥°ª - ÇöÀç ÁÂÇ¥°ª
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;  // diffÀÇ ´ÜÀ§º¤ÅÍ * 25 * Time.deltaTime
        if (moveDir.sqrMagnitude < diff.magnitude)
            controller.Move(moveDir); // moveDir·Î object ÀÌµ¿
        else
            controller.Move(diff); // diff·Î object ÀÌµ¿
    }
    

}

