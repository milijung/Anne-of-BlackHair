using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] SideMobs;
    public GameObject[] Item;
    public static int MobStartNum = 0; // SpawnManager 실행 전에 Mob이 등장하는 것 방지
    int MobCreateNum = 0;
    int ItemCreateTerm; // Mob이 ItemCreateTerm번 생성될 때마다 Item 1개 생성

    public float startNum_Create, finalNum_Create; // mob 등장간의 시간 간격. startNum: 최소 시간 간격, finalNum: 최대 시간 간격
    private void Start()
    {
        StartCoroutine(CreateMob());
    }
    IEnumerator CreateMob()
    {
        ItemCreateTerm = Random.Range(3, 6);
        yield return new WaitForSeconds(3f); // 시작하고 3초 후부터 Mob 등장
        while (true)
        {
            if (GameManager.isPlay || GameManager.gameOver)
            {
                if (MobCreateNum != ItemCreateTerm)
                {
                    SideMobs[DeactiveMob()].SetActive(true); // 비활성화된 Mob들 중에서 1개를 활성화
                    yield return new WaitForSeconds(Random.Range(startNum_Create, finalNum_Create));
                    MobCreateNum++;
                }
                else
                {
                    Item[DeactiveItem()].SetActive(true); // 비활성화된 Item들 중에서 1개를 활성화
                    yield return new WaitForSeconds(Random.Range(0.4f, 0.7f)); // 아이템이 생성된 후, 0.4초-0.7초 지나고 바로 Mob이 생성
                    SideMobs[DeactiveMob()].SetActive(true);
                    yield return new WaitForSeconds(Random.Range(startNum_Create, finalNum_Create));
                    ItemCreateTerm = Random.Range(3, 6); // 새로운 ItemCreateTerm 정하기
                    MobCreateNum = 0;
                }
                if (MobStartNum == 0)
                {
                    MobStartNum++;
                }
            }
            else
            {
                yield return new WaitForSeconds(GameManager.instance.Count.Length);
            }
            yield return null;
        }
    }


    int DeactiveMob() // 비활성화된 Mob중에서 선택하는 함수
    {
        List<int> num = new List<int>();
        for(int i = 0; i < SideMobs.Length; i++)
        {
            if (!SideMobs[i].activeSelf) // 비활성화된 Mob의 인덱스를 List에 추가
            {
                num.Add(i);
            }
        }
        int x = 0;
        if (num.Count > 0)
        {
            x = num[Random.Range(0, num.Count)]; 
        }
        return x; // 비활성화된 Mob의 인덱스중 1개를 반환
    }
    int DeactiveItem() // 비활성화된 아이템 중에서 선택하는 함수
    {
        List<int> num = new List<int>();
        for (int i = 0; i < Item.Length; i++)
        {
            if (!Item[i].activeSelf) // 비활성화된 서브캐릭터의 인덱스를 List에 추가
            {
                num.Add(i);
            }
        }
        int x = 0;
        if (num.Count > 0)
        {
            x = num[Random.Range(0, num.Count)];
        }
        return x; // 비활성화된 서브캐릭터의 인덱스중 1개를 반환
    }

    GameObject CreateObj(GameObject obj, Transform parent)
    {
        GameObject copy = Instantiate(obj); // 매개변수로 받은 게임 오브젝트를 복제
        copy.transform.SetParent(parent);
        copy.SetActive(false);
        return copy;
    }
}
