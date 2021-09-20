using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void Update()
    {
        if (!Move.isAttack)
        {
            DestroyWeapon();
        }
    }
    void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
