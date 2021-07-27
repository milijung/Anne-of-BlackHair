using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeManager : MonoBehaviour
{
    public static bool tap, doubleTap, swipeLeft, swipeRight;
    private bool isDraging = false; // 드래그하고 있으면 true
    private bool Move = false;
    private Vector2 startTouch; // tap이 시작된 좌표값
    private Vector2 swipeDelta; // 스와이프된 거리

    private float lastTouchTime;
    private const float doubleTouchDelay = 0.5f;

    private void Start()
    {
        lastTouchTime = Time.time;
    }
    private void Update()
    {
        doubleTap = tap = swipeLeft = swipeRight = false;

        // Standalone Input 모듈: 컨트롤러/마우스 입력에 대해 동작하도록 설계됨.
        #region Standalone Inputs 
        if (Input.GetMouseButtonDown(0)) // 만약 마우스 버튼을 눌렀다면 (0: 마우스 왼쪽버튼)
        {
            tap = true;
            isDraging = true;
            Move = false;
            if(doubleTap) { doubleTap = false; } // 짝수번 눌렀을때만 더블탭 인식
            startTouch = Input.mousePosition; // startTouch = 클릭한 마우스 좌표값
        }
        else if (Input.GetMouseButtonUp(0)) // 만약 마우스 버튼을 눌렀다 바로 뗐다면 (0: 마우스 왼쪽버튼)
        {
            isDraging = false;
            StartCoroutine("isDoubleTap");
            if (!doubleTap && !Move) { lastTouchTime = Time.time; }
            Reset();
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began) // 만약 터치가 시작되었으면
            {
                tap = true;
                isDraging = true;
                Move = false;
                if (doubleTap) { doubleTap = false; } // 짝수번 눌렀을때만 더블탭 인식
                startTouch = Input.touches[0].position; // startTouch = 터치한 좌표값
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)// 만약 터치된 손가락이 스크린에서 떨어질 때|| 모바일폰을 귀에 갖다 대거나 touch tracking을 수행하지 않아야 할 경우면
            {
                isDraging = false;
                StartCoroutine("isDoubleTap");
                if (!doubleTap && !Move) { lastTouchTime = Time.time; }
                Reset();
            }
        }
        #endregion
        

        swipeDelta = Vector2.zero;
        if (isDraging) // 만약 드래그하고 있다면
        {
            if (Input.touches.Length > 0) // 모바일일 경우
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0)) // 마우스 입력일 경우
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }
        //Did we cross the distance?
        if (swipeDelta.magnitude > 125) // 만약 스와이프된 거리가 일정값 이상이면
        {
            Move = true; // 스와이프 했는지 체크
            float x = swipeDelta.x;

            if (x < 0)
                swipeLeft = true; // swipeDelta.x<0이면 swipeLeft = true
            else
                swipeRight = true; // swipeDelta.x>0이면 swipeRight = true
            Reset();
        }
    } 
    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero; // (0,0)으로 초기화
        isDraging = false;
    }
    IEnumerator isDoubleTap()
    {
        if ((Time.time - lastTouchTime < doubleTouchDelay) && !Move)
        {
            doubleTap = true;
        }
        StopCoroutine("isDoubleTap");
        yield return null;

    }

}
