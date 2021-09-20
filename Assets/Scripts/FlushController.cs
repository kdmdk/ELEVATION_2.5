using UnityEngine;
using UnityEngine.UI;

public class FlushController : MonoBehaviour
{
	SpriteRenderer sprite;
	Image img;
	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		img = GameObject.Find("FlashImg").GetComponent<Image>();
		img.color = Color.clear;
		sprite.color = new Color(1.0f, 1.0f, 1.0f);
	}

	void Update()
	{
		if (Enemy.isBlink)
		{
			this.sprite.color = new Color(1.0f, 0f, 0f, 0.5f);
			img.color = new Color(0.5f, 0f, 0f, 0.5f);
			Enemy.isBlink = false;
		}
		else if (ItemManager.isPoison)
		{
			this.sprite.color = new Color(0f, 0f, 1.0f, 1.0f);
			img.color = new Color(0f, 0f, 0.5f, 1.0f);
			ItemManager.isPoison = false;
		}
		else if (ItemManager.isFirstAidKit)
		{
			this.sprite.color = new Color(0.0f, 0.8f, 0.8f, 1.0f);
			img.color = new Color(0f, 0.8f, 0.8f, 1.0f);
			ItemManager.isFirstAidKit= false;
		}
		else
		{
			img.color = Color.Lerp(this.img.color, Color.clear, Time.deltaTime * 10);
			this.sprite.color = Color.Lerp(this.sprite.color, new Color(1.0f, 1.0f, 1.0f), Time.deltaTime * 10);
		}
	}
}