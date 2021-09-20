using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "EnemyStatus", menuName = "Create EnemyStatus")]
public class EnemyStatus : ScriptableObject
{
    public new string name;
    public int power;
    public float speed;
    public GameObject Object;
}
