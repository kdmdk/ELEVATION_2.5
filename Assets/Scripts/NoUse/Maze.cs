using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// 穴掘りアルゴリズム
public class Maze : MonoBehaviour
{

    /// 2次元レイヤー
    class Layer2D
    {
        int _width; // 幅.
        int _height; // 高さ.
        int _outOfRange = -1; // 領域外を指定した時の値.
        int[] _values = null; // マップデータ.
        /// 幅.
        public int Width
        {
            get { return _width; }
        }
        /// 高さ.
        public int Height
        {
            get { return _height; }
        }

        /// 作成.
        public void Create(int width, int height)
        {
            _width = width;
            _height = height;
            _values = new int[Width * Height];
        }

        /// 座標をインデックスに変換する.
        public int ToIdx(int x, int y)
        {
            return x + (y * Width);
        }

        /// 領域外かどうかチェックする.
        public bool IsOutOfRange(int x, int y)
        {
            if (x < 0 || x >= Width)
            {
                return true;
            }
            if (y < 0 || y >= Height)
            {
                return true;
            }

            // 領域内.
            return false;
        }
        /// 値の取得.
        // @param x X座標.
        // @param y Y座標.
        // @return 指定の座標の値（領域外を指定したら_outOfRangeを返す）.
        public int Get(int x, int y)
        {
            if (IsOutOfRange(x, y))
            {
                return _outOfRange;
            }

            return _values[y * Width + x];
        }

        /// 値の設定.
        // @param x X座標.
        // @param y Y座標.
        // @param v 設定する値.
        public void Set(int x, int y, int v)
        {
            if (IsOutOfRange(x, y))
            {
                // 領域外を指定した.
                return;
            }

            _values[y * Width + x] = v;
        }

        /// 特定の値をすべてのセルに設定する.
        public void Fill(int v)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Set(x, y, v);
                }
            }
        }

        /// デバッグ出力.
        public void Dump()
        {
            Debug.Log("[Layer2D] (w,h)=(" + Width + "," + Height + ")");
            for (int y = 0; y < Height; y++)
            {
                string s = "";
                for (int x = 0; x < Width; x++)
                {
                    s += Get(x, y) + ",";
                }
                Debug.Log(s);
            }
        }
    }

    /// チップ定数
    const int CHIP_NONE = 0; // 通過可能
    const int CHIP_WALL = 1; // 通行不可

    /// 穴掘り開始
    void Start()
    {
        // ダンジョンを作る
        var layer = new Layer2D();
        // ダンジョンの幅と高さは奇数のみ
        layer.Create(10 + 1, 8 + 1);
        // すべて壁を埋める
        layer.Fill(CHIP_WALL);

        // 開始地点を決める
        int xstart = 2; // 開始地点は偶数でないといけない
        int ystart = 4; // 開始地点は偶数でないといけない

        // 穴掘り開始
        _Dig(layer, xstart, ystart);

        // 結果表示
        layer.Dump();
    }

    /// 穴を掘る
    void _Dig(Layer2D layer, int x, int y)
    {
        // 開始地点を掘る
        layer.Set(x, y, CHIP_NONE);

        Vector2[] dirList = {
      new Vector2 (-1, 0),
      new Vector2 (0, -1),
      new Vector2 (1, 0),
      new Vector2 (0, 1)
    };

        // シャッフル
        for (int i = 0; i < dirList.Length; i++)
        {
            var tmp = dirList[i];
            var idx = Random.Range(0, dirList.Length - 1);
            dirList[i] = dirList[idx];
            dirList[idx] = tmp;
        }

        foreach (var dir in dirList)
        {
            int dx = (int)dir.x;
            int dy = (int)dir.y;
            if (layer.Get(x + dx * 2, y + dy * 2) == 1)
            {
                // 2マス先が壁なので掘れる
                layer.Set(x + dx, y + dy, CHIP_NONE);

                // 次の穴を掘る
                _Dig(layer, x + dx * 2, y + dy * 2);
            }
        }
    }
}