using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{ 
	//EnemyModel enemyModel;

	public static float defaultCountdown = 0.2f;
	float countdown = defaultCountdown;

	public bool isMuteki = true;
	bool inWall = false;

	Animator animator;

	private float remainingTimeToMove = 1.0f;

	private float acceleration = 0.001f;

	private Vector3 currentDirection = Vector3.down;
	LayerMask mask = 1 << 8;

	//bool judgment;

	public int thisEnemyPower;
	public float thisEnemySpeed;
	public int thisEnemyExp;

	public string Type;
	public static string thisEnemyType;

	//private GameObject EXPText;
	//private GameObject LVText;
	private GameObject SCOREText;
	private GameObject DText;

	public GameObject hitEffect;

	public GameObject Key;
	//static BoxCollider2D collider;

	public static bool isBlink = false;

	public Vector3 direction;

	public int num = 0;


	public Transform target;

	private List<TileManager.Tile> tiles = new List<TileManager.Tile>();
	private TileManager manager;

	public TileManager.Tile nextTile = null;
	public TileManager.Tile targetTile;
	TileManager.Tile currentTile;

	bool isFind = false;

	public static int sield = 0;

	private void Awake()
    {
		thisEnemyType = Type;

		manager = GameObject.Find("EnemySpawn").GetComponent<TileManager>();
		tiles = manager.tiles;
	}

    void Start()
	{
		direction = currentDirection;
		animator = GetComponent<Animator>();
		int resultFloor = NewGame.getFloor();

        //階数上がる毎に加速する
		acceleration *= resultFloor;

        //ゲームオーバー判定
		//judgment = true;

		//UIText = this.GetComponent<Text>();
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{
		/*
		if (NewGame.Life <= 0)
    	{
			if (judgment)
			{				
				judgment = false;
				SceneManager.LoadScene("GameOver");
			}
		}
        */
		countdown -= Time.deltaTime;
        if(countdown <= 0)
        {
			isMuteki = false;
        }

		remainingTimeToMove -= Time.deltaTime;

		if (remainingTimeToMove < 0)
		{

			//remainingTimeToMove = 0.5f;
            remainingTimeToMove = (12 - thisEnemySpeed) / 10;

            //remainingTimeToMove -= acceleration;

            if (isFind)
            {
				AILogic();
            }
            else
            {
				selectDirection();
			}

			currentDirection = direction;
			this.transform.position += direction;
		}
	}

	// 移動の方向を決める
	private Vector3 selectDirection()
	{

		//Vector3 direction = Vector3.zero;
		int count = 0;
		bool[] check = { false, false, false, false };

		while (true)
		{

			int num = Random.Range(0, 4);

			//int num = 0;

			/*一旦コメントアウト*/
			//AILogic();
			/*
			if(direction == Vector3.up)
            {
				num = 0;
            }
			else if (direction == Vector3.down)
			{
				num = 1;
			}
			else if (direction == Vector3.right)
			{
				num = 2;
			}
			else if (direction == Vector3.left)
			{
				num = 3;
			}
			*/
			//num = Random.Range(0, 4);


			if (num == 0 && !check[num])
			{
				direction = Vector3.up;
				check[num] = true;
				animator.SetInteger("direction", 3);
				count++;
			}
			else if (num == 1 && !check[num])
			{
				direction = Vector3.down;
				check[num] = true;
				animator.SetInteger("direction", 0);
				count++;
			}
			else if (num == 2 && !check[num])
			{
				direction = Vector3.right;
				check[num] = true;
				animator.SetInteger("direction", 2);
				count++;
			}
			else if (num == 3 && !check[num])
			{
				direction = Vector3.left;
				check[num] = true;
				animator.SetInteger("direction", 1);
				count++;
			}

			// 進行方向にブロックがないか判定
			RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position + direction, mask);

			// ブロックが無く、進行方向と反対ではない場合
			if (!hit.collider && direction != currentDirection * -1)
			{
				break;
			}

			// 行き止まりの場合に戻る
			if (count == check.Length)
			{
				direction = currentDirection * -1;
				break;
			}
		}

		return direction;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "SearchArea")
		{
			isFind = true;
        }

		if (other.gameObject.tag == "wall")
        {
			//collider = GetComponent<BoxCollider2D>();
			//collider.size = new Vector2(0, 0);
			inWall = true;
			Destroy(this.gameObject);
			//Debug.Log(inWall);
		}

		if(inWall == false && isMuteki == false && other.gameObject.tag == "Player")
        {
            if (!NextFloor.isFever)
            {
				isBlink = true;
			}
			//StartCoroutine("Blink",isBlink);
			//効果音
			SoundManager.instance.PlaySE(4);
			//プレイヤーキャラクター別のダメージ処理
			//sieldはキャラ固有の防御力
			/*
			switch (SelectManager.playerType)
			{
				case "Hatman":
					sield = 1;
					break;
				case "Thief":
					sield = 0;
					break;
				case "Warrior":
					sield = 2;
					break;
				case "JonnySan":
					sield = 2;
					break;
				case "ShimazuSan":
					sield = 4;
					break;
				case "Cat":
					sield = 0;
					break;
				default:
					break;
			}
			*/
			//ダメージ処理、無敵でなければライフが減る
			if (!NextFloor.isFever)
			{
				NewGame.Life -= thisEnemyPower - NewGame.sield;
				isMuteki = true;
				countdown = 1;
			}
			LifeLost();
		}
		//敵との接触時の処理
		//敵出現時の無敵状態の判定

		if (inWall == false && other.gameObject.tag == "Wepon" || inWall == false && NextFloor.isFever == true && other.gameObject.tag == "Player")
        {
			//Debug.Log("Attack!");
			NewGame.KILL++;
			NextFloor.killEnemy++;
            //コンボの処理
            if(NewGame.comboRemainingTime <= 0)
            {
				NewGame.COMBO = 1;
            }
            else
            {
				NewGame.COMBO += 1;
			}
			NewGame.comboRemainingTime = NewGame.comboDefaultRemainingTime;

			/*
			//プレイヤーキャラクター別のダメージ処理
			//sieldはキャラ固有の防御力
			int sield = 0;
			switch (SelectManager.playerType)
            {
				case "Hatman":
					sield = 1;
					break;
				case "Thief":
					sield = 0;
					break;
				case "Warrior":
					sield = 2;
					break;
				default:
					break;
            }

			//ダメージ処理、無敵でなければライフが減る
            if (!NextFloor.isFever)
            {
				NewGame.Life -= thisEnemyPower - sield;
			}
			
			//経験値取得　コンボ数が経験値にプラスされる処理 防御力が高いと獲得経験値が下がる仕様
			NewGame.EXP += thisEnemyExp + NewGame.COMBO - sield;
			*/
			NewGame.SCORE += thisEnemyExp + NewGame.COMBO;


			//this.EXPText = GameObject.Find("EXPText");
			//this.EXPText.GetComponent<Text>().text = "EXP:" + NewGame.EXP;

			this.SCOREText = GameObject.Find("SCOREText");
			this.SCOREText.GetComponent<Text>().text = "" + NewGame.SCORE;


			//効果音
			SoundManager.instance.PlaySE(0);

            //無敵時間
            countdown = defaultCountdown;

            //敵オブジェクトの破壊
			Destroy(this.gameObject);
			Debug.Log(other);
			if(NextFloor.killEnemy == 10)
            {
				Instantiate(Key, this.gameObject.transform.position, transform.rotation);
			}

			//レベルアップの処理
			if (NewGame.EXP >= 100)
			{
				NewGame.EXP -= 100;

				/*
				//レベルアップ時に回復
				if(NewGame.Life <= NewGame.MAXLIFE)
                {
					NewGame.Life += 20;
				}
				*/

				NewGame.LV++;
				//Lv = NewGame.LV;
				//this.LVText = GameObject.Find("LVText");
				//this.LVText.GetComponent<Text>().text = "LV:" + NewGame.LV;
				this.DText = GameObject.Find("DText");
				this.DText.GetComponent<Text>().text = "" + NewGame.POW;
			}

			//UIAnimation.UIEffect();
			UIAnimation.isDmg = true;

			//
			Instantiate(hitEffect,this.transform.position,Quaternion.identity);
		}
	}
	static public void LifeLost()
    {
		if (NewGame.Life <= 0)
		{
			//バナーを削除
			//GameObject banner = GameObject.Find("BANNER(Clone)");
			//Destroy(banner);
			NextFloor nextFloor = new NextFloor();
			nextFloor.bannerDestroy();
			
			if (NewGame.SCORE > NewGame.HighScore)
			{
				PlayerPrefs.SetInt("HIGH-SCORE", NewGame.SCORE);
				PlayerPrefs.Save();
			}
			Move.UpSpeed = 0;
			SceneManager.LoadScene("GameOver");
			Debug.Log("dead!");
		}
	}
	/*
	IEnumerator Blink(bool isB)
	{
		while (isB)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			var renderComponent = player.GetComponent<Renderer>();
			renderComponent.enabled = !renderComponent.enabled;
			yield return new WaitForSeconds(interval);
        }
	}
	*/
	public void AILogic()
	{
		
		//Debug.Log(direction);
		//Debug.Log(transform.position.x);
		// get current tile
		Vector3 currentPos = new Vector3(transform.position.x, transform.position.y);
		//Debug.Log("currentPos =" + currentPos);
		currentTile = tiles[manager.Index((int)currentPos.x, (int)currentPos.y)];

		targetTile = GetTargetTilePerGhost();

		// get the next tile according to direction
		if (direction == Vector3.right ) nextTile = tiles[manager.Index((int)(currentPos.x + 1), (int)currentPos.y)];
		if (direction == Vector3.left) nextTile = tiles[manager.Index((int)(currentPos.x - 1), (int)currentPos.y)];
		if (direction == Vector3.up) nextTile = tiles[manager.Index((int)currentPos.x, (int)(currentPos.y + 1))];
		if (direction == Vector3.down) nextTile = tiles[manager.Index((int)currentPos.x, (int)(currentPos.y - 1))];

		//Debug.Log(direction);
		//Debug.Log(currentTile.isIntersection);
		//nextTile.occupied +" AND "+ 
		if (nextTile.occupied || currentTile.isIntersection)
		{
			//---------------------
			// IF WE BUMP INTO WALL
			if (nextTile.occupied && !currentTile.isIntersection)
			{

				//行き止まりの時
				if (direction == Vector3.up && currentTile.right == null && currentTile.left == null
					|| direction == Vector3.down && currentTile.right == null && currentTile.left == null
					|| direction == Vector3.left && currentTile.up == null && currentTile.down == null
					|| direction == Vector3.right && currentTile.up == null && currentTile.down == null)
				{
					direction = direction * -1;
					Debug.Log("行き止まり");
				}

				//Debug.Log("nextTile = " + nextTile.x + " " + nextTile.y);
				// if ghost moves to right or left and there is wall next tile
				else if (direction == Vector3.right || direction == Vector3.left)
				{
					if (currentTile.down == null) direction = Vector3.up;
					else direction = Vector3.down;

				}

				// if ghost moves to up or down and there is wall next tile
				else if (direction == Vector3.up || direction == Vector3.down)
				{
					if (currentTile.left == null) direction = Vector3.right;
					else direction = Vector3.left;

				}
			}

			//---------------------------------------------------------------------------------------
			// IF WE ARE AT INTERSECTION
			// calculate the distance to target from each available tile and choose the shortest one


			//Debug.Log("currentTile =" + currentTile.x + " " + currentTile.y);
			//Debug.Log("targetTile = " + targetTile.x +" "+targetTile.y);
			if (currentTile.isIntersection)
			{

				float dist1, dist2, dist3, dist4;
				dist1 = dist2 = dist3 = dist4 = 999999f;
				if (currentTile.up != null && !currentTile.up.occupied && !(direction.y < 0)) dist1 = manager.distance(currentTile.up, targetTile);
				if (currentTile.down != null && !currentTile.down.occupied && !(direction.y > 0)) dist2 = manager.distance(currentTile.down, targetTile);
				if (currentTile.left != null && !currentTile.left.occupied && !(direction.x > 0)) dist3 = manager.distance(currentTile.left, targetTile);
				if (currentTile.right != null && !currentTile.right.occupied && !(direction.x < 0)) dist4 = manager.distance(currentTile.right, targetTile);

				float min = Mathf.Min(dist1, dist2, dist3, dist4);
				if (min == dist1) direction = Vector3.up;
				if (min == dist2) direction = Vector3.down;
				if (min == dist3) direction = Vector3.left;
				if (min == dist4) direction = Vector3.right;

			}
		}

		// if there is no decision to be made, designate next waypoint for the ghost
		else
		{
			direction = direction;  // setter updates the waypoint
		}

		//animatorに値を渡す
		if(direction == Vector3.up)
		{
			animator.SetInteger("direction", 3);
		}
		else if (direction == Vector3.down)
        {
			animator.SetInteger("direction", 0);
		}
		else if (direction == Vector3.left)
		{
			animator.SetInteger("direction", 1);
		}
		else if (direction == Vector3.right)
		{
			animator.SetInteger("direction", 2);
		}



		//return direction;

	}
	TileManager.Tile GetTargetTilePerGhost()
	{
		Vector3 targetPos;
		TileManager.Tile targetTile;
		Vector3 dir = Vector2.zero;


		if (Move.direction == 0)
		{
			dir = Vector2.up;
		}
		else if (Move.direction == 1)
		{
			dir = Vector2.left;
		}
		else if (Move.direction == 2)
		{
			dir = Vector2.right;
		}
		else if (Move.direction == 3)
		{
			dir = Vector2.down;
		}

		// get the target tile position (round it down to int so we can reach with Index() function)
		switch (thisEnemyType)
		{
			default:
			case "blinky":  // target = pacman
				targetPos = new Vector3(target.position.x, target.position.y);
				targetTile = tiles[manager.Index((int)targetPos.x, (int)targetPos.y)];
				//Debug.Log("blinky's Target = " + targetTile.x + " " +targetTile.y);
				break;
			case "pinky":   // target = pacman + 4*pacman's direction (4 steps ahead of pacman)
				//dir = target.GetComponent<PlayerController>().getDir();
				targetPos = new Vector3(target.position.x, target.position.y) + 4 * dir;

				// if pacmans going up, not 4 ahead but 4 up and 4 left is the target
				// read about it here: http://gameinternals.com/post/2072558330/understanding-pac-man-ghost-behavior
				// so subtract 4 from X coord from target position
				if (dir == Vector3.up) targetPos -= new Vector3(4, 0, 0);

				targetTile = tiles[manager.Index((int)targetPos.x, (int)targetPos.y)];
				break;
			case "inky":    // target = ambushVector(pacman+2 - blinky) added to pacman+2
				//dir = target.GetComponent<PlayerController>().getDir();
				Vector3 blinkyPos = GameObject.Find("blinky").transform.position;
				Vector3 ambushVector = target.position + 2 * dir - blinkyPos;
				targetPos = new Vector3(target.position.x, target.position.y) + 2 * dir + ambushVector;
				targetTile = tiles[manager.Index((int)targetPos.x, (int)targetPos.y)];
				break;
			case "clyde":
				targetPos = new Vector3(target.position.x, target.position.y);
				targetTile = tiles[manager.Index((int)targetPos.x, (int)targetPos.y)];
				if (manager.distance(targetTile, currentTile) < 9)
					targetTile = tiles[manager.Index(0, 2)];
				break;
				/*
			default:
				targetTile = null;
				Debug.Log("TARGET TILE NOT ASSIGNED");
				break;
				*/

		}
		//Debug.Log(targetTile.x + " " + targetTile.y);
		return targetTile;
	}
}