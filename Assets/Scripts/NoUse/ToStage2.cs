using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToStage2 : MonoBehaviour
{

	private GameObject FloorText;
	private int Floor;
	public AudioClip m_clearSe;

	void Start()
	{
		NewGame.addfloor();
		int resultFloor = NewGame.getFloor();
		Floor = resultFloor;
		this.FloorText = GameObject.Find("FloorText");
		this.FloorText.GetComponent<Text>().text = this.Floor + "F";

		if (Floor != 1)
		{
			Debug.Log(Floor);
			//AudioSource.PlayClipAtPoint(m_clearSe, transform.position);
		}

	}


	void OnTriggerEnter2D(Collider2D other)
	{
		//LoadNextScene();

		this.FloorText.GetComponent<Text>().text = this.Floor + "F";
		SceneManager.LoadScene("Stage2");
		SoundManager.instance.PlaySE(1);
	}
}
