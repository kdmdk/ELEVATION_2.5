using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    bool WarriorSelect = false;
    bool HatmanSelect = false;
    bool ThiefSelect = false;

    bool JonnySanSelect = false;
    bool ShimazuSanSelect = false;
    bool CatSelect = false;

    bool UpotuKunSelect = false;
    bool SantaSelect = false;

    bool MerchantSelect = false;
    bool WitchSelect = false;
    bool JackOSelect = false;
    bool BunnyGirlSelect = false;

    public static string playerType = "Hatman";

    public GameObject player;
    public GameObject thief;
    public GameObject warrior;

    public GameObject score_object = null;
    public GameObject gold_object = null;

    private void Start()
    {
        Text score_text = score_object.GetComponent<Text>();
        score_text.text = "" + PlayerPrefs.GetInt("HIGH-SCORE",NewGame.HighScore);

        Text gold_text = gold_object.GetComponent<Text>();
        gold_text.text = "" + PlayerPrefs.GetInt("GOLD-POINT", NewGame.GOLD);

        StartCoroutine(SavePanel());
    }
    private void Update()
    {
        Text gold_text = gold_object.GetComponent<Text>();
        gold_text.text = "" + PlayerPrefs.GetInt("GOLD-POINT", NewGame.GOLD);

        if (WarriorSelect)
        {
            NewGame.reLife();
            NewGame.refloor();
            NewGame.characterSpeed = 7.0f;
            NewGame.sield = 2;
            NewGame.FeverTime = 6.0f;
            NewGame.MAXLIFE = 200;
            NewGame.Life = 200;
            NewGame.POW = 30;
            NewGame.ItemSum = 4;
            SoundManager.instance.PlaySE(1);
            playerType = "Warrior";
            SceneManager.LoadScene("Stage1");

           
        }
        else if (HatmanSelect)
        {
            NewGame.reLife();
            NewGame.refloor();
            NewGame.characterSpeed = 8.0f;
            NewGame.sield = 0;
            NewGame.FeverTime = 8.0f;
            NewGame.MAXLIFE = 200;
            NewGame.Life = 200;
            NewGame.POW = 20;
            NewGame.ItemSum = 4;
            SoundManager.instance.PlaySE(1);
            playerType = "Hatman";
            SceneManager.LoadScene("Stage1");
            
        }
        else if (ThiefSelect)
        {
            NewGame.reLife();
            NewGame.refloor();
            NewGame.characterSpeed = 10.0f;
            NewGame.sield = 0;
            NewGame.FeverTime = 6.0f;
            NewGame.MAXLIFE = 100;
            NewGame.Life = 100;
            NewGame.POW = 15;
            NewGame.ItemSum = 4;
            SoundManager.instance.PlaySE(1);
            playerType = "Thief";
            SceneManager.LoadScene("Stage1");
            
        }
        else if (JonnySanSelect)
        {
            NewGame.reLife();
            NewGame.refloor();
            NewGame.characterSpeed = 9.0f;
            NewGame.sield = 1;
            NewGame.FeverTime = 7.0f;
            NewGame.MAXLIFE = 500;
            NewGame.Life = 500;
            NewGame.POW = 30;
            NewGame.ItemSum = 5;
            SoundManager.instance.PlaySE(1);
            playerType = "JonnySan";
            SceneManager.LoadScene("Stage1");
            
        }
        else if (ShimazuSanSelect)
        {
            NewGame.reLife();
            NewGame.refloor();
            NewGame.characterSpeed = 8.0f;
            NewGame.sield = 3;
            NewGame.FeverTime = 6.0f;
            NewGame.MAXLIFE = 350;
            NewGame.Life = 350;
            NewGame.POW = 40;
            NewGame.ItemSum = 5;
            SoundManager.instance.PlaySE(1);
            playerType = "ShimazuSan";
            SceneManager.LoadScene("Stage1");
            
        }
        else if (CatSelect)
        {
            NewGame.reLife();
            NewGame.refloor();
            NewGame.characterSpeed = 11.0f;
            NewGame.sield = 0;
            NewGame.FeverTime = 9.0f;
            NewGame.MAXLIFE = 999;
            NewGame.Life = 999;
            NewGame.POW = 99;
            NewGame.ItemSum = 6;
            SoundManager.instance.PlaySE(1);
            playerType = "Cat";
            SceneManager.LoadScene("Stage1");
            
        }
        else if (UpotuKunSelect)
        {
            NewGame.reLife();
            NewGame.refloor();
            NewGame.characterSpeed = 9.0f;
            NewGame.sield = 1;
            NewGame.FeverTime = 6.0f;
            NewGame.MAXLIFE = 300;
            NewGame.Life = 300;
            NewGame.POW = 60;
            NewGame.ItemSum = 3;
            SoundManager.instance.PlaySE(1);
            playerType = "UpotuKun";
            SceneManager.LoadScene("Stage1");
            
        }
        else if (SantaSelect)
        {
            NewGame.reLife();
            NewGame.refloor();
            NewGame.characterSpeed = 9.0f;
            NewGame.sield = 0;
            NewGame.FeverTime = 6.0f;
            NewGame.MAXLIFE = 500;
            NewGame.Life = 500;
            NewGame.POW = 70;
            NewGame.ItemSum = 8;
            SoundManager.instance.PlaySE(1);
            playerType = "Santa";
            SceneManager.LoadScene("Stage1");
            
        }

        else if (MerchantSelect)
        {
            NewGame.reLife();
            NewGame.refloor();
            NewGame.characterSpeed = 8.0f;
            NewGame.sield = 1;
            NewGame.FeverTime = 6.0f;
            NewGame.MAXLIFE = 150;
            NewGame.Life = 150;
            NewGame.POW = 40;
            NewGame.ItemSum = 4;
            SoundManager.instance.PlaySE(1);
            playerType = "Merchant";
            SceneManager.LoadScene("Stage1");
            
        }

        else if (WitchSelect)
        {
            NewGame.reLife();
            NewGame.refloor();
            NewGame.characterSpeed = 9.0f;
            NewGame.sield = 0;
            NewGame.FeverTime = 7.0f;
            NewGame.MAXLIFE = 250;
            NewGame.Life = 250;
            NewGame.POW = 30;
            NewGame.ItemSum = 3;
            SoundManager.instance.PlaySE(1);
            playerType = "Witch";
            SceneManager.LoadScene("Stage1");
            
        }

        else if (JackOSelect)
        {
            NewGame.reLife();
            NewGame.refloor();
            NewGame.characterSpeed = 10.0f;
            NewGame.sield = 3;
            NewGame.FeverTime = 7.0f;
            NewGame.MAXLIFE = 600;
            NewGame.Life = 600;
            NewGame.POW = 60;
            NewGame.ItemSum = 5;
            SoundManager.instance.PlaySE(1);
            playerType = "JackO";
            SceneManager.LoadScene("Stage1");
            
        }
        else if (BunnyGirlSelect)
        {
            NewGame.reLife();
            NewGame.refloor();
            NewGame.characterSpeed = 9.5f;
            NewGame.sield = 0;
            NewGame.FeverTime = 8.0f;
            NewGame.MAXLIFE = 800;
            NewGame.Life = 800;
            NewGame.POW = 80;
            NewGame.ItemSum = 5;
            SoundManager.instance.PlaySE(1);
            playerType = "BunnyGirl";
            SceneManager.LoadScene("Stage1");
            
        }
    }

    public void GetWarriorButton()
    {
        WarriorSelect = true;
    }
    public void GetHatmanButton()
    {
        HatmanSelect = true;
    }
    public void GetThiefButton()
    {
        ThiefSelect = true;
    }
    public void GetJonnySanButton()
    {
        JonnySanSelect = true;
    }
    public void GetShimazuSanButton()
    {
        ShimazuSanSelect = true;
    }
    public void GetCatButton()
    {
        CatSelect = true;
    }
    public void GetUpotuKunButton()
    {
        UpotuKunSelect = true;
    }
    public void GetSantaButton()
    {
        SantaSelect = true;
    }
    public void GetMerchantButton()
    {
        MerchantSelect = true;
    }
    public void GetWitchButton()
    {
        WitchSelect = true;
    }
    public void GetJackOButton()
    {
        JackOSelect = true;
    }
    public void GetBunnyGirlButton()
    {
        BunnyGirlSelect = true;
    }
    IEnumerator SavePanel()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(GameObject.Find("SavePanel"));
    }
}
