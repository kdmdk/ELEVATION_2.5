using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DownStair : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
        if (Move.isLeave && other.gameObject.tag == "Player")
		{
			NewGame.subfloor();
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			SoundManager.instance.PlaySE(1);
			Move.isLeave = false;
			NextFloor.isFever = false;
			Move.UpSpeed = 0.0f;
		}
	}
}
