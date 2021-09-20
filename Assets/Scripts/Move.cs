using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public FloatingJoystick joystick;
	public DynamicJoystick joystickD;



	bool isUButtonDown = false;
	bool isDButtonDown = false;
    bool isLButtonDown = false;
	bool isRButtonDown = false;

	bool isATKButtonDown = false;

	float delta = 0;
	[SerializeField] bool isMove = false;

	[SerializeField] public static bool isLeave = false;

	Vector3 nowPos = Vector3.zero;
	Vector3 targetPos = Vector3.zero;

	//public GameObject Block;
	//public GameObject Player;

	Animator animator;

	LayerMask mask = 1 << 8;

    int horizontal;
	int vertical;


	public static float UpSpeed = 0.0f;

	public static int direction = 0;
	public GameObject weaponUp;
	public GameObject weaponDown;
	public GameObject weaponRight;
	public GameObject weaponLeft;
	public GameObject weapon;

	static GameObject Obj;

	static float defultAttackTime = 0.4f;
	float attackTime = defultAttackTime;
	public static bool isAttack = false;

	void Start()
	{
		animator = GetComponent<Animator>();
		/*
        if(SelectManager.playerType == "Hatman")
        {
			characterSpeed = 10.0f;
        }
        else if (SelectManager.playerType == "Thief")
        {
			characterSpeed = 11.0f;
		}
        else if (SelectManager.playerType == "Warrior")
        {
			characterSpeed = 9.0f;
		}
		else if (SelectManager.playerType == "JonnySan")
		{
			characterSpeed = 12.0f;
		}
		else if (SelectManager.playerType == "ShimazuSan")
		{
			characterSpeed = 10.0f;
		}
		else if (SelectManager.playerType == "Cat")
		{
			characterSpeed = 15.0f;
		}
		*/
		manager = GameObject.Find("EnemySpawn").GetComponent<TileManager>();
	}

	TileManager manager;
	public int debugValue;
	public Vector3 currentPos;
	public Vector2 vector2;

	void Update()
	{
		currentPos = new Vector3(transform.position.x, transform.position.y);
		debugValue = manager.Index((int)currentPos.x, (int)currentPos.y);
		vector2 = new Vector2(manager.tiles[debugValue].x, manager.tiles[debugValue].y);

		bool rightJoy = false;
		bool leftJoy = false;
		bool upJoy = false;
		bool downJoy = false;

        //ジョイスティックの感度
		float tilt = 0.1f;
        /*
		float joystickX = joystick.Horizontal;
		float joystickY = joystick.Vertical;
        */
		//float joystickX = joystickD.Horizontal;
		//float joystickY = joystickD.Vertical;

		/*
		if (joystickX >= tilt)
        {
			rightJoy = true;
        }

		if (joystickX <= - tilt)
		{
			leftJoy = true;
		}

		if (joystickY >= tilt)
		{
			upJoy = true;
		}

		if (joystickY <= - tilt)
		{
			downJoy = true;
		}
		*/

		//RaycastHit2D result = Physics2D.Linecast (nowPos, targetPos);

		//ヒットしたオブジェクトを出力
		//Debug.Log (result.collider);

		//自爆ボタン
		if(Input.GetKeyDown(KeyCode.R))
        {
			NewGame.Life = 0;
			Enemy.LifeLost();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
			PlayerPrefs.SetInt("JonnySan", 0);
			PlayerPrefs.SetInt("ShimazuSan", 0);
			PlayerPrefs.SetInt("UpotuKun", 0);
			PlayerPrefs.SetInt("JackO", 0);
			PlayerPrefs.SetInt("Santa", 0);
			PlayerPrefs.SetInt("BunnyGirl", 0);
			PlayerPrefs.SetInt("Cat", 0);
			PlayerPrefs.Save();
		}
		//チートボタン
		if (Input.GetKeyDown(KeyCode.N))
		{
			NewGame.Floor = NewGame.MAXFLOOR - 1;
		}
		//チートボタン2
		if (Input.GetKeyDown(KeyCode.G))
		{
			NewGame.SCORE += 1000;
		}

		// 右のキーを押した時に１になる、左は−１
		horizontal = (int)(Input.GetAxisRaw("Horizontal"));

		// 上のキーを押した時に１になる、下は−１
		vertical = (int)(Input.GetAxisRaw("Vertical"));

		// 動いていない時
		if (!isMove)
		{

			// 右
			if (horizontal == 1 || this.isRButtonDown || rightJoy)
			{
				nowPos = this.transform.position;
				targetPos = this.transform.position + Vector3.right;

				RaycastHit2D result = Physics2D.Linecast(transform.position, targetPos, mask);
				animator.SetInteger("direction", 2);

				direction = 2;

				if (result.transform == null)
				{
					isMove = true;
					//isLeave = true;
				}
				/*
				else
				{
					isMove = true;
					direction = 3;
					targetPos = this.transform.position + Vector3.up;
					//RaycastHit2D result2 = Physics2D.Linecast(transform.position, targetPos, mask);
                    if (result.transform == null)
                    {
						isMove = false;
                    }
				}
				*/
				//TurnDirection(2);
			}
			// 左
			else if (horizontal == -1 || this.isLButtonDown || leftJoy)
			{
				nowPos = this.transform.position;
				targetPos = this.transform.position - Vector3.right;

				RaycastHit2D result = Physics2D.Linecast(transform.position, targetPos, mask);
				animator.SetInteger("direction", 1);

				direction = 1;

				if (result.transform == null)
				{
					isMove = true;
					//isLeave = true;
				}
				/*
				else
				{
					isMove = true;
					direction = 0;
					targetPos = this.transform.position - Vector3.up;
					//RaycastHit2D result2 = Physics2D.Linecast(transform.position, targetPos, mask);
					if (result.transform == null)
					{
						isMove = false;
					}
				}
				*/
				//TurnDirection(1);
			}
			// 上
			else if (vertical == 1 || this.isUButtonDown || upJoy)
			{
				nowPos = this.transform.position;
				targetPos = this.transform.position + Vector3.up;

				RaycastHit2D result = Physics2D.Linecast(transform.position, targetPos, mask);
				animator.SetInteger("direction", 3);

				direction = 3;

				if (result.transform == null)
				{
					isMove = true;
					//isLeave = true;
				}
				/*
				else
				{
					isMove = true;
					direction = 1;
					targetPos = this.transform.position - Vector3.right;
					//RaycastHit2D result2 = Physics2D.Linecast(transform.position, targetPos, mask);
					if (result.transform == null)
					{
						isMove = false;
					}
				}
				*/
				//TurnDirection(3);
			}
			// 下
			else if (vertical == -1 || this.isDButtonDown || downJoy)
			{
				nowPos = this.transform.position;
				targetPos = this.transform.position - Vector3.up;

				RaycastHit2D result = Physics2D.Linecast(transform.position, targetPos, mask);
				animator.SetInteger("direction", 0);

				direction = 0;

				if (result.transform == null)
				{
					isMove = true;
					//isLeave = true;
				}
				/*
                else
                {
					isMove = true;
					direction = 2;
					targetPos = this.transform.position + Vector3.right;
					//RaycastHit2D result2 = Physics2D.Linecast(transform.position, targetPos, mask);
					if (result.transform == null)
					{
						isMove = false;
					}
				}
				*/
				//TurnDirection(0);
			}
		}
		else
		{
			TurnDirection(direction);

			delta += Time.deltaTime * (NewGame.characterSpeed + UpSpeed);

			this.transform.position = Vector3.Lerp(nowPos, targetPos, delta);

			if (delta >= 1.0f)
			{
				isMove = false;
				delta = 0;
			}
            else
            {
				isLeave = true;
            }
		}

		//武器をふる
        if (Input.GetKeyDown(KeyCode.Space) || this.isATKButtonDown)
        {
			this.isATKButtonDown = false;
			if (NewGame.POW > 0)
            {
				NewGame.POW -= 1;
				SoundManager.instance.PlaySE(5);
				isAttack = true;
				//Vector3 weaponPosition = this.transform;
				Vector3 weaponPosition = transform.position;
				switch (direction)
				{
					case 0:
						//Instantiate(weaponDown, this.transform.position + Vector3.down, Quaternion.Euler(0, 0, 0));
						//Debug.Log(direction);
						weapon = weaponDown;
						//weaponPosition = Vector3.down;
						weaponPosition.y -= 1;
						break;
					case 1:
						//Instantiate(weaponLeft, this.transform.position + Vector3.left, Quaternion.Euler(0, 0, 0));
						//Debug.Log(direction);
						weapon = weaponLeft;
						//weaponPosition = Vector3.left;
						weaponPosition.x -= 1;

						break;
					case 2:
						//Instantiate(weaponRight, this.transform.position + Vector3.right, Quaternion.Euler(0, 0, 0));
						//Debug.Log(direction);
						weapon = weaponRight;
						//weaponPosition = Vector3.right;
						weaponPosition.x += 1;
						break;
					case 3:
						//Instantiate(weaponUp, this.transform.position + Vector3.up, Quaternion.Euler(0, 0, 0));
						//Debug.Log(direction);
						weapon = weaponUp;
						//weaponPosition = Vector3.up;
						weaponPosition.y += 1;
						break;
					default:
						break;
				}
				//Instantiate(weapon, this.transform.position + Vector3.down, Quaternion.Euler(0, 0, 0));
				Obj = (GameObject)Instantiate(weapon, weaponPosition, Quaternion.identity);
				Obj.transform.parent = this.transform;
			}
            else
            {
				SoundManager.instance.PlaySE(14);
			}
		}

		if (attackTime < 0)
		{
			attackTime = defultAttackTime;
			isAttack = false;
		}
        if (isAttack)
        {
			attackTime -= Time.deltaTime;
        }

	}

	void TurnDirection(int d)
    {
		/*
		var directionList = new List<string>()
		{
			"down","left","right","up"
		};
		var numList = new List<int>()
		{
			0,1,2,3
		};
		*/

		RaycastHit2D result = Physics2D.Linecast(transform.position, targetPos, mask);
		//Debug.Log(result);

		if (result.transform != null)
        {
			if(d == 0)
            {
				direction = 2;
				targetPos = this.transform.position + Vector3.right;
			}
			else if(d == 1)
            {
				direction = 0;
				targetPos = this.transform.position - Vector3.up;
			}
			else if(d == 2)
            {
				direction = 3;
				targetPos = this.transform.position + Vector3.up;
			}
			else if(d == 3)
            {
				direction = 1;
				targetPos = this.transform.position - Vector3.right;
			}
			Debug.Log("d = " + d);
		}
    }

	public void GetMyUpButtonDown()
	{
		this.isUButtonDown = true;
		Debug.Log("GetMyUpButtonDown");
	}
	public void GetMyUpButtonUp()
	{
		this.isUButtonDown = false;
		Debug.Log("GetMyUpButtonUp");
	}
	public void GetMyDownButtonDown()
	{
		this.isDButtonDown = true;
		Debug.Log("GetMyDownButtonDown");
	}
	public void GetMyDownButtonUp()
	{
		this.isDButtonDown = false;
		Debug.Log("GetMyDownButtonUp");
	}

	public void GetMyLeftButtonDown()
	{
		this.isLButtonDown = true;
		Debug.Log("GetMyLeftButtonDown");
	}
	public void GetMyLeftButtonUp()
	{
		this.isLButtonDown = false;
		Debug.Log("GetMyLeftButtonUp");
	}

	public void GetMyRightButtonDown()
	{
		this.isRButtonDown = true;
		Debug.Log("GetMyRightButtonDown");
	}
	public void GetMyRightButtonUp()
	{
		this.isRButtonDown = false;
		Debug.Log("GetMyRightButtonUp");
	}

	public void GetATKButtonDown()
    {
		this.isATKButtonDown = true;
    }
	public void GetATKButtonUp()
	{
		this.isATKButtonDown = false;
	}


}