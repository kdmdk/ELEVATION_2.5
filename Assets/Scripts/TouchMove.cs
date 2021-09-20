using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{
	//public Transform target;
	public GameObject Player;
	private List<TileManager.Tile> tiles = new List<TileManager.Tile>();
	private TileManager manager;
	public TileManager.Tile targetTile;
	public Vector3 direction;
	TileManager.Tile currentTile;
	public TileManager.Tile nextTile = null;
	Animator animator;
	private Vector3 currentDirection = Vector3.down;
	public Transform target;
	Vector3 mousePosition;
	Vector3 targetPos;
	[SerializeField] Transform dummyPoint = default;
	TileManager.Tile tmpInterSection;
	private float remainingTimeToMove = 1.0f;

	void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		manager = GameObject.Find("EnemySpawn").GetComponent<TileManager>();
		tiles = manager.tiles;
		direction = currentDirection;
		animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		//target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{

		mousePosition = Input.mousePosition;
		targetPos = Camera.main.ScreenToWorldPoint(mousePosition);
		TileManager.Tile tmptargetTile = tiles[manager.Index((int)(targetPos.x), (int)(targetPos.y))];

		dummyPoint.position = new Vector2(tmptargetTile.x, tmptargetTile.y);
		Debug.Log("mousePosition:"+mousePosition);

		if (Input.GetMouseButtonUp(0))
		{


			mousePosition = Input.mousePosition;
			//Debug.Log("LeftClick:" + mousePosition);

			//targetPos = new Vector3(target.position.x, target.position.y);

			mousePosition.z = 10.0f;
			targetPos = Camera.main.ScreenToWorldPoint(mousePosition);
			//Debug.Log("mousePosition:" + mousePosition);


			//targetPos = mousePosition;
			//Debug.Log("targetTile:"+targetTile);
			// Debug.Log(targetPos.x + ":" + targetPos.y);
			// TODO:壁の判定がうまくいっていない = クリックした場所の認識がズレているので修正
			targetTile = tiles[manager.Index((int)(targetPos.x + 2), (int)(targetPos.y + 2))];
			// Debug.Log("targetTile" + targetTile.x + ":" + targetTile.y);
			if (targetTile.occupied)
			{
				targetTile = null;
				Debug.Log("壁クリック");
			}
			else
			{
				direction = Vector3.down;

			}
			//Debug.Log("targetTile:" + targetTile);
		}

		remainingTimeToMove -= Time.deltaTime;
		if (remainingTimeToMove < 0 && targetTile != null)
		{
			remainingTimeToMove = 0.1f;
			AILogic();
			currentDirection = direction;
			Player.transform.position += direction;
			if (currentTile == targetTile)
			{
				targetTile = null;
				Debug.Log("到着");
				direction = Vector3.zero;
			}

			else if(nextTile == tmpInterSection)
            {
				targetTile = null;
				Debug.Log("ループ");
				//direction = Vector3.zero;
			}

		}
	}


	public void AILogic()
	{
		
		//Debug.Log(direction);
		//Debug.Log(transform.position.x);
		// get current tile
		Vector3 currentPos = new Vector3(Player.transform.position.x, Player.transform.position.y);
		// Vector3 currentPos = targetPos;
		//Debug.Log("currentPos =" + currentPos);
		currentTile = tiles[manager.Index((int)currentPos.x, (int)currentPos.y)];

		//targetTile = GetTargetTilePerGhost();

		// get the next tile according to direction
		if (direction == Vector3.right) nextTile = tiles[manager.Index((int)(currentPos.x + 1), (int)currentPos.y)];
		if (direction == Vector3.left) nextTile = tiles[manager.Index((int)(currentPos.x - 1), (int)currentPos.y)];
		if (direction == Vector3.up) nextTile = tiles[manager.Index((int)currentPos.x, (int)(currentPos.y + 1))];
		if (direction == Vector3.down) nextTile = tiles[manager.Index((int)currentPos.x, (int)(currentPos.y - 1))];

		//Debug.Log(direction);
		//Debug.Log(currentTile.isIntersection);
		//nextTile.occupied +" AND "+
		//Debug.Log(nextTile.occupied + ":" + nextTile.isIntersection);
		// Debug.Log("currentTile" + currentTile.x + ":" + currentTile.y);
		// Debug.Log("currnetPos" + currentPos.x + ":" + currentPos.y);
		// Debug.Log("direction"+direction);
		// Debug.Log("nextTile"+nextTile.x +":"+ nextTile.y);

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
				tmpInterSection = currentTile;
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
		if (direction == Vector3.up)
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


		// Debug.Log(direction);
		//return direction;

	}
	/*
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
		
		//targetPos = Camera.main.ScreenToWorldPoint(mousePosition);
		//targetPos = new Vector3(target.position.x, target.position.y);
		targetTile = tiles[manager.Index((int)targetPos.x, (int)targetPos.y)];
		Debug.Log("targetTile:" + targetTile);
		Debug.Log(targetPos.x + ":" + targetPos.y);
		return targetTile;
	}
	*/
}