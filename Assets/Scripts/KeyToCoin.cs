using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyToCoin : MonoBehaviour
{
    public GameObject Coin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NextFloor.isGetKey)
        {
            Instantiate(Coin,this.gameObject.transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
