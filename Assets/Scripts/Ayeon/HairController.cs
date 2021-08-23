using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }

    public void _get_bleach()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.RED")
        || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.DEEP_RED")
        || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.ORANGE")
        || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.DEEP_YELLOW"))
            return;
        animator.SetTrigger("Bleach"); ;
    }

    public void _get_dye()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.RED")
        || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.DEEP_RED")
        || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.ORANGE")
        || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.DEEP_RED"))
            animator.SetTrigger("Dye");

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BLEACH"))
            return;

    }

}
