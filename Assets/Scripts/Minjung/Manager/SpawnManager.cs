using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    List<GameObject> TreePool = new List<GameObject>();
    List<GameObject> HousePool = new List<GameObject>();
    List<GameObject> BackGround = new List<GameObject>();
    List<GameObject> BerryPool = new List<GameObject>();
    List<int> StreetLightNum = new List<int>();

    public Sun sun;
    public GameObject[] SideMobs;
    public GameObject[] Item;
    public GameObject[] Tree;
    public GameObject[] House;
    GameObject[][] Road = new GameObject[4][];
    public GameObject[] RoadOne, RoadTwo, RoadThree, RoadFour;
    public GameObject[] BerryBox;
    public GameObject[] BackgroundScrollImage;
    public static int MobStartNum = 0; // SpawnManager 실행 전에 Mob이 등장하는 것 방지
    public static bool isforest = false;
    public static int Speed_Num;

    public int objCnt = 4;
    int x_Back, x_forest, x_Berry;

    float[][] Back = new float[3][];
    float[][] Mob = new float[3][];
    float[] Obstacle;
    float[][] ItemTerm = new float[3][]; // order: Item Berry color
    float[] cat = { 2, 3, 5 }; // cat_run create Term

    private void Awake()
    {
        Road[0] = RoadOne;
        Road[1] = RoadTwo;
        Road[2] = RoadThree;
        Road[3] = RoadFour;

        Back[0] = new float[] { 0.2f, 9.8f, 0.7f, 9.3f };
        Back[1] = new float[] { 0.13f, 9.95f, 0.4f, 9.6f };
        Back[2] = new float[] { 0.09f, 9.91f, 0.28f, 9.72f };

        Mob[0] = new float[] { 1, 2 };
        Mob[1] = new float[] { 0.6f, 0.8f };
        Mob[2] = new float[] { 0.5f, 0.8f };

        Obstacle = new float[] { 1, 0.7f, 0.5f };
        ItemTerm[0] = new float[] { 10, 0.3f, 7 };
        ItemTerm[1] = new float[] { 9, 0.15f, 6.5f };
        ItemTerm[2] = new float[] { 8, 0.1f, 6 };



        for (int q = 0; q < objCnt; q++)
        {
            for (int i = 0; i < Tree.Length; i++)
            {
                TreePool.Add(CreateObj(Tree[i], transform));
            }
            for (int i = 0; i < House.Length; i++)
            {
                HousePool.Add(CreateObj(House[i], transform));
            }
        }
        for (int i = 0; i < TreePool.Count; i++)
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
        for (int i = 0; i < 5; i++)
            BerryPool.Add(CreateObj(BerryBox[3], transform));
    }
    private void Start()
    {
        x_Back = x_forest = x_Berry = Speed_Num = 0;
        isforest = false;
        StartCoroutine(CreateBack());
        StartCoroutine(CreateMob());
        StartCoroutine(CreateRoad());
        StartCoroutine(CreateItem());
        StartCoroutine(CreateColor());
    }
    private void Update()
    {
        if (isforest && x_forest == 0)
        {
            StartCoroutine(BackgroundScroll());
        }
    }
    IEnumerator BackgroundScroll()
    {
        x_forest++;
        while (true)
        {
            if (GameManager.isPlay)
            {
                BackgroundScrollImage[0].SetActive(true);
                while (isforest)
                {
                    yield return null;
                }
                BackgroundScrollImage[1].SetActive(true);
                break;
            }
            yield return null;
        }
        StopCoroutine(BackgroundScroll());

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
                    isforest = true;
                    BackGround[x_Back].SetActive(true);
                    yield return new WaitForSeconds(Back[GameManager.speedIndex][0]);
                    x_Back++;
                    if (x_Back == TreePool.Count)
                    {
                        isforest = false;
                        yield return new WaitForSeconds(Back[GameManager.speedIndex][1]);
                    }
                }
                else
                {
                    isforest = false;
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
                    yield return new WaitForSeconds(Back[GameManager.speedIndex][2]);

                    if (x_Back == BackGround.Count)
                    {
                        yield return new WaitForSeconds(Back[GameManager.speedIndex][3]);
                        x_Back = 0;
                        if (Speed_Num < 5) { Speed_Num++; }
                        x_forest = 0;
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
        yield return new WaitForSeconds(2f); // 시작하고 3초 후부터 Mob 등장
        for (int i = 0; i < BerryBox.Length; i++)
        {
            BerryBox[i].SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        while (true)
        {
            if (GameManager.isPlay && !isforest&& !BackgroundScrollImage[0].activeSelf)
            {
                float time = Random.Range(Mob[GameManager.speedIndex][0], Mob[GameManager.speedIndex][1]); // 마을사람들이 등장하는 시간 간격
                SideMobs[DeactiveMob()].SetActive(true); // 비활성화된 Mob들 중에서 1개를 활성화
                yield return new WaitForSeconds(time);

                if (MobStartNum == 0)
                    MobStartNum++;
            }
            else
            {
                yield return new WaitForSeconds(GameManager.instance.Count.Length);
            }
            yield return null;
        }
    }
    IEnumerator CreateItem() // 아이템 주머니
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                if (!isforest)
                {
                    x_Berry = 0;
                    Item[0].SetActive(true); // 아이템주머니 활성화
                    yield return new WaitForSeconds(ItemTerm[GameManager.speedIndex][0]); // 아이템이 생성된 후, 몇초 지나고 다음 아이템이 생성
                }
                else
                {
                    if (x_Berry < 5)
                    {
                        BerryPool[x_Berry].SetActive(true);
                        x_Berry++;
                        yield return new WaitForSeconds(ItemTerm[GameManager.speedIndex][1]);
                    }
                    else
                        yield return null;
                }

            }
            else
            {
                yield return new WaitForSeconds(GameManager.instance.Count.Length);
            }
            yield return null;
        }
    }
    IEnumerator CreateColor()
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                Item[Random.Range(1, 3)].SetActive(true);
                yield return new WaitForSeconds(ItemTerm[GameManager.speedIndex][2]);
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
                if (Sun.sunRise) // 낮
                {
                    if (isforest)// 숲
                    {
                        if (BackgroundScrollImage[0].transform.position.y < -4)
                            Road[0][DeactiveRoad_afternoon()].SetActive(true);
                    }
                    else // 마을
                    {
                        if (BackgroundScrollImage[1].transform.position.y < -4)
                            Road[1][DeactiveRoad_afternoon()].SetActive(true);
                    }
                    yield return new WaitForSeconds(Obstacle[GameManager.speedIndex]);
                }
                else // 밤
                {
                    if (isforest)// 숲
                    {
                        if (BackgroundScrollImage[0].transform.position.y < -4)
                            Road[2][DeactiveRoad_night()].SetActive(true);
                        yield return new WaitForSeconds(Obstacle[GameManager.speedIndex]);
                    }
                    else // 마을
                    {
                        if (BackgroundScrollImage[1].transform.position.y < -4)
                            Road[3][DeactiveRoad_night()].SetActive(true);
                        yield return new WaitForSeconds(cat[GameManager.speedIndex]);
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
    int DeactiveMob() // 비활성화된 Mob중에서 선택하는 함수
    {
        List<int> num = new List<int>();
        for (int i = 0; i < SideMobs.Length; i++)
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

    int DeactiveRoad_afternoon() // 비활성화된 장애물중에서 선택하는 함수
    {
        List<int> num = new List<int>();
        if (isforest)
        {
            for (int i = 0; i < Road[0].Length; i++)
            {
                if (!Road[0][i].activeSelf)
                {
                    num.Add(i);
                }
            }
        }
        else
        {
            for (int i = 0; i < Road[1].Length; i++)
            {
                if (!Road[1][i].activeSelf)
                {
                    num.Add(i);
                }
            }
        }
        int x = 0;
        if (num.Count > 0)
        {
            x = num[Random.Range(0, num.Count)];
        }
        return x;
    }
    int DeactiveRoad_night() // 비활성화된 장애물중에서 선택하는 함수
    {
        List<int> num = new List<int>();
        if (isforest)
        {
            for (int i = 0; i < Road[2].Length; i++)
            {
                if (!Road[2][i].activeSelf)
                {
                    num.Add(i);
                }
            }
        }
        else
        {
            for (int i = 0; i < Road[3].Length; i++)
            {
                if (!Road[3][i].activeSelf)
                {
                    num.Add(i);
                }
            }
        }
        int x = 0;
        if (num.Count > 0)
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

