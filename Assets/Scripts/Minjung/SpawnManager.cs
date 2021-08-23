using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] SideMobs;
    public GameObject[] Item;
    public GameObject[] BackGround;
    public GameObject[] Road;
    public static int MobStartNum = 0; // SpawnManager 실행 전에 Mob이 등장하는 것 방지
   // int MobCreateNum = 0;
   //int ItemCreateTerm; // Mob이 ItemCreateTerm번 생성될 때마다 Item 1개 생성
    int BackCreateNum = 0; // 풀,나무,집 생성관련 수

    public float startNum_Create, finalNum_Create; // mob 등장간의 시간 간격. startNum: 최소 시간 간격, finalNum: 최대 시간 간격
    private void Start()
    {
        StartCoroutine(CreateMob());
        StartCoroutine(CreateRoad());
        StartCoroutine(CreateItem());
        StartCoroutine(CreateColor());
    }
    IEnumerator CreateMob() // 마을사람들 생성
    {
        int BackNum = Random.Range(1, 4); // 풀, 나무, 집 생성 term
        yield return new WaitForSeconds(3f); // 시작하고 3초 후부터 Mob 등장
        while (true)
        {
            if (GameManager.isPlay)
            {
                float time = Random.Range(startNum_Create, finalNum_Create); // 마을사람들이 등장하는 시간 간격
                SideMobs[DeactiveMob()].SetActive(true); // 비활성화된 Mob들 중에서 1개를 활성화
                BackCreateNum++;
                yield return new WaitForSeconds(time/2);
                if (BackCreateNum == BackNum) // 마을사람들이 BackNum번 생성될때마다 풀/나무/집 중 1개 활성화
                {
                    BackGround[DeactiveBack()].SetActive(true);
                    yield return new WaitForSeconds(time / 2);
                    BackCreateNum = 0;
                    BackNum = Random.Range(1, 3);
                }
                else
                {
                    yield return new WaitForSeconds(time / 2);
                }
                if (MobStartNum ==0)
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
    IEnumerator CreateItem() // 아이템 주머니:  2초마다 생성
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                Item[0].SetActive(true); // 아이템주머니 활성화
                yield return new WaitForSeconds(2); // 아이템이 생성된 후, 몇초 지나고 다음 아이템이 생성
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
            yield return null;
        }
    }
    IEnumerator CreateRoad() // 장애물 생성
    {
        while (true)
        {
            if (GameManager.score %50 == 0 && GameManager.isPlay) // 점수가 50의 배수가 될때마다 생성
            {
                int score = GameManager.score;
                int num = Random.Range(15, 20); // 언제까지 장애물이 등장할지 결정
                while (GameManager.score <= score + num) // 점수가 50n + num이 될때까지 장애물 등장
                {
                    Road[DeactiveRoad()].SetActive(true);
                    yield return new WaitForSeconds(Random.Range(1, 2));

                }
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
    int DeactiveBack()
    {
        List<int> num = new List<int>();
        for(int i=0; i < BackGround.Length; i++)
        {
            if (!BackGround[i].activeSelf)
            {
                num.Add(i);
            }
        }
        int x = 0;
        if(num.Count > 0)
        {
            x = num[Random.Range(0, num.Count)];
        }
        return x; // 비활성화된 BackGround의 인덱스중 1개를 반환
    }
    
    int DeactiveRoad()
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
        GameObject copy = Instantiate(obj); // 매개변수로 받은 게임 오브젝트를 복제
        copy.transform.SetParent(parent);
        copy.SetActive(false);
        return copy;
    }
}
