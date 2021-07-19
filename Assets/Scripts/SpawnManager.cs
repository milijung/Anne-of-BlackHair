using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] SideMobs;
    public GameObject[] Item;
    public static int MobStartNum = 0; // SpawnManager 실행 전에 Mob이 등장하는 것 방지

    public float startNum, finalNum; // mob 등장간의 시간 간격. startNum: 최소 시간 간격, finalNum: 최대 시간 간격
    private void Start()
    {
        StartCoroutine(CreateMob());
        StartCoroutine(CreateItem());
    }
    IEnumerator CreateMob()
    {
        yield return new WaitForSeconds(3f); // 시작하고 3초 후부터 Mob 등장
        while (true)
        {
            SideMobs[DeactiveMob()].SetActive(true); // 비활성화된 Mob들 중에서 1개를 활성화
            yield return new WaitForSeconds(Random.Range(startNum, finalNum)); 
            if (MobStartNum == 0)
            {
                MobStartNum++;
            }
        }
    }
    IEnumerator CreateItem()
    {
        yield return new WaitForSeconds(5f); // 시작하고 5초 후부터 Item 등장
        while (true)
        {
            Item[DeactiveItem()].SetActive(true); // 비활성화된 Item들 중에서 1개를 활성화
            yield return new WaitForSeconds(Random.Range(startNum + 5, finalNum + 5));
            if (MobStartNum == 0)
            {
                MobStartNum++;
            }
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
