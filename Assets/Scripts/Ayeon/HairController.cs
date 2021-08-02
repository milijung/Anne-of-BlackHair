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
        animator.SetTrigger("Bleach");
    }

    public void _get_dye()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BLACK")
        || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.HAIR_ROOT")
        || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.HAIR_ROOT2")
        || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.HAIR_ROOT3"))
        return;
    
        else animator.SetTrigger("Dye");
    }

}
