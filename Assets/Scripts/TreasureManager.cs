using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    public GameObject[] DropItems;

    new SpriteRenderer renderer;
    public Sprite[] image;
    bool isContact = true;

    // アイテムのデータを保持する辞書
    Dictionary<int, string> itemInfo;

    // 敵がドロップするアイテムの辞書
    Dictionary<int, float> itemDropDict;

    float JewelRate;
    float CoinRate;
    float PoisonRate;
    float KeyRate;
    float SwordRate;
    float FirstAidKitRate = 5.0f;

    void Start()
    {
        switch (SelectManager.playerType)
        {
            case "Hatman":
                JewelRate = 20.0f;
                CoinRate = 20.0f;
                PoisonRate = 15.0f;
                KeyRate = 25.0f;
                SwordRate = 20.0f;
                break;
            case "Thief":
                JewelRate = 20.0f;
                CoinRate = 25.0f;
                PoisonRate = 15.0f;
                KeyRate = 20.0f;
                SwordRate = 20.0f;
                break;
            case "Warrior":
                JewelRate = 20.0f;
                CoinRate = 20.0f;
                PoisonRate = 15.0f;
                KeyRate = 20.0f;
                SwordRate = 25.0f;
                break;
            case "JonnySan":
                JewelRate = 25.0f;
                CoinRate = 20.0f;
                PoisonRate = 15.0f;
                KeyRate = 20.0f;
                SwordRate = 20.0f;
                break;
            case "ShimazuSan":
                JewelRate = 20.0f;
                CoinRate = 20.0f;
                PoisonRate = 5.0f;
                KeyRate = 20.0f;
                SwordRate = 20.0f;
                break;
            case "Cat":
                JewelRate = 20.0f;
                CoinRate = 20.0f;
                PoisonRate = 30.0f;
                KeyRate = 20.0f;
                SwordRate = 20.0f;
                break;
            case "UpotuKun":
                JewelRate = 20.0f;
                CoinRate = 20.0f;
                PoisonRate = 15.0f;
                KeyRate = 20.0f;
                SwordRate = 20.0f;
                break;
            case "Santa":
                JewelRate = 20.0f;
                CoinRate = 20.0f;
                PoisonRate = 15.0f;
                KeyRate = 20.0f;
                SwordRate = 20.0f;
                break;
            default:
                JewelRate = 20.0f;
                CoinRate = 20.0f;
                PoisonRate = 20.0f;
                KeyRate = 20.0f;
                SwordRate = 20.0f;
                break;
        }
        isContact = true;

        renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isContact)
        {
            if (other.gameObject.tag == "Player")
            {
                isContact = false;
                StartCoroutine("HideBox");
            }
        }
    }
    IEnumerator HideBox()
    {
        renderer.sprite = image[1];
        SoundManager.instance.PlaySE(6);
        yield return new WaitForSeconds(0.5f);
        ItemGenerate();
        gameObject.SetActive(false);
    }
    void ItemGenerate()
    {
        int itemId = GetDropItem();

        //int randomNumber = Random.Range(0, DropItems.Length);
        Instantiate(DropItems[itemId], this.gameObject.transform.position, transform.rotation);
    }


    int GetDropItem()
    {
        // 各種辞書の初期化
        InitializeDicts();

        // ドロップアイテムの抽選
        int itemId = Choose();

        // アイテムIDに応じたメッセージ出力
        if (itemId != 0)
        {
            string itemName = itemInfo[itemId];
            Debug.Log(itemName + " を入手した!");
        }
        else
        {
            Debug.Log("アイテムは入手できませんでした。");
        }
        return itemId;
    }

    void InitializeDicts()
    {
        itemInfo = new Dictionary<int, string>();
        itemInfo.Add(0, "Coin");
        itemInfo.Add(1, "Jewel");
        itemInfo.Add(2, "Key");
        itemInfo.Add(3, "Poison");
        itemInfo.Add(4, "Weapon");
        itemInfo.Add(5, "FirstAidKit");

        itemDropDict = new Dictionary<int, float>();
        itemDropDict.Add(0, CoinRate);
        itemDropDict.Add(1, JewelRate);
        itemDropDict.Add(2, KeyRate);
        itemDropDict.Add(3, PoisonRate);
        itemDropDict.Add(4, SwordRate);
        itemDropDict.Add(5, FirstAidKitRate);
    }

    int Choose()
    {
        // 確率の合計値を格納
        float total = 0;

        // 敵ドロップ用の辞書からドロップ率を合計する
        foreach (KeyValuePair<int, float> elem in itemDropDict)
        {
            total += elem.Value;
        }

        // Random.valueでは0から1までのfloat値を返すので
        // そこにドロップ率の合計を掛ける
        float randomPoint = Random.value * total;

        // randomPointの位置に該当するキーを返す
        foreach (KeyValuePair<int, float> elem in itemDropDict)
        {
            if (randomPoint < elem.Value)
            {
                Debug.Log("elem.key =" + elem.Key);
                return elem.Key;
            }
            else
            {
                randomPoint -= elem.Value;
            }
        }
        return 0;
    }
}