using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{

    public GameObject[] spawnObject;

    public float interval = 3;

    int xPosition;
    int yPosition;

    //int[] ArrayX = { 18, 3, 6, 25, 19, 10, 1, 31, 7, 33, 11, 20, 5, 8, 15, 26, 16, 30, 23, 7, 30, 23, 14, 19, 32, 14, 8, 23, 27, 26, 19, 15, 30, 12, 19, 7, 15, 2, 17, 9 };
    //int[] ArrayY = { 20, 12, 9, 9, 13, 8, 9, 21, 21, 16, 1, 9, 9, 19, 10, 11, 6, 25, 10, 8, 6, 18, 19, 25, 25, 24, 6, 24, 1, 9, 15, 18, 24, 1, 1, 9, 7, 10, 25, 3 };

    //[SerializeField]
    int EnemySum = 0;
    public static int EnemyVol;
    int[] ArrayX;
    int[] ArrayY;

    public GameObject Jewel;
    public GameObject Tresure;
    public GameObject Key;
    public GameObject Coin;
    public GameObject Sword;
    public GameObject Poison;
    public GameObject FirstAidKit;

    public MakeDungeon makeDungeon;

    private void Awake()
    {
        EnemySum = NewGame.Floor + 15;
        //EnemyVol = EnemySum;
        EnemyVol = (NewGame.Floor / 2) + 6;
        if(EnemyVol > spawnObject.Length)
        {
            EnemyVol = spawnObject.Length;
        }
        ArrayX = new int[EnemySum];
        ArrayY = new int[EnemySum];
    }
    /*
    private void Start()
    {
        //SpawnEnemy();
        
        for(int i = 0; i < 20; i++)
        {
            int X = Random.Range(1, 34);
            ArrayX[i] = X;
            Debug.Log(X);
        }
        for (int i = 0; i < 20; i++)
        {
            int Y = Random.Range(1, 26);
            ArrayY[i] = Y;
            //Debug.Log(Y);
        }
    }
    */

    void Update()
    {
        //transform.position = new Vector2(Random.Range(1, 15), Random.Range(1, 11));
        //xPosition = Random.Range(1, 15);
        //yPosition = Random.Range(1, 11);
    }

    public void StartSpawn()
    {
        StartCoroutine("SpawnEnemy");
    }

    public void StopSpawn()
    {
        StopCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            xPosition = Random.Range(3, 31);
            yPosition = Random.Range(3, 23);

            int randomNumber = Random.Range(0, EnemyVol);
            Instantiate(spawnObject[randomNumber], new Vector2(xPosition, yPosition), transform.rotation);


            //Debug.Log("SPAWN" + randomNumber);
            //Instantiate(spawnObject, new Vector2(xPosition,yPosition), transform.rotation);

            float wait = interval - (NewGame.Floor / 5);
            if (wait <= 0)
            {
                wait = 1.0f;
            }
            yield return new WaitForSeconds(wait);
        }
    }
    public void SetEnemy()
    {
        for (int i = 0; i < EnemySum; i++)
        {
            int X = Random.Range(3, 31);
            ArrayX[i] = X;
            //Debug.Log(X);
        }
        for (int i = 0; i < EnemySum; i++)
        {
            int Y = Random.Range(3, 23);
            ArrayY[i] = Y;
            //Debug.Log(Y);
        }
        for (int i = 0; i < EnemySum; i++)
        {
            int randomNumber = Random.Range(0, EnemyVol);
            Instantiate(spawnObject[randomNumber], new Vector2(ArrayX[i], ArrayY[i]), transform.rotation);


            //Debug.Log("SPAWN" + i);
        }
    }
    public void SetItem()
    {
        for(int i = 0; i < NewGame.ItemSum; i++)
        {
            StartCoroutine("SpawnItem");
        }
    }
    public void SetKey()
    {
        MakeDungeon.SetItemPos();
        Instantiate(Key, MakeDungeon.itemPos, transform.rotation);
    }
    public void SetSword()
    {
        MakeDungeon.SetItemPos();
        Instantiate(Sword, MakeDungeon.itemPos, transform.rotation);
    }
    public void SetCoin()
    {
        MakeDungeon.SetItemPos();
        Instantiate(Coin, MakeDungeon.itemPos, transform.rotation);
    }
    public void SetPoison()
    {
        MakeDungeon.SetItemPos();
        Instantiate(Poison, MakeDungeon.itemPos, transform.rotation);
    }
    public void SetJewel()
    {
        MakeDungeon.SetItemPos();
        Instantiate(Jewel, MakeDungeon.itemPos, transform.rotation);
    }
    public void SetFirstAidKit()
    {
        MakeDungeon.SetItemPos();
        Instantiate(FirstAidKit, MakeDungeon.itemPos, transform.rotation);
    }
    IEnumerator SpawnItem()
    {
        float wait = interval - (NewGame.Floor / 5);
        if (wait <= 0)
        {
            wait = 1.0f;
        }

        MakeDungeon.SetItemPos();
        Instantiate(Tresure, MakeDungeon.itemPos, transform.rotation);
        yield return new WaitForSeconds(wait);
    }
}