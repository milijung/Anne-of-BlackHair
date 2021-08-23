using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairController : MonoBehaviour
{
    Animator animator1;

    public GameObject _twinkle;
    TwinkleController _twinkle_controller;
    // Start is called before the first frame update
    void Start()
    {
        _twinkle_controller = _twinkle.GetComponent<TwinkleController>();
        animator1 = GameObject.Find("Player").GetComponent<Animator>();
    }

    public void _get_jump()
    {
        animator1.SetTrigger("J");
    }

    public void _get_bleach()
    {
        animator1.SetBool("B",true);
    }

    public void _get_dye()
    {

        if (animator1.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BLACK")
        || animator1.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.HAIR_ROOT1")
        || animator1.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.HAIR_ROOT2")
        || animator1.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.HAIR_ROOT3")
        || animator1.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.HAIR_ROOT4"))
            return;

        animator1.SetBool("D",true);
        _twinkle_controller.twinkle_on();
    }
}
