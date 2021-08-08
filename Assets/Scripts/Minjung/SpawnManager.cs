using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    List<GameObject> TreePool = new List<GameObject>();
    List<GameObject> HousePool = new List<GameObject>();
    List<GameObject> BackGround = new List<GameObject>();
    List<int> StreetLightNum = new List<int>();
    public GameObject[] SideMobs;
    public GameObject[] Item;
    public GameObject[] Tree;
    public GameObject[] House;
    public GameObject[] Road;
    public static int MobStartNum = 0; // SpawnManager 실행 전에 Mob이 등장하는 것 방지
    public int objCnt = 4;
    int x_Back;

    public float startNum_Create, finalNum_Create; // mob 등장간의 시간 간격. startNum: 최소 시간 간격, finalNum: 최대 시간 간격
    private void Awake()
    {
        for(int q = 0; q < objCnt; q++)
        {
            for(int i = 0; i < Tree.Length; i++)
            {
                TreePool.Add(CreateObj(Tree[i], transform));
            }
            for(int i=0; i < House.Length; i++)
            {
                HousePool.Add(CreateObj(House[i], transform));
            }
        }
        for(int i = 0; i < TreePool.Count; i++)
        {
            BackGround.Add(TreePool[i]);
        }
        for (int i = 0; i < HousePool.Count; i++)
        {
            BackGround.Add(HousePool[i]);
            if (HousePool[i].name.Contains("streetlight"))
            {
                StreetLightNum.Add(TreePool.Count + i);
            }
        }
    }
    private void Start()
    {
        x_Back = 0;
        StartCoroutine(CreateMob());
        StartCoroutine(CreateRoad());
        StartCoroutine(CreateBack());
        StartCoroutine(CreateItem());
        StartCoroutine(CreateColor());
    }
    IEnumerator CreateBack() // 풀,나무,집 생성
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            if (GameManager.isPlay)
            {
                if (x_Back <= TreePool.Count)
                {
                    BackGround[x_Back].SetActive(true);
                    yield return new WaitForSeconds(0.2f);
                    x_Back++;
                    if (x_Back == TreePool.Count)
                    {
                        yield return new WaitForSeconds(9.8f);
                    }
                }
                else
                {
                    if (StreetLightNum.Contains(x_Back))
                    {
                        BackGround[x_Back].SetActive(true);
                        BackGround[x_Back + 1].SetActive(true);
                        x_Back += 2;
                    }
                    else
                    {
                        BackGround[x_Back].SetActive(true);
                        x_Back++;
                    }
                    yield return new WaitForSeconds(0.7f);
                    
                    if (x_Back == BackGround.Count)
                    {
                        yield return new WaitForSeconds(9.3f);
                        x_Back = 0;
                    }
                }

            }
            else
            {
                yield return new WaitForSeconds(GameManager.instance.Count.Length);
            }
            yield return null;
        }
    }
    IEnumerator CreateMob() // 마을사람들 생성
    {
        yield return new WaitForSeconds(3f); // 시작하고 3초 후부터 Mob 등장
        while (true)
        {
            if (GameManager.isPlay)
            {
                float time = Random.Range(startNum_Create, finalNum_Create); // 마을사람들이 등장하는 시간 간격
                SideMobs[DeactiveMob()].SetActive(true); // 비활성화된 Mob들 중에서 1개를 활성화
                yield return new WaitForSeconds(time);
                
                if (MobStartNum ==0)
                    MobStartNum++;
            }
            else
            {
                yield return new WaitForSeconds(GameManager.instance.Count.Length);
            }
            yield return null;
        }
    }
    IEnumerator CreateItem() // 아이템 주머니:  2초마다 생성
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                Item[0].SetActive(true); // 아이템주머니 활성화
                yield return new WaitForSeconds(2); // 아이템이 생성된 후, 몇초 지나고 다음 아이템이 생성
            }
            else
            {
                yield return new WaitForSeconds(GameManager.instance.Count.Length);
            }
            yield return null;
        }
    }
    IEnumerator CreateColor() // 탈색약 -> 염색약 순으로 생성
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                Item[1].SetActive(true); // 탈색약 활성화 후에 염색약 활성화
                yield return new WaitForSeconds(1);
                Item[2].SetActive(true);
                yield return new WaitForSeconds(3);
            }
            else
            {
                yield return new WaitForSeconds(GameManager.instance.Count.Length);
            }
            yield return null;
        }
    }
    IEnumerator CreateRoad() // 장애물 생성
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                Road[DeactiveRoad()].SetActive(true);
                yield return new WaitForSeconds(Random.Range(1, 3));
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
    
    int DeactiveRoad() // 비활성화된 장애물중에서 선택하는 함수
    {
        List<int> num = new List<int>();
        for(int i=0; i< Road.Length; i++)
        {
            if (!Road[i].activeSelf)
            {
                num.Add(i);
            }
        }
        int x = 0;
        if(num.Count > 0)
        {
            x = num[Random.Range(0, num.Count)];
        }
        return x;
    }
    GameObject CreateObj(GameObject obj, Transform parent)
    {
        GameObject copy = Instantiate(obj);
        copy.transform.SetParent(parent);
        copy.SetActive(false);
        return copy;
    }
}
