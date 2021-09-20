using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject Effect;
    GameObject player;
    static GameObject Obj;
    static BoxCollider2D bcollider;

    public static bool isPoison = false;
    public static bool isFirstAidKit = false;

    private void Awake()
    {
        //GameObject BarrierObj = GameObject.Find("Barrier");
        //BarrierObj.SetActive(false);
        //Debug.Log(BarrierObj);
        if(SelectManager.playerType == "Hatman")
        {
            bcollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        }
        else if(SelectManager.playerType == "Thief")
        {
            bcollider = GameObject.Find("Thief").GetComponent<BoxCollider2D>();
        }
        else if (SelectManager.playerType == "Warrior")
        {
            bcollider = GameObject.Find("Warrior").GetComponent<BoxCollider2D>();
        }
        else if (SelectManager.playerType == "JonnySan")
        {
            bcollider = GameObject.Find("Jonny-san").GetComponent<BoxCollider2D>();
        }
        else if (SelectManager.playerType == "ShimazuSan")
        {
            bcollider = GameObject.Find("Shimazu-san").GetComponent<BoxCollider2D>();
        }
        else if (SelectManager.playerType == "Cat")
        {
            bcollider = GameObject.Find("Cat").GetComponent<BoxCollider2D>();
        }
        else if (SelectManager.playerType == "UpotuKun")
        {
            bcollider = GameObject.Find("UpotuKun").GetComponent<BoxCollider2D>();
        }
        else if (SelectManager.playerType == "Santa")
        {
            bcollider = GameObject.Find("Santa").GetComponent<BoxCollider2D>();
        }
        else if (SelectManager.playerType == "Merchant")
        {
            bcollider = GameObject.Find("Merchant").GetComponent<BoxCollider2D>();
        }
        else if (SelectManager.playerType == "Witch")
        {
            bcollider = GameObject.Find("Witch").GetComponent<BoxCollider2D>();
        }
        else if (SelectManager.playerType == "JackO")
        {
            bcollider = GameObject.Find("JackOLantern").GetComponent<BoxCollider2D>();
        }
        else if (SelectManager.playerType == "BunnyGirl")
        {
            bcollider = GameObject.Find("BunnyGirl").GetComponent<BoxCollider2D>();
        }
    }

    public void GetItem()
    {
        if (this.gameObject.CompareTag("Jewel"))
        {
            if (!NextFloor.isFever)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                Obj = (GameObject)Instantiate(Effect, this.transform.position, Quaternion.identity);
                Obj.transform.parent = player.transform;
                SoundManager.instance.PlaySE(3);
                Destroy(this.gameObject);
                NextFloor.isFever = true;
                Move.UpSpeed = 0;// 16.0f - NewGame.characterSpeed;
                bcollider.size = new Vector2(1.9f, 1.9f);
            }
            else
            {
                NextFloor.countTime = 0;
                NewGame.SCORE += 50;
                SoundManager.instance.PlaySE(3);
                Destroy(this.gameObject);
            }
        }
        else if (this.gameObject.CompareTag("Coin"))
        {
            NewGame.SCORE += 100;
            SoundManager.instance.PlaySE(7);
            Destroy(this.gameObject);
        }
        else if (this.gameObject.CompareTag("Key"))
        {
            if (!NextFloor.isGetKey)
            {
                NextFloor.isGetKey = true;
                SoundManager.instance.PlaySE(8);
                Destroy(this.gameObject);
            }
            else
            {
                NewGame.SCORE += 50;
                SoundManager.instance.PlaySE(8);
                Destroy(this.gameObject);
            }
        }
        else if (this.gameObject.CompareTag("Poison"))
        {
            if (!NextFloor.isFever)
            { 
                isPoison = true;
                NewGame.Life -= 30;
                SoundManager.instance.PlaySE(9);
                Enemy.LifeLost();
            }
            else
            {
                SoundManager.instance.PlaySE(12);
            }
            Destroy(this.gameObject);

        }
        else if (this.gameObject.CompareTag("Sword"))
        {
            NewGame.POW += 10;
            SoundManager.instance.PlaySE(5);
            Destroy(this.gameObject);
        }
        else if (this.gameObject.CompareTag("FirstAidKit"))
        {
            isFirstAidKit = true;
            if(NewGame.MAXLIFE - NewGame.Life >= 200)
            {
                NewGame.Life += 200;
            }
            else
            {
                NewGame.Life += NewGame.MAXLIFE - NewGame.Life;
            }
            SoundManager.instance.PlaySE(11);
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("item error");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GetItem();
        }
    }
    public static void DestroyEffect()
    {
        bcollider.size = new Vector2(0.1f, 0.1f);
        Destroy(Obj);
        Debug.Log("DestroyEffect");

    }
}
