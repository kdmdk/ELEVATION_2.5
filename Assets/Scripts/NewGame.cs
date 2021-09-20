using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
	public static int Floor = 0;

	public static int Life = 200;
	public static int MAXLIFE = 200;

	public static int EXP = 0;

	public static int LV = 1;

	public static int COMBO = 0;

	public static float comboDefaultRemainingTime = 5.0f;

	public static float comboRemainingTime = comboDefaultRemainingTime;

	public static int SCORE = 0;

	public static int POW = 20;

	public static int KILL = 0;

	public static int ItemSum = 3;

	public static float FeverTime = 8.0f;

	public static int sield = 0;

	public static float characterSpeed = 8.0f;

	public static int HighScore;

	public static int GOLD;

	public static bool isClear = false;

	public static int MAXFLOOR = 15;


	public static int JonnySanPlayable;
	public static int ShimazuSanPlayable;
	public static int UpotuKunPlayable;
	public static int JackOPlayable;
	public static int SantaPlayable;
	public static int BunnyGirlPlayable;
	public static int CatPlayable;


	public static int getFloor()
	{
		return Floor;
	}

	public static void addfloor()
	{
		Floor++;
	}
	public static void subfloor()
	{
		Floor--;
	}
	public static void refloor()
	{
		Floor = 0;
    }

    public static void reLife()
    {
		Life = MAXLIFE;
		EXP = 0;
		LV = 1;
		COMBO = 0;

		SCORE = 0;
		POW = 10;
		KILL = 0;

		isClear = false;
    }
	
	void Start()
	{
		SoundManager.instance.PlayBGM(0);
		HighScore = PlayerPrefs.GetInt("HIGH-SCORE", HighScore);
		GOLD = PlayerPrefs.GetInt("GOLD-POINT", GOLD);

		JonnySanPlayable = PlayerPrefs.GetInt("JonnySan", JonnySanPlayable);
		ShimazuSanPlayable = PlayerPrefs.GetInt("ShimazuSan", ShimazuSanPlayable);
		UpotuKunPlayable = PlayerPrefs.GetInt("UpotuKun", UpotuKunPlayable);
		JackOPlayable = PlayerPrefs.GetInt("JackO", JackOPlayable);
		SantaPlayable = PlayerPrefs.GetInt("Santa", SantaPlayable);
		BunnyGirlPlayable = PlayerPrefs.GetInt("BunnyGirl", BunnyGirlPlayable);
		CatPlayable = PlayerPrefs.GetInt("Cat", CatPlayable);

		
	}
	void Update()
	{
		if (Input.anyKeyDown)
        {
			SceneManager.LoadScene("SelectCharacter");
			Debug.Log(Floor);
		}
			

	}
}
