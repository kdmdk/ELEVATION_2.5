using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TileManager : MonoBehaviour
{
	
	public class Tile
	{
		public int x { get; set; }
		public int y { get; set; }
		public bool occupied { get; set; }
		public int adjacentCount { get; set; }
		public bool isIntersection { get; set; }

		public Tile left, right, up, down;

		public Tile(int x_in, int y_in)
		{
			x = x_in; y = y_in;
			occupied = false;
			left = right = up = down = null;
		}
	};

	public List<Tile> tiles = new List<Tile>();

	// Use this for initialization
	void Start()
	{
		ReadTiles();
		
		int count = 0;
		foreach(Tile element in tiles)
        {
			count++;
			//Debug.Log(count + " " + element.x + " " + element.y + " " + element.occupied);
        }
		
	}

	// Update is called once per frame
	void Update()
	{
		//DrawNeighbors();

	}

	//-----------------------------------------------------------------------
	// hardcoded tile data: 1 = free tile, 0 = wall
	void ReadTiles()
	{
		
		//追加したコード
		//tileDataを変換してdataに代入
		int Max = MakeDungeon.X * MakeDungeon.Y;
		//string[] sAry = new string[Max];
		string data = "";
		for (int i = 0; i < Max; i++)
		{
			/*
			sAry[i] = MakeDungeon.tileData[i].ToString();
			//Debug.Log(MakeDungeon.tileData[i]);
			if (i % MakeDungeon.X == 0 && i != 0)
			{
			*/
			// TODO:改行を先にいれる
			if (i % (MakeDungeon.X) == 0 && i != 0) // TODO:MakeDungeon.XではなくMakeDungeon.X-1
			{
				//sAry[i] = "\n";
				data += "\n";
			}
			data += MakeDungeon.tileData[i].ToString();
		}

		//string data = string.Join("", sAry);
		Debug.Log(data); // これが綺麗な長方形である事を確認するべし

		/*
		int X = MakeDungeon.X, Y = MakeDungeon.Y;

		// TODO:dataの反映が違っていた：壁判定ができていない
		for (int i = 0; i < MakeDungeon.X; i++)
		{
			for (int j = 0; j < MakeDungeon.Y; j++)
			{
				Tile newTile = new Tile(i, j);
				int index = j + i * MakeDungeon.X;
				if (data[index] == '1')
				{
					// check for left-right neighbor
					if (i != 0 && data[index - 1] == '1')
					{
						// assign each tile to the corresponding side of other tile
						newTile.left = tiles[tiles.Count - 1];
						tiles[tiles.Count - 1].right = newTile;

						// adjust adjcent tile counts of each tile
						newTile.adjacentCount++;
						tiles[tiles.Count - 1].adjacentCount++;
					}
				}
				// if the current tile is not movable
				else
				{
					//Debug.Log(data[i]);
					newTile.occupied = true;
				}

			}
		}
		
		for (int j = 0; j < X; j++)
		{

			X = 1; // for every line
			for (int i = 0; i < Y; ++i)
			{
				Tile newTile = new Tile(X, Y);

				// if the tile we read is a valid tile (movable)
				if (data[i] == '1')
				{
					// check for left-right neighbor
					if (i != 0 && data[i - 1] == '1')
					{
						// assign each tile to the corresponding side of other tile
						newTile.left = tiles[tiles.Count - 1];
						tiles[tiles.Count - 1].right = newTile;

						// adjust adjcent tile counts of each tile
						newTile.adjacentCount++;
						tiles[tiles.Count - 1].adjacentCount++;
					}
				}
				// if the current tile is not movable
				else
				{
					Debug.Log(data[i]);
					newTile.occupied = true;
				}


				// check for up-down neighbor, starting from second row (Y<30)
				int upNeighbor = tiles.Count - data.Length; // up neighbor index
				if (Y < MakeDungeon.Ymax - 1 && !newTile.occupied && !tiles[upNeighbor].occupied)
				{
					tiles[upNeighbor].down = newTile;
					newTile.up = tiles[upNeighbor];

					// adjust adjcent tile counts of each tile
					newTile.adjacentCount++;
					tiles[upNeighbor].adjacentCount++;
				}

				tiles.Add(newTile);
				X++;
			}

			Y--;
		}
		*/

		int X = 1, Y = MakeDungeon.Y;
		// int X = 1, Y = 1;
		using (StringReader reader = new StringReader(data))
		{
			string line;
			while ((line = reader.ReadLine()) != null)
			{

				X = 1; // for every line
				for (int i = 0; i < line.Length; ++i)
				{
					//Debug.Log("X = " + X);
					//Debug.Log("Y = " + Y);

					Tile newTile = new Tile(X, Y);

					//Debug.Log("newTile = " + newTile.x + " " + newTile.y);

					// if the tile we read is a valid tile (movable)
					if (line[i] == '1')
					{
						// check for left-right neighbor
						if (i != 0 && line[i - 1] == '1')
						{
							// assign each tile to the corresponding side of other tile
							newTile.left = tiles[tiles.Count - 1];
							tiles[tiles.Count - 1].right = newTile;

							// adjust adjcent tile counts of each tile
							newTile.adjacentCount++;
							tiles[tiles.Count - 1].adjacentCount++;
						}
					}

					// if the current tile is not movable
					else newTile.occupied = true;

					// check for up-down neighbor, starting from second row (Y<30)
					int upNeighbor = tiles.Count - line.Length; // up neighbor index
					if (Y < MakeDungeon.Y && !newTile.occupied && !tiles[upNeighbor].occupied)
					{
						tiles[upNeighbor].down = newTile;
						newTile.up = tiles[upNeighbor];

						// adjust adjcent tile counts of each tile
						newTile.adjacentCount++;
						tiles[upNeighbor].adjacentCount++;
					}

					tiles.Add(newTile);
					X++;
				}

				Y--;
			}
		}


		// after reading all tiles, determine the intersection tiles
		foreach (Tile tile in tiles)
		{
			if (tile.adjacentCount > 2)
				tile.isIntersection = true;
		}
		
	}
	//-----------------------------------------------------------------------
	// Draw lines between neighbor tiles (debug)
	void DrawNeighbors()
	{
		foreach (Tile tile in tiles)
		{
			Vector3 pos = new Vector3(tile.x, tile.y, 0);
			Vector3 up = new Vector3(tile.x + 0.1f, tile.y + 1, 0);
			Vector3 down = new Vector3(tile.x - 0.1f, tile.y - 1, 0);
			Vector3 left = new Vector3(tile.x - 1, tile.y + 0.1f, 0);
			Vector3 right = new Vector3(tile.x + 1, tile.y - 0.1f, 0);

			if (tile.up != null) Debug.DrawLine(pos, up);
			if (tile.down != null) Debug.DrawLine(pos, down);
			if (tile.left != null) Debug.DrawLine(pos, left);
			if (tile.right != null) Debug.DrawLine(pos, right);
		}

	}


	//----------------------------------------------------------------------
	// returns the index in the tiles list of a given tile's coordinates

	public int Index(int X, int Y)
	{
		int Xmax = MakeDungeon.X;
		int Ymax = MakeDungeon.Y;
		return (Ymax - Y - 2) * Xmax + X + 1;

		// if the requsted index is in bounds
		//Debug.Log ("Index called for X: " + X + ", Y: " + Y);
		if (X >= 1 && X <= Xmax && Y <= Ymax && Y >= 1)
			return (Ymax - Y) * Xmax + X + 1;

		// else, if the requested index is out of bounds
		// return closest in-bounds tile's index 
		if (X < 1) X = 1;
		if (X > Xmax) X = Xmax;
		if (Y < 1) Y = 1;
		if (Y > Ymax) Y = Ymax;

		return (Ymax - Y) * Xmax + X - 1;
	}

	public int Index(Tile tile)
	{
		int Xmax = MakeDungeon.X;
		int Ymax = MakeDungeon.Y;
		return (Ymax - tile.y) * Xmax + tile.x - 1;
	}

	//----------------------------------------------------------------------
	// returns the distance between two tiles
	public float distance(Tile tile1, Tile tile2)
	{
		return Mathf.Sqrt(Mathf.Pow(tile1.x - tile2.x, 2) + Mathf.Pow(tile1.y - tile2.y, 2));
	}
}
