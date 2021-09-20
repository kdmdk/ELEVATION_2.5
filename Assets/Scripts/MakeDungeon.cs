using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MakeDungeon : MonoBehaviour
{
    // 設定する値
    public static int Xmax = 33;        //縦横のサイズ ※必ず奇数にすること
    public static int Ymax = 25;
    public GameObject wall;    //壁用オブジェクト
    public GameObject floor;   //床用オブジェクト
    public GameObject start;   //スタート地点に配置するオブジェクト
    public GameObject goal;    //ゴール地点に配置するオブジェクト

    public GameObject door;

    // 内部パラメータ
    public enum CellType { Wall, Path };   //セルの種類
    public static CellType[,] cells;

    private Vector2Int startPos;    //スタートの座標
    private Vector2Int goalPos;     //ゴールの座標

    //public Vector2Int nextStartPos = new Vector2Int(0,0);

    public GameObject Player;
    public GameObject Thief;
    public GameObject Warrior;

    public GameObject JonnySan;
    public GameObject ShimazuSan;
    public GameObject Cat;

    public GameObject UpotuKun;
    public GameObject Santa;

    public GameObject Merchant;
    public GameObject Witch;
    public GameObject JackO;
    public GameObject BunnyGirl;


    public static int[] tileData;
    public static int[] tileDataA;

    public static int[] tileDataOriginal;

    public static int X;
    public static int Y;

    [System.NonSerialized] public static Vector2 itemPos;

    public Vector2[] itemPositions = new Vector2[20];

    //int count = 0;
    private void Awake()
    {
        
        //マップ状態初期化
        cells = new CellType[Xmax, Ymax];

        //スタート地点の取得
        //startPos = nextStartPos;
        /*
        if (NewGame.Floor != 0)
        {
            startPos = nextStartPos;
        }
        else
        {
            startPos = GetStartPosition();
        }
        */

        if (NewGame.Floor % 2 == 0)
        {
            startPos = new Vector2Int(1, 1);
            Player.transform.position = new Vector3(1, 1, 0);
            Thief.transform.position = new Vector3(1, 1, 0);
            Warrior.transform.position = new Vector3(1, 1, 0);
            JonnySan.transform.position = new Vector3(1, 1, 0);
            ShimazuSan.transform.position = new Vector3(1, 1, 0);
            Cat.transform.position = new Vector3(1, 1, 0);
            UpotuKun.transform.position = new Vector3(1, 1, 0);
            Santa.transform.position = new Vector3(1, 1, 0);
            Merchant.transform.position = new Vector3(1, 1, 0);
            Witch.transform.position = new Vector3(1, 1, 0);
            JackO.transform.position = new Vector3(1, 1, 0);
            BunnyGirl.transform.position = new Vector3(1, 1, 0);
        }
        else
        {
            startPos = new Vector2Int(Xmax - 2, Ymax - 2);
            Player.transform.position = new Vector3(Xmax - 2, Ymax - 2, 0);
            Thief.transform.position = new Vector3(Xmax - 2, Ymax - 2, 0);
            Warrior.transform.position = new Vector3(Xmax - 2, Ymax - 2, 0);
            JonnySan.transform.position = new Vector3(Xmax - 2, Ymax - 2, 0);
            ShimazuSan.transform.position = new Vector3(Xmax - 2, Ymax - 2, 0);
            Cat.transform.position = new Vector3(Xmax - 2, Ymax - 2, 0);
            UpotuKun.transform.position = new Vector3(Xmax - 2, Ymax - 2, 0);
            Santa.transform.position = new Vector3(Xmax - 2, Ymax - 2, 0);
            Merchant.transform.position = new Vector3(Xmax - 2, Ymax - 2, 0);
            Witch.transform.position = new Vector3(Xmax - 2, Ymax - 2, 0);
            JackO.transform.position = new Vector3(Xmax - 2, Ymax - 2, 0);
            BunnyGirl.transform.position = new Vector3(Xmax - 2, Ymax - 2, 0);
        }



        //通路の生成
        //初回はゴール地点を設定する

        //goalPos = MakeMapInfo(startPos);

        /*
        int goalX = Random.Range(1, 15);
        int goalY = Random.Range(1, 11);

        cells[goalX - 1, goalY - 1] = CellType.Path;
        cells[goalX - 1, goalY] = CellType.Path;
        cells[goalX - 1, goalY + 1] = CellType.Path;
        cells[goalX, goalY - 1] = CellType.Path;
        cells[goalX, goalY] = CellType.Path;
        cells[goalX, goalY + 1] = CellType.Path;
        cells[goalX + 1, goalY - 1] = CellType.Path;
        cells[goalX + 1, goalY] = CellType.Path;
        cells[goalX + 1, goalY + 1] = CellType.Path;

        goalPos = new Vector2Int(goalX, goalY);

        nextStartPos = goalPos;
        */

        //プレイヤーキャラのインスタンス化
        if (SelectManager.playerType == "Hatman")
        {
            //Instantiate(Player, new Vector3(startPos.x, startPos.y, 0), Quaternion.Euler(0, 0, 0));
            Player.SetActive(true);
            Thief.SetActive(false);
            Warrior.SetActive(false);
            JonnySan.SetActive(false);
            ShimazuSan.SetActive(false);
            Cat.SetActive(false);
            UpotuKun.SetActive(false);
            Santa.SetActive(false);
            Merchant.SetActive(false);
            Witch.SetActive(false);
            JackO.SetActive(false);
            BunnyGirl.SetActive(false);
        }
        else if (SelectManager.playerType == "Thief")
        {
            //Instantiate(Thief, new Vector3(startPos.x, startPos.y, 0), Quaternion.Euler(0, 0, 0));
            Player.SetActive(false);
            Thief.SetActive(true);
            Warrior.SetActive(false);
            JonnySan.SetActive(false);
            ShimazuSan.SetActive(false);
            Cat.SetActive(false);
            UpotuKun.SetActive(false);
            Santa.SetActive(false);
            Merchant.SetActive(false);
            Witch.SetActive(false);
            JackO.SetActive(false);
            BunnyGirl.SetActive(false);
        }
        else if (SelectManager.playerType == "Warrior")
        {
            //Instantiate(Warrior, new Vector3(startPos.x, startPos.y, 0), Quaternion.Euler(0, 0, 0));
            Player.SetActive(false);
            Thief.SetActive(false);
            Warrior.SetActive(true);
            JonnySan.SetActive(false);
            ShimazuSan.SetActive(false);
            Cat.SetActive(false);
            UpotuKun.SetActive(false);
            Santa.SetActive(false);
            Merchant.SetActive(false);
            Witch.SetActive(false);
            JackO.SetActive(false);
            BunnyGirl.SetActive(false);
        }
        else if (SelectManager.playerType == "JonnySan")
        {
            //Instantiate(Warrior, new Vector3(startPos.x, startPos.y, 0), Quaternion.Euler(0, 0, 0));
            Player.SetActive(false);
            Thief.SetActive(false);
            Warrior.SetActive(false);
            JonnySan.SetActive(true);
            ShimazuSan.SetActive(false);
            Cat.SetActive(false);
            UpotuKun.SetActive(false);
            Santa.SetActive(false);
            Merchant.SetActive(false);
            Witch.SetActive(false);
            JackO.SetActive(false);
            BunnyGirl.SetActive(false);
        }
        else if (SelectManager.playerType == "ShimazuSan")
        {
            //Instantiate(Warrior, new Vector3(startPos.x, startPos.y, 0), Quaternion.Euler(0, 0, 0));
            Player.SetActive(false);
            Thief.SetActive(false);
            Warrior.SetActive(false);
            JonnySan.SetActive(false);
            ShimazuSan.SetActive(true);
            Cat.SetActive(false);
            UpotuKun.SetActive(false);
            Santa.SetActive(false);
            Merchant.SetActive(false);
            Witch.SetActive(false);
            JackO.SetActive(false);
            BunnyGirl.SetActive(false);
        }
        else if (SelectManager.playerType == "Cat")
        {
            //Instantiate(Warrior, new Vector3(startPos.x, startPos.y, 0), Quaternion.Euler(0, 0, 0));
            Player.SetActive(false);
            Thief.SetActive(false);
            Warrior.SetActive(false);
            JonnySan.SetActive(false);
            ShimazuSan.SetActive(false);
            Cat.SetActive(true);
            UpotuKun.SetActive(false);
            Santa.SetActive(false);
            Merchant.SetActive(false);
            Witch.SetActive(false);
            JackO.SetActive(false);
            BunnyGirl.SetActive(false);
        }
        else if (SelectManager.playerType == "UpotuKun")
        {
            //Instantiate(Warrior, new Vector3(startPos.x, startPos.y, 0), Quaternion.Euler(0, 0, 0));
            Player.SetActive(false);
            Thief.SetActive(false);
            Warrior.SetActive(false);
            JonnySan.SetActive(false);
            ShimazuSan.SetActive(false);
            Cat.SetActive(false);
            UpotuKun.SetActive(true);
            Santa.SetActive(false);
            Merchant.SetActive(false);
            Witch.SetActive(false);
            JackO.SetActive(false);
            BunnyGirl.SetActive(false);
        }
        else if (SelectManager.playerType == "Santa")
        {
            //Instantiate(Warrior, new Vector3(startPos.x, startPos.y, 0), Quaternion.Euler(0, 0, 0));
            Player.SetActive(false);
            Thief.SetActive(false);
            Warrior.SetActive(false);
            JonnySan.SetActive(false);
            ShimazuSan.SetActive(false);
            Cat.SetActive(false);
            UpotuKun.SetActive(false);
            Santa.SetActive(true);
            Merchant.SetActive(false);
            Witch.SetActive(false);
            JackO.SetActive(false);
            BunnyGirl.SetActive(false);
        }
        else if (SelectManager.playerType == "Merchant")
        {
            //Instantiate(Warrior, new Vector3(startPos.x, startPos.y, 0), Quaternion.Euler(0, 0, 0));
            Player.SetActive(false);
            Thief.SetActive(false);
            Warrior.SetActive(false);
            JonnySan.SetActive(false);
            ShimazuSan.SetActive(false);
            Cat.SetActive(false);
            UpotuKun.SetActive(false);
            Santa.SetActive(false);
            Merchant.SetActive(true);
            Witch.SetActive(false);
            JackO.SetActive(false);
            BunnyGirl.SetActive(false);
        }
        else if (SelectManager.playerType == "Witch")
        {
            //Instantiate(Warrior, new Vector3(startPos.x, startPos.y, 0), Quaternion.Euler(0, 0, 0));
            Player.SetActive(false);
            Thief.SetActive(false);
            Warrior.SetActive(false);
            JonnySan.SetActive(false);
            ShimazuSan.SetActive(false);
            Cat.SetActive(false);
            UpotuKun.SetActive(false);
            Santa.SetActive(false);
            Merchant.SetActive(false);
            Witch.SetActive(true);
            JackO.SetActive(false);
            BunnyGirl.SetActive(false);
        }
        else if (SelectManager.playerType == "JackO")
        {
            //Instantiate(Warrior, new Vector3(startPos.x, startPos.y, 0), Quaternion.Euler(0, 0, 0));
            Player.SetActive(false);
            Thief.SetActive(false);
            Warrior.SetActive(false);
            JonnySan.SetActive(false);
            ShimazuSan.SetActive(false);
            Cat.SetActive(false);
            UpotuKun.SetActive(false);
            Santa.SetActive(false);
            Merchant.SetActive(false);
            Witch.SetActive(false);
            JackO.SetActive(true);
            BunnyGirl.SetActive(false);
        }
        else if (SelectManager.playerType == "BunnyGirl")
        {
            //Instantiate(Warrior, new Vector3(startPos.x, startPos.y, 0), Quaternion.Euler(0, 0, 0));
            Player.SetActive(false);
            Thief.SetActive(false);
            Warrior.SetActive(false);
            JonnySan.SetActive(false);
            ShimazuSan.SetActive(false);
            Cat.SetActive(false);
            UpotuKun.SetActive(false);
            Santa.SetActive(false);
            Merchant.SetActive(false);
            Witch.SetActive(false);
            JackO.SetActive(false);
            BunnyGirl.SetActive(true);
        }
        //ゴールの設定
        if (NewGame.Floor % 2 == 0)
        {
            goalPos = new Vector2Int(Xmax - 2, Ymax - 2);
        }
        else
        {
            goalPos = new Vector2Int(1, 1);
        }

        
        //通路生成を繰り返して袋小路を減らす
        var tmpStart = goalPos;
        
        for (int i = 0; i < (Xmax + Ymax) * 3; i++)
        {
            MakeMapInfo(tmpStart);
            tmpStart = GetStartPosition();
        }
        

        
        AddFloor();
        MakeMapOriginal(tmpStart);

        //マップの状態に応じて壁と通路を生成する
        BuildDungeon();


        //スタート地点とゴール地点にオブジェクトを配置する
        //初回で取得したスタート地点とゴール地点は必ずつながっているので破綻しない
        var startObj = Instantiate(start, new Vector3(startPos.x, startPos.y, 0), Quaternion.Euler(0, 0, 0));
        var goalObj = Instantiate(goal, new Vector3(goalPos.x, goalPos.y, 0), Quaternion.Euler(0, 0, 0));

        startObj.transform.parent = this.transform;
        goalObj.transform.parent = this.transform;


        //SetItemPos();
        ToArray();
    }

    private void Start()
    {
        
    }

    //TileManagerに渡す用に作成
    void ToArray()
    {

        X = Xmax + 2;
        Y = Ymax + 2;

        tileDataA = new int[X * Y];
        int k = 0;
        for (int j = Ymax - 1; j >= 0; j--)
        {
            for (int i = 0;i < Xmax; i++)
            {
                if(cells[i,j] == CellType.Path)
                {
                    tileDataA[k] = 1;
                    //Debug.Log(i + " " + j);
                }
                else
                {
                    tileDataA[k] = 0;
                }
                k++;
            }
        }
        
        tileData = new int[X * Y];
        
        int count = 0;
        for (int i = 0; i < X * Y; i++)
        {            
            if(i < X || i % X == X - 1 || i % X == 0 || (i > X * (Y - 1)))
            {
                tileData[i] = 0;
            }
            else
            {
                tileData[i] = tileDataA[count];
                //Debug.Log(i + " " + count);
                count++;
            }
        }

        /*
        bool IsOutOfBounds(int x, int y) => (x < 0 || y < 0 || x >= Xmax || y >= Ymax);

        //縦横1マスずつ大きくループを回し、外壁とする
        for (int i = -1; i <= Xmax; i++)
        {
            for (int j = -1; j <= Ymax; j++)
            {
                //範囲外、または壁の場合に壁オブジェクトを生成する
                if (IsOutOfBounds(i, j) || cells[i, j] == CellType.Wall)
                {
                    //var wallObj = Instantiate(wall, new Vector3(i, j, -1), Quaternion.Euler(0, 0, 0));
                    //wallObj.transform.parent = this.transform;
                    tileData[k] = 1;
                }
                else
                {
                    tileData[k] = 0;
                }
                //全ての場所に床オブジェクトを生成
                //var floorObj = Instantiate(floor, new Vector3(i, j, 0), Quaternion.Euler(0, 0, 0));
                //floorObj.transform.parent = this.transform;
            }
        }
        */


/*
        for (int i = 0; i < X * Y; i += X)
        {
            Debug.Log(tileData[i] + "" + tileData[i + 1] + "" + tileData[i + 2] + "" + tileData[i + 3] + "" + tileData[i + 4] + "" + tileData[i + 5] + "" + tileData[i + 6] + "" + tileData[i + 7] + "" + tileData[i + 8] + "" + tileData[i + 9] + "" + tileData[i + 10] + "" + tileData[i + 11] + "" + tileData[i + 12]
                + "" + tileData[i + 13] + "" + tileData[i + 14] + tileData[i + 15] + "" + tileData[i + 16] + "" + tileData[i + 17] + "" + tileData[i + 18] + "" + tileData[i + 19] + "" + tileData[i + 20] + "" + tileData[i + 21] + "" + tileData[i + 22] + "" + tileData[i + 23] + "" + tileData[i + 24] + "" + tileData[i + 25] + "" + tileData[i + 26] + "" + tileData[i + 27] + "" + tileData[i + 28]
                + tileData[i + 29] + "" + tileData[i + 30] + "" + tileData[i + 31] + "" + tileData[i + 32] + "" + tileData[i + 33] + "" + tileData[i + 34]);
        }
*/
    }

    //アイテムの設置
    public static void SetItemPos()
    {
        int xPosition = Random.Range(3, Xmax - 5);
        int yPosition = Random.Range(3, Ymax - 5);

        while (cells[xPosition, yPosition] != CellType.Path)
        {
            xPosition = Random.Range(3, Xmax - 5);
            yPosition = Random.Range(3, Ymax - 5);
        }
        /*
        cells[xPosition - 1, yPosition] = CellType.Path;
        cells[xPosition + 1, yPosition] = CellType.Path;
        cells[xPosition - 1, yPosition - 1] = CellType.Path;
        cells[xPosition, yPosition - 1] = CellType.Path;
        cells[xPosition + 1, yPosition - 1] = CellType.Path;
        cells[xPosition - 1, yPosition + 1] = CellType.Path;
        cells[xPosition, yPosition + 1] = CellType.Path;
        cells[xPosition + 1, yPosition + 1] = CellType.Path;
        */

        itemPos = new Vector2(xPosition, yPosition);
        //itemPositions[count] = new Vector2(xPosition, yPosition);
        //Debug.Log(i + ":" + itemPositions[i]);
        //count++;

    }

    // スタート地点の取得
    private Vector2Int GetStartPosition()
    {
        //ランダムでx,yを設定
        int randomX = Random.Range(0, Xmax);
        int randomY = Random.Range(0, Ymax);

        //x、yが両方共偶数になるまで繰り返す
        while (!(randomX % 2 == 0 && randomY % 2 == 0))
        {
            randomX = Mathf.RoundToInt(Random.Range(0, Xmax));
            randomY = Mathf.RoundToInt(Random.Range(0, Ymax));
        }

        return new Vector2Int(randomX, randomY);
    }


    // マップ生成
    private Vector2Int MakeMapInfo(Vector2Int _startPos)
    {
        //スタート位置配列を複製
        var tmpStartPos = _startPos;

        //移動可能な座標のリストを取得
        var movablePositions = GetMovablePositions(tmpStartPos);

        //移動可能な座標がなくなるまで探索を繰り返す
        while (movablePositions != null)
        {
            //移動可能な座標からランダムで1つ取得し通路にする
            var tmpPos = movablePositions[Random.Range(0, movablePositions.Count)];
            cells[tmpPos.x, tmpPos.y] = CellType.Path;

            //元の地点と通路にした座標の間を通路にする
            var xPos = tmpPos.x + (tmpStartPos.x - tmpPos.x) / 2;
            var yPos = tmpPos.y + (tmpStartPos.y - tmpPos.y) / 2;
            cells[xPos, yPos] = CellType.Path;

            //移動後の座標を一時変数に格納し、再度移動可能な座標を探索する
            tmpStartPos = tmpPos;
            movablePositions = GetMovablePositions(tmpStartPos);
        }
        //探索終了時の座標を返す
        return tmpStartPos;
    }

    //追加したプログラム２
    private Vector2Int MakeMapOriginal(Vector2Int _startPos)
    {
        if (NewGame.Floor == 4)
        {
            int[,] tileDataOriginal_05 = {  { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1 },
                                            { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,1,0,1,1,1 },
                                            { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,1,0,1,1,1 },
                                            { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,1,0,0,1,0 },
                                            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                            { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                            { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                            { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                            { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                            { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                            { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                            { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                            { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                            { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                            { 1,1,1,1,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                            { 1,1,1,1,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                            { 1,1,1,1,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                         };

            for (int i = 0; i < tileDataOriginal_05.GetLength(0); i++)
            {
                for (int j = 0; j < tileDataOriginal_05.GetLength(1) - 1; j++)
                {
                    int y = tileDataOriginal_05.GetLength(0) - 1 - i;
                    if (tileDataOriginal_05[i, j] == 1)
                    {
                        cells[j, y] = CellType.Path;
                    }
                    else
                    {
                        cells[j, y] = CellType.Wall;
                    }
                }
            }
        }
        else if (NewGame.Floor == 9)
        {
            int[,] tileDataOriginal_10 ={{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,1,1,1,1,1 },
                                        { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,1,1,1,1,1 },
                                        { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                        { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                        { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                        { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                        { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                        { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                        { 1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 0,1,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                        { 1,1,1,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                        { 1,1,1,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0 },
                                        { 1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                     };

            for (int i = 0; i < tileDataOriginal_10.GetLength(0); i++)
            {
                for (int j = 0; j < tileDataOriginal_10.GetLength(1) - 1; j++)
                {
                    int y = tileDataOriginal_10.GetLength(0) - 1 - i;
                    if (tileDataOriginal_10[i, j] == 1)
                    {
                        cells[j, y] = CellType.Path;
                    }
                    else
                    {
                        cells[j, y] = CellType.Wall;
                    }
                }
            }
        }
        else if (NewGame.Floor == 14)
        {
            int[,] tileDataOriginal_15 ={{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,0 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1 },
                                        { 1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1 },
                                        { 1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1 },
                                        { 1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1 },
                                        { 1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                                     };

            for (int i = 0; i < tileDataOriginal_15.GetLength(0); i++)
            {
                for (int j = 0; j < tileDataOriginal_15.GetLength(1) - 1; j++)
                {
                    int y = tileDataOriginal_15.GetLength(0) - 1 - i;
                    if (tileDataOriginal_15[i, j] == 1)
                    {
                        cells[j, y] = CellType.Path;
                    }
                    else
                    {
                        cells[j, y] = CellType.Wall;
                    }
                }
            }
        }
            return _startPos;
    }

        //追加したプログラム
        void AddFloor()
        {
        //ランダムで通路をつくる
        

        
        
        //外周を通路にする
        for (int i = 0; i < Xmax; i++)
        {
            cells[i, 0] = CellType.Path;
            cells[i, Ymax - 1] = CellType.Path;
        }
        for (int i = 0; i < Ymax; i++)
        {
            cells[0, i] = CellType.Path;
            cells[Xmax - 1, i] = CellType.Path;
        }
        
        //壁を減らす
        int delX = 0;
        int delY = 0;
        int reduceWall = 35;

        for (int i = 0; i < reduceWall; i++)
        {
            delX = Random.Range(1, Xmax - 1);
            delY = Random.Range(1, Ymax - 1);
            if (cells[delX, delY] == CellType.Path)
            {
                i--;
            }
            cells[delX, delY] = CellType.Path;
        }
        


        /*
        //外周に壁を一つづつ作る
        int x = Random.Range(1, Xmax - 1);
        int x2 = Random.Range(1, Xmax - 1);
        cells[x, Ymax - 1] = CellType.Wall;
        cells[x2, 0] = CellType.Wall;
        
        int y = Random.Range(1, Ymax - 1);
        int y2 = Random.Range(1, Ymax - 1);
        cells[Xmax - 1, y] = CellType.Wall;
        cells[0, y2] = CellType.Wall;
        */

        /*
        cells[0, 0] = CellType.Path;
        cells[0, 1] = CellType.Path;
        cells[0, 2] = CellType.Path;
        cells[1, 0] = CellType.Path;
        cells[1, 1] = CellType.Path;
        cells[1, 2] = CellType.Path;
        cells[2, 0] = CellType.Path;
        cells[2, 1] = CellType.Path;
        cells[2, 2] = CellType.Path;

        cells[Xmax - 3, Ymax - 3] = CellType.Path;
        cells[Xmax - 3, Ymax - 2] = CellType.Path;
        cells[Xmax - 3, Ymax - 1] = CellType.Path;
        cells[Xmax - 2, Ymax - 3] = CellType.Path;
        cells[Xmax - 2, Ymax - 2] = CellType.Path;
        cells[Xmax - 2, Ymax - 1] = CellType.Path;
        cells[Xmax - 1, Ymax - 3] = CellType.Path;
        cells[Xmax - 1, Ymax - 2] = CellType.Path;
        cells[Xmax - 1, Ymax - 1] = CellType.Path;
        */
        if(NewGame.Floor % 2 != 0)
        {
            cells[0, 0] = CellType.Path;
            cells[0, 1] = CellType.Path;
            cells[0, 2] = CellType.Path;
            cells[0, 3] = CellType.Wall;
            cells[0, 4] = CellType.Path;

            cells[1, 0] = CellType.Path;
            cells[1, 1] = CellType.Path;
            cells[1, 2] = CellType.Path;
            cells[1, 3] = CellType.Path;
            cells[1, 4] = CellType.Path;

            cells[2, 0] = CellType.Path;
            cells[2, 1] = CellType.Path;
            cells[2, 2] = CellType.Path;
            cells[2, 3] = CellType.Wall;
            cells[2, 4] = CellType.Path;

            cells[3, 0] = CellType.Wall;
            cells[3, 1] = CellType.Wall;
            cells[3, 2] = CellType.Wall;
            cells[3, 3] = CellType.Wall;
            cells[3, 4] = CellType.Path;

            cells[Xmax - 3, Ymax - 3] = CellType.Path;
            cells[Xmax - 3, Ymax - 2] = CellType.Path;
            cells[Xmax - 3, Ymax - 1] = CellType.Path;
            cells[Xmax - 2, Ymax - 3] = CellType.Path;
            cells[Xmax - 2, Ymax - 2] = CellType.Path;
            cells[Xmax - 2, Ymax - 1] = CellType.Path;
            cells[Xmax - 1, Ymax - 3] = CellType.Path;
            cells[Xmax - 1, Ymax - 2] = CellType.Path;
            cells[Xmax - 1, Ymax - 1] = CellType.Path;

            Instantiate(door, new Vector3(1, 3, 0), Quaternion.Euler(0, 0, 0));

        }
        else
        {
            cells[0, 0] = CellType.Path;
            cells[0, 1] = CellType.Path;
            cells[0, 2] = CellType.Path;
            cells[1, 0] = CellType.Path;
            cells[1, 1] = CellType.Path;
            cells[1, 2] = CellType.Path;
            cells[2, 0] = CellType.Path;
            cells[2, 1] = CellType.Path;
            cells[2, 2] = CellType.Path;

            cells[Xmax - 4, Ymax - 5] = CellType.Path;
            cells[Xmax - 4, Ymax - 4] = CellType.Wall;
            cells[Xmax - 4, Ymax - 3] = CellType.Wall;
            cells[Xmax - 4, Ymax - 2] = CellType.Wall;
            cells[Xmax - 4, Ymax - 1] = CellType.Wall;

            cells[Xmax - 3, Ymax - 5] = CellType.Path;
            cells[Xmax - 3, Ymax - 4] = CellType.Wall;
            cells[Xmax - 3, Ymax - 3] = CellType.Path;
            cells[Xmax - 3, Ymax - 2] = CellType.Path;
            cells[Xmax - 3, Ymax - 1] = CellType.Path;

            cells[Xmax - 2, Ymax - 5] = CellType.Path;
            cells[Xmax - 2, Ymax - 4] = CellType.Path;
            cells[Xmax - 2, Ymax - 3] = CellType.Path;
            cells[Xmax - 2, Ymax - 2] = CellType.Path;
            cells[Xmax - 2, Ymax - 1] = CellType.Path;

            cells[Xmax - 1, Ymax - 5] = CellType.Path;
            cells[Xmax - 1, Ymax - 4] = CellType.Wall;
            cells[Xmax - 1, Ymax - 3] = CellType.Path;
            cells[Xmax - 1, Ymax - 2] = CellType.Path;
            cells[Xmax - 1, Ymax - 1] = CellType.Path;

            Instantiate(door, new Vector3(Xmax - 2, Ymax - 4, 0), Quaternion.Euler(0, 0, 0));
             
        }


    }

    // 移動可能な座標のリストを取得する
    private List<Vector2Int> GetMovablePositions(Vector2Int _startPos)
    {
        //可読性のため座標を変数に格納
        var x = _startPos.x;
        var y = _startPos.y;

        //移動方向毎に2つ先のx,y座標を仮計算
        var positions = new List<Vector2Int> {
            new Vector2Int(x, y + 2),
            new Vector2Int(x, y - 2),
            new Vector2Int(x + 2, y),
            new Vector2Int(x - 2, y)
        };

        //移動方向毎に移動先の座標が範囲内かつ壁であるかを判定する
        //真であれば、返却用リストに追加する
        var movablePositions = positions.Where(p => !IsOutOfBounds(p.x, p.y) && cells[p.x, p.y] == CellType.Wall);

        return movablePositions.Count() != 0 ? movablePositions.ToList() : null;
    }


    //与えられたx、y座標が範囲外の場合真を返す
    private bool IsOutOfBounds(int x, int y) => (x < 0 || y < 0 || x >= Xmax || y >= Ymax);


    //パラメータに応じてオブジェクトを生成する
    private void BuildDungeon()
    {
        //縦横1マスずつ大きくループを回し、外壁とする
        for (int i = -1; i <= Xmax; i++)
        {
            for (int j = -1; j <= Ymax; j++)
            {
                //範囲外、または壁の場合に壁オブジェクトを生成する
                if (IsOutOfBounds(i, j) || cells[i, j] == CellType.Wall)
                {
                    var wallObj = Instantiate(wall, new Vector3(i, j, -1), Quaternion.Euler(0, 0, 0));
                    wallObj.transform.parent = this.transform;
                }

                //全ての場所に床オブジェクトを生成
                var floorObj = Instantiate(floor, new Vector3(i, j, 0), Quaternion.Euler(0, 0, 0));
                floorObj.transform.parent = this.transform;
            }
        }
    }
}