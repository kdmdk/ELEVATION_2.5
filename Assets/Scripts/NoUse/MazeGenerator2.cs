using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator2 : MonoBehaviour
{

    public int vertical = 15;
    public int horizontal = 15;

    public GameObject cube;

    int vi;
    int hi;

    public GameObject miner;

    void Start()
    {
        Vector3 pos = new Vector3(0, 0, 0);

        for (vi = 0; vi < vertical; vi++)
        {
            for (hi = 0; hi < horizontal; hi++)
            {
                GameObject copy = Instantiate(cube,
                    new Vector3(
                        pos.x + hi,
                        pos.y,
                        pos.z + vi
                    ), Quaternion.identity);

                copy.name = vi + "-" + hi.ToString();
            }
        }

        int ver1 = Random.Range(1, vertical - 1);
        int hor1 = Random.Range(1, horizontal - 1);

        GameObject start = GameObject.Find(ver1 + "-" + hor1);
        Destroy(start);

        GameObject minerObj = Instantiate(miner, Vector3.zero, Quaternion.identity);

        Miner2 minerScr = minerObj.GetComponent<Miner2>();

        minerScr.DoMining(ver1, hor1);
        /*
        //Walkerオブジェクト検索しWalkerスクリプトを取得、
        //そしてReceive関数に引数送って実行する
        GameObject walker = GameObject.Find("Main Camera");
        Walker walkerScr = walker.GetComponent<Walker>();
        walkerScr.Receive(ver1, hor1);
        */
    }

}