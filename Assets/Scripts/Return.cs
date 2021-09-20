using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Return : MonoBehaviour
{
	private GameObject FloorText;
	private int Floor;

	private GameObject SCOREText;

	public Sprite HatmanImg;
	public Sprite WarriorImg;
	public Sprite ThiefImg;
	public Sprite JonnySanImg;
	public Sprite ShimazuSanImg;
	public Sprite CatImg;
	public Sprite UpotuKunImg;
	public Sprite SantaImg;

	public Sprite MerchantImg;
	public Sprite WitchImg;
	public Sprite JackOImg;
	public Sprite BunnyGirlImg;

	public Image image;
	// Use this for initialization
	void Start()
	{
		AdButton.AdOK = false;

		if (NewGame.isClear)
        {
			SoundManager.instance.PlaySE(13);

			int resultFloor = NewGame.getFloor();
			Floor = resultFloor;
			this.FloorText = GameObject.Find("FloorText");
			this.FloorText.GetComponent<Text>().text = this.Floor + "F";


			this.SCOREText = GameObject.Find("SCOREText");
			this.SCOREText.GetComponent<Text>().text = "" + NewGame.SCORE;

			Debug.Log("GAMECLEAR!");
		}
        else
        {
			SoundManager.instance.PlaySE(2);

			int resultFloor = NewGame.getFloor();
			Floor = resultFloor;
			this.FloorText = GameObject.Find("FloorText");
			this.FloorText.GetComponent<Text>().text = this.Floor + "F";


			this.SCOREText = GameObject.Find("SCOREText");
			this.SCOREText.GetComponent<Text>().text = "" + NewGame.SCORE;

			Debug.Log("GAMEOVER!");
		}
		Debug.Log("isClear:" + NewGame.isClear);

		if(SelectManager.playerType == "Hatman")
        {
			image.sprite = HatmanImg;
		}
		else if (SelectManager.playerType == "Warrior")
		{
			image.sprite = WarriorImg;
		}
		else if (SelectManager.playerType == "Thief")
		{
			image.sprite = ThiefImg;
		}
		else if (SelectManager.playerType == "JonnySan")
		{
			image.sprite = JonnySanImg;
		}
		else if (SelectManager.playerType == "ShimazuSan")
		{
			image.sprite = ShimazuSanImg;
		}
		else if (SelectManager.playerType == "Cat")
		{
			image.sprite = CatImg;
		}
		else if (SelectManager.playerType == "UpotuKun")
		{
			image.sprite = UpotuKunImg;
		}
		else if (SelectManager.playerType == "Santa")
		{
			image.sprite = SantaImg;
		}
		else if (SelectManager.playerType == "Merchant")
		{
			image.sprite = MerchantImg;
		}
		else if (SelectManager.playerType == "Witch")
		{
			image.sprite = WitchImg;
		}
		else if (SelectManager.playerType == "JackO")
		{
			image.sprite = JackOImg;
		}
		else if (SelectManager.playerType == "BunnyGirl")
		{
			image.sprite = BunnyGirlImg;
		}
	}
	/*
	// Update is called once per frame
	void Update()
	{
		Invoke("dead", 0.5f);

	}

	private void dead()
	{
		if (Input.anyKeyDown)
        {
			SceneManager.LoadScene("NewGame");
			NewGame.refloor();
			NewGame.reLife();
		}
	}
	*/
}
