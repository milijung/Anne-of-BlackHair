using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMob_Controller : MonoBehaviour
{
    public bool isSurprise;
    public Animator[] mob_ani;
    public SpriteRenderer[] spriteRenderers;
    public GameObject[] mobs;

    private void Start()
    {
        isSurprise = false;
    }

    private void Update()
    {
        
    }

    public void Set()
    {
        for(int i = 0; i < mob_ani.Length; i++)
        {
            mob_ani[i].SetBool("isErase", true);
        }

        for(int i = 0; i < mobs.Length; i++)
        {
            spriteRenderers[i].sprite = mobs[i].GetComponent<MobBase>().sprites[1];
        }

        Invoke("UnSet", 2f);
    }

    public void UnSet()
    {
        for (int i = 0; i < mob_ani.Length; i++)
        {
            mob_ani[i].SetBool("isErase", false);
        }

        for (int i = 0; i < mobs.Length; i++)
        {
            spriteRenderers[i].sprite = mobs[i].GetComponent<MobBase>().sprites[0];
        }
    }
}
