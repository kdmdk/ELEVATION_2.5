using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using DG.Tweening;
using GoogleMobileAds.Api;

public class NextFloor : MonoBehaviour
{
	BannerView bannerView;

	private GameObject FloorText;
	private int Floor;

	private GameObject LifeText;

	private GameObject EXPText;

	private GameObject LVText;
	private GameObject COMBOText;

	private GameObject SCOREText;
	private GameObject DText;
	private GameObject KILLText;

	private GameObject KEYText;

	//UIエフェクト用
	//RectTransform rectTran;

	//public static bool FloorTransition = false;
	public static float countTime = 0;
	public static bool isFever = false;
	public static bool isGetKey = false;

	public static int killEnemy = 0;

	GameObject FeverText;

	void Start()
	{
		killEnemy = 0;
		//FloorTransition = false;
		NewGame.addfloor();
		int resultFloor = NewGame.getFloor();
		Floor = resultFloor;
		this.FloorText = GameObject.Find("FloorText");
		this.FloorText.GetComponent<Text>().text = this.Floor + "F";

		isGetKey = false;
		Move.isLeave = false;
		//UIエフェクト用
		//COMBOText = GameObject.Find("COMBOText");
		//rectTran = COMBOText.GetComponent<RectTransform>();

		/*
		if (Floor != 1)
		{
			Debug.Log(Floor);
			//AudioSource.PlayClipAtPoint(m_clearSe, transform.position);
		}
        */
		this.FeverText = GameObject.Find("FeverText");
		this.FeverText.GetComponent<Text>().text = "";
	}

	private void Update()
    {
		NewGame.comboRemainingTime -= Time.deltaTime;

        int life = NewGame.Life;
        if (NewGame.Life <= 0)
        {
			NewGame.Life = 0;
        }
		this.LifeText = GameObject.Find("LifeText");
		this.LifeText.GetComponent<Text>().text = life.ToString();
		/*
		int exp = NewGame.EXP;
		this.EXPText = GameObject.Find("EXPText");
        this.EXPText.GetComponent<Text>().text = exp.ToString();

		int lv = NewGame.LV;
		this.LVText = GameObject.Find("LVText");
		this.LVText.GetComponent<Text>().text = lv.ToString();
		*/
		int combo = NewGame.COMBO;
		this.COMBOText = GameObject.Find("COMBOText");
		this.COMBOText.GetComponent<Text>().text = combo.ToString();

		int score = NewGame.SCORE;
		this.SCOREText = GameObject.Find("SCOREText");
		this.SCOREText.GetComponent<Text>().text = score.ToString();

		int d = NewGame.POW;
		this.DText = GameObject.Find("DText");
		this.DText.GetComponent<Text>().text = d.ToString();

		int kill = NewGame.KILL;
		this.KILLText = GameObject.Find("KILLText");
		this.KILLText.GetComponent<Text>().text = kill.ToString();

		this.KEYText = GameObject.Find("KEYText");
		if (isGetKey)
        {
			this.KEYText.GetComponent<Text>().text = "GET!";
		}
		else
        {
			this.KEYText.GetComponent<Text>().text = "";
		}

		if (isFever)
		{
			float time = NewGame.FeverTime - countTime;
			this.FeverText.GetComponent<Text>().text = "FEVER";
			countTime += Time.deltaTime;
			//Debug.Log(countTime);
		}
		
		if (countTime >= NewGame.FeverTime)
		{

			this.FeverText.GetComponent<Text>().text = "";
			isFever = false;
			countTime = 0;
			ItemManager.DestroyEffect();
			Move.UpSpeed = 0.0f;

			//GameObject BarrierObj = GameObject.FindWithTag("Barrier");
			//BarrierObj.SetActive(false);
		}

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
        {
			if(Floor == NewGame.MAXFLOOR)
            {
				//プレイヤーキャラ毎にボーナスポイント
				if(SelectManager.playerType == "Hatman")
                {
					NewGame.SCORE += 5000;
                }
				else if (SelectManager.playerType == "Warrior")
				{
					NewGame.SCORE += 5000;
				}
				else if (SelectManager.playerType == "Thief")
				{
					NewGame.SCORE += 6000;
				}
				else if (SelectManager.playerType == "JonnySan")
				{
					NewGame.SCORE += 2000;
				}
				else if (SelectManager.playerType == "ShimazuSan")
				{
					NewGame.SCORE += 2000;
				}
				else if (SelectManager.playerType == "Cat")
				{
					NewGame.SCORE += 1000;
				}
				else if (SelectManager.playerType == "UpotuKun")
				{
					NewGame.SCORE += 2000;
				}
				else if (SelectManager.playerType == "Santa")
				{
					NewGame.SCORE += 1000;
				}
				else if (SelectManager.playerType == "Merchant")
				{
					NewGame.SCORE += 4000;
				}
				else if (SelectManager.playerType == "Witch")
				{
					NewGame.SCORE += 4000;
				}
				else if (SelectManager.playerType == "JackO")
				{
					NewGame.SCORE += 1000;
				}
				else if (SelectManager.playerType == "BunnyGirl")
				{
					NewGame.SCORE += 1000;
				}

				if (NewGame.SCORE > NewGame.HighScore)
				{
					PlayerPrefs.SetInt("HIGH-SCORE", NewGame.SCORE);
					PlayerPrefs.Save();
				}
				SceneManager.LoadScene("GameClear");
				Debug.Log("CLEAR!");
				SoundManager.instance.PlaySE(1);
				//NewGame.addfloor();
				NewGame.isClear = true;

				//バナーを削除
				//GameObject banner = GameObject.Find("BANNER(Clone)");
				//Destroy(banner);
				bannerDestroy();
			}
            else
            {
				countTime = 0;
				isFever = false;
				Move.UpSpeed = 0.0f;
				//GameObject BarrierObj = GameObject.FindWithTag("Barrier");
				//BarrierObj.SetActive(false);

				//LoadNextScene();
				//FloorTransition = true;
				this.FloorText.GetComponent<Text>().text = this.Floor + "F";
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				SoundManager.instance.PlaySE(1);

			}

			Move.isLeave = false;
			Debug.Log("Move.isLeave" + Move.isLeave);

		}
	}
    /*
    public void EffectUI()
    {
		//UIエフェクト
		rectTran.DOScale(Vector3.down, 2.0f);
		rectTran.DOPlayBackwards();
		Debug.Log(rectTran);
	}
    */
	public void bannerDestroy()
    {
		//バナーを削除
		//GameObject banner = GameObject.Find("BANNER(Clone)");
		//Destroy(banner);
		//bannerView.Hide();
		//bannerView.Destroy();
		GoogleAds.bannerHideAndDestroy();
	}
}
