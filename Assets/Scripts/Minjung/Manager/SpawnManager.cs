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

    public Animator player_animator;
    public Sun sun;
    public GameObject[] SideMobs;
    public GameObject[] Item;
    public GameObject[] Tree;
    public GameObject[] House;
    GameObject[][] Road = new GameObject[4][];
    public GameObject[] RoadOne, RoadTwo, RoadThree, RoadFour;
    public GameObject[] BerryBox;
    public GameObject[] BackgroundScrollImage;
    public static int MobStartNum = 0; // SpawnManager ���� ���� Mob�� �����ϴ� �� ����
    public static bool isforest = false;
    public static int Speed_Num;

    public int objCnt = 4;
    int x_Back, x_forest;

    float[][] Back = new float[4][];
    float[][] Mob = new float[4][];
    float[] Obstacle;
    float[][] ItemTerm = new float[4][]; // order: Item Berry color
    float[] cat = { 2, 3, 5, 5 }; // cat_run create Term

    private void Awake()
    {
        Road[0] = RoadOne;
        Road[1] = RoadTwo;
        Road[2] = RoadThree;
        Road[3] = RoadFour;

        Back[0] = new float[] { 0.2f, 9.8f, 0.7f, 9.3f };
        Back[1] = new float[] { 0.12f, 9.88f, 0.4f, 9.6f };
        Back[2] = new float[] { 0.08f, 9.92f, 0.28f, 9.75f };
        Back[3] = new float[] { 0.035f, 9f, 0.17f, 9f };

        Mob[0] = new float[] { 1, 2 };
        Mob[1] = new float[] { 0.6f, 0.8f };
        Mob[2] = new float[] { 0.5f, 0.8f };
        Mob[3] = new float[] { 0.2f, 0.5f };

        Obstacle = new float[] { 1, 0.7f, 0.5f, 0.2f };
        ItemTerm[0] = new float[] { 10, 0.3f, 6.5f };
        ItemTerm[1] = new float[] { 9, 0.2f, 6f };
        ItemTerm[2] = new float[] { 8, 0.15f, 5.5f };
        ItemTerm[3] = new float[] { 4, 0.06f, 2f };



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
            BerryPool.Add(CreateObj(BerryBox[3],transform));
    }
    
    private void Start()
    {
        x_Back = x_forest = Speed_Num = 0;
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
            if(GameManager.speedIndex!=3) StartCoroutine(BackgroundScroll());
            StartCoroutine(CreateBerry());
        }
    }
    IEnumerator CreateBerry()
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                
                for(int i=0;i<5;i++)
                {
                    BerryPool[i].SetActive(true);
                    yield return new WaitForSeconds(ItemTerm[GameManager.speedIndex][1]);
                }
                if (!Sun.sunRise)
                {
                    yield return new WaitForSeconds(ItemTerm[GameManager.speedIndex][1]);
                    Road[2][0].SetActive(true);
                }
                break;
            }
            yield return null;
        }
        StopCoroutine(CreateBerry());
    }
    IEnumerator BackgroundScroll()
    {
        x_forest++;
        while (true)
        {
            if (GameManager.isPlay)
            {
                BackgroundScrollImage[0].SetActive(true);
                while (isforest || GameManager.speedIndex == 3)
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
    IEnumerator CreateBack() // Ǯ,����,�� ����
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
    IEnumerator CreateMob() // ��������� ����
    {
        yield return new WaitForSeconds(2f); // �����ϰ� 3�� �ĺ��� Mob ����
        for (int i = 0; i < BerryBox.Length; i++)
        {
            BerryBox[i].SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        while (true)
        {
            if (GameManager.isPlay)
            {
                if (!isforest)
                {
                    float time = Random.Range(Mob[GameManager.speedIndex][0], Mob[GameManager.speedIndex][1]); // ����������� �����ϴ� �ð� ����
                    SideMobs[DeactiveMob()].SetActive(true); // ��Ȱ��ȭ�� Mob�� �߿��� 1���� Ȱ��ȭ
                    yield return new WaitForSeconds(time);

                    if (MobStartNum == 0)
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
    IEnumerator CreateItem() // ������ �ָӴ�
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                if (BackgroundScrollImage[1].activeSelf)
                {
                    while (!BackgroundScrollImage[0].activeSelf)
                    {
                        Item[0].SetActive(true); // �������ָӴ� Ȱ��ȭ
                        yield return new WaitForSeconds(ItemTerm[GameManager.speedIndex][0]); // �������� ������ ��, ���� ������ ���� �������� ����
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
    IEnumerator CreateColor()
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                if (player_animator.GetInteger("State") >= 4)
                {
                    Item[2].SetActive(true);
                    yield return new WaitForSeconds(ItemTerm[GameManager.speedIndex][2]+1f);
                }
                else
                {
                    Item[1].SetActive(true);
                    yield return new WaitForSeconds(ItemTerm[GameManager.speedIndex][2]);
                }
            }
            else
            {
                yield return new WaitForSeconds(GameManager.instance.Count.Length);
            }
            yield return null;
        }
    }
    IEnumerator CreateRoad() // ��ֹ� ����
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                if (Sun.sunRise) // ��
                {
                    if (isforest)// ��
                    {
                        if (BackgroundScrollImage[0].transform.position.y < -4)
                            Road[0][DeactiveRoad_afternoon()].SetActive(true);
                    }
                    else // ����
                    {
                        if (BackgroundScrollImage[1].transform.position.y < -4)
                            Road[1][DeactiveRoad_afternoon()].SetActive(true);
                    }
                    yield return new WaitForSeconds(Obstacle[GameManager.speedIndex]);
                }
                else // ��
                {
                    if (isforest)// ��
                    {
                        yield return null;
                    }
                    else // ����
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
    int DeactiveMob() // ��Ȱ��ȭ�� Mob�߿��� �����ϴ� �Լ�
    {
        List<int> num = new List<int>();
        for (int i = 0; i < SideMobs.Length; i++)
        {
            if (!SideMobs[i].activeSelf) // ��Ȱ��ȭ�� Mob�� �ε����� List�� �߰�
            {
                num.Add(i);
            }
        }
        int x = 0;
        if (num.Count > 0)
        {
            x = num[Random.Range(0, num.Count)];
        }
        return x; // ��Ȱ��ȭ�� Mob�� �ε����� 1���� ��ȯ
    }

    int DeactiveRoad_afternoon() // ��Ȱ��ȭ�� ��ֹ��߿��� �����ϴ� �Լ�
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
    int DeactiveRoad_night() // ��Ȱ��ȭ�� ��ֹ��߿��� �����ϴ� �Լ�
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

