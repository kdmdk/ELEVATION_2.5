using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public EnemySpawn enemySpawn;
    //public UIAnimation uIAnimation;
    void Start()
    {
        enemySpawn.SetEnemy();
        enemySpawn.StartSpawn();
        enemySpawn.SetItem();
        enemySpawn.SetKey();
        enemySpawn.SetPoison();
        enemySpawn.SetSword();
        enemySpawn.SetJewel();
        enemySpawn.SetCoin();
        if((NewGame.Floor + 1) % 5 == 0)
        {
            enemySpawn.SetFirstAidKit();
            Debug.Log(NewGame.Floor);
        }
        //uIAnimation.UIEffect();
    }

    
    void Update()
    {
        
    }
}
