using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator _ann_animator;

    public GameObject _twinkle;
    TwinkleController _twinkle_controller;

    // Start is called before the first frame update
    void Start()
    {
        _twinkle_controller = _twinkle.GetComponent<TwinkleController>();
        _ann_animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _ann_jump()
    {
        _ann_animator.SetTrigger("J");
    }

    public void _ann_get_bleach()
    {
        _ann_animator.SetBool("B",true);
    }

    public void _ann_get_dye()
    {

        if (_ann_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BLACK")
        || _ann_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.HAIR_ROOT1")
        || _ann_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.HAIR_ROOT2")
        || _ann_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.HAIR_ROOT3")
        || _ann_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.HAIR_ROOT4"))
            return;

        _ann_animator.SetBool("D",true);
        _twinkle_controller.twinkle_on();
    }
}
