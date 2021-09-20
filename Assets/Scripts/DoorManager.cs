using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && NextFloor.isGetKey)
        {
            SoundManager.instance.PlaySE(10);
            Debug.Log("OPEN!");
            Destroy(this.gameObject);
        }
    }
}
