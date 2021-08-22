using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMob_Controller : MonoBehaviour
{
    public Animator[] mob_ani;
    public SpriteRenderer[] spriteRenderers;
    public BoxCollider[] mob_collider;
    public GameObject[] mobs;

    //마녀의 열매 사용시 빙글빙글 애니메이션 ON
    public void Set()
    {
        //for문은 애니메이션 있는 서브캐릭터에만 적용
        for(int i = 0; i < mob_ani.Length; i++)
        {
            mob_ani[i].SetBool("isErase", true);
        }

        //애니메이션 없는 애들은 이미지만 대체
        for(int i = 0; i < mobs.Length; i++)
        {
            spriteRenderers[i].sprite = mobs[i].GetComponent<MobBase>().sprites[2];
        }
        for(int i=0;i<mob_collider.Length;i++)
            mob_collider[i].enabled = false;

        Invoke("UnSet", 2f);    //마녀의 열매 효과 2초동안 지속
    }

    //마녀의 열매 효과 해제
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
        for (int i = 0; i < mob_collider.Length; i++)
            mob_collider[i].enabled = true;
    }
}
