using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator _ann_animator;

    // Start is called before the first frame update
    void Start()
    {
        _ann_animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    public void _ann_jump()
    {
        StartCoroutine(isJump());
    }
    IEnumerator isJump()
    {
        RoadBase.jump = true;
        yield return new WaitForSeconds(0.4f);
        RoadBase.jump = false;
        StopCoroutine(isJump());
    }
}
