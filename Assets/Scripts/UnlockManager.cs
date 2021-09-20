using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockManager : MonoBehaviour
{

    public GameObject JonnySanPanel;
    public GameObject ShimazuSanPanel;
    public GameObject UpotuKunPanel;
    public GameObject JackOPanel;
    public GameObject SantaPanel;
    public GameObject BunnyGirlPanel;
    public GameObject CatPanel;

    public GameObject UnlockWindow;

    int targetPanel;

    public GameObject gold_object;

    Text gold_text;
    // Start is called before the first frame update
    void Start()
    {
        UnlockWindow.SetActive(false);
        gold_text = gold_object.GetComponent<Text>();
        gold_text.text = "" + PlayerPrefs.GetInt("GOLD-POINT", NewGame.GOLD);

    }

    // Update is called once per frame
    void Update()
    {
        if (NewGame.JonnySanPlayable == 1)
        {
            Destroy(JonnySanPanel.gameObject);
        }
        if (NewGame.ShimazuSanPlayable == 1)
        {
            Destroy(ShimazuSanPanel.gameObject);
        }
        if (NewGame.UpotuKunPlayable == 1)
        {
            Destroy(UpotuKunPanel.gameObject);
        }
        if (NewGame.JackOPlayable == 1)
        {
            Destroy(JackOPanel.gameObject);
        }
        if (NewGame.SantaPlayable == 1)
        {
            Destroy(SantaPanel.gameObject);
        }
        if (NewGame.BunnyGirlPlayable == 1)
        {
            Destroy(BunnyGirlPanel.gameObject);
        }
        if (NewGame.CatPlayable == 1)
        {
            Destroy(CatPanel.gameObject);
        }
    }
    public void NoButton()
    {
        SoundManager.instance.PlaySE(14);
        UnlockWindow.SetActive(false);

    }
    public void YesButton()
    {
        switch (targetPanel)
        {
            case 1:
                if(NewGame.GOLD >= 2000)
                {
                    SoundManager.instance.PlaySE(12);
                    NewGame.GOLD -= 2000;
                    PlayerPrefs.SetInt("GOLD-POINT", NewGame.GOLD);
                    PlayerPrefs.Save();

                    NewGame.JonnySanPlayable = 1;
                    PlayerPrefs.SetInt("JonnySan", NewGame.JonnySanPlayable);
                    PlayerPrefs.Save();

                    gold_text.text = "" + PlayerPrefs.GetInt("GOLD-POINT", NewGame.GOLD);
                    UnlockWindow.SetActive(false);
                }
                else
                {
                    NoButton();
                }
                break;
            case 2:
                if (NewGame.GOLD >= 2000)
                {
                    SoundManager.instance.PlaySE(12);
                    NewGame.GOLD -= 2000;
                    PlayerPrefs.SetInt("GOLD-POINT", NewGame.GOLD);
                    PlayerPrefs.Save();

                    NewGame.ShimazuSanPlayable = 1;
                    PlayerPrefs.SetInt("ShimazuSan", NewGame.ShimazuSanPlayable);
                    PlayerPrefs.Save();

                    gold_text.text = "" + PlayerPrefs.GetInt("GOLD-POINT", NewGame.GOLD);
                    UnlockWindow.SetActive(false);
                }
                else
                {
                    NoButton();
                }
                break;
            case 3:
                if (NewGame.GOLD >= 2000)
                {
                    SoundManager.instance.PlaySE(12);
                    NewGame.GOLD -= 2000;
                    PlayerPrefs.SetInt("GOLD-POINT", NewGame.GOLD);
                    PlayerPrefs.Save();

                    NewGame.UpotuKunPlayable = 1;
                    PlayerPrefs.SetInt("UpotuKun", NewGame.UpotuKunPlayable);
                    PlayerPrefs.Save();

                    gold_text.text = "" + PlayerPrefs.GetInt("GOLD-POINT", NewGame.GOLD);
                    UnlockWindow.SetActive(false);
                }
                else
                {
                    NoButton();
                }
                break;
            case 4:
                if (NewGame.GOLD >= 6000)
                {
                    SoundManager.instance.PlaySE(12);
                    NewGame.GOLD -= 6000;
                    PlayerPrefs.SetInt("GOLD-POINT", NewGame.GOLD);
                    PlayerPrefs.Save();

                    NewGame.JackOPlayable = 1;
                    PlayerPrefs.SetInt("JackO", NewGame.JackOPlayable);
                    PlayerPrefs.Save();

                    gold_text.text = "" + PlayerPrefs.GetInt("GOLD-POINT", NewGame.GOLD);
                    UnlockWindow.SetActive(false);
                }
                else
                {
                    NoButton();
                }
                break;
            case 5:
                if (NewGame.GOLD >= 6000)
                {
                    SoundManager.instance.PlaySE(12);
                    NewGame.GOLD -= 6000;
                    PlayerPrefs.SetInt("GOLD-POINT", NewGame.GOLD);
                    PlayerPrefs.Save();

                    NewGame.SantaPlayable = 1;
                    PlayerPrefs.SetInt("Santa", NewGame.SantaPlayable);
                    PlayerPrefs.Save();

                    gold_text.text = "" + PlayerPrefs.GetInt("GOLD-POINT", NewGame.GOLD);
                    UnlockWindow.SetActive(false);
                }
                else
                {
                    NoButton();
                }
                break;
            case 6:
                if (NewGame.GOLD >= 6000)
                {
                    SoundManager.instance.PlaySE(12);
                    NewGame.GOLD -= 6000;
                    PlayerPrefs.SetInt("GOLD-POINT", NewGame.GOLD);
                    PlayerPrefs.Save();

                    NewGame.BunnyGirlPlayable = 1;
                    PlayerPrefs.SetInt("BunnyGirl", NewGame.BunnyGirlPlayable);
                    PlayerPrefs.Save();

                    gold_text.text = "" + PlayerPrefs.GetInt("GOLD-POINT", NewGame.GOLD);
                    UnlockWindow.SetActive(false);
                }
                else
                {
                    NoButton();
                }
                break;
            case 7:
                if (NewGame.GOLD >= 10000)
                {
                    SoundManager.instance.PlaySE(12);
                    NewGame.GOLD -= 10000;
                    PlayerPrefs.SetInt("GOLD-POINT", NewGame.GOLD);
                    PlayerPrefs.Save();

                    NewGame.CatPlayable = 1;
                    PlayerPrefs.SetInt("Cat", NewGame.CatPlayable);
                    PlayerPrefs.Save();

                    gold_text.text = "" + PlayerPrefs.GetInt("GOLD-POINT", NewGame.GOLD);
                    UnlockWindow.SetActive(false);
                }
                else
                {
                    NoButton();
                }
                break;
            default:
                break;
        }
            
    }

    public void OnClick_1()
    {
        UnlockWindow.SetActive(true);
        targetPanel = 1;
    }
    public void OnClick_2()
    {
        UnlockWindow.SetActive(true);
        targetPanel = 2;
    }
    public void OnClick_3()
    {
        UnlockWindow.SetActive(true);
        targetPanel = 3;
    }
    public void OnClick_4()
    {
        UnlockWindow.SetActive(true);
        targetPanel = 4;
    }
    public void OnClick_5()
    {
        UnlockWindow.SetActive(true);
        targetPanel = 5;
    }
    public void OnClick_6()
    {
        UnlockWindow.SetActive(true);
        targetPanel = 6;
    }
    public void OnClick_7()
    {
        UnlockWindow.SetActive(true);
        targetPanel = 7;
    }
}
