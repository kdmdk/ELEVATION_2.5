using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    //float interval = 1.0f;
    //float time = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        /*
        if(Enemy.isBlink == true) { 
            time += Time.deltaTime;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            var renderComponent = player.GetComponent<Renderer>();
            renderComponent.enabled = !renderComponent.enabled;

            if (time > 1)
            {
                if (renderComponent.enabled = !renderComponent.enabled)
                {
                    renderComponent.enabled = renderComponent.enabled;
                    time = 0;
                }
                else
                {
                    renderComponent.enabled = !renderComponent.enabled;
                    time = 0;
                }
            }
        }
        */
        /*
        if (Enemy.isBlink == true)
        {
            StartCoroutine("playerBlink");
            //Enemy.isBlink = false;
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            var renderComponent = player.GetComponent<Renderer>();
            renderComponent.enabled = renderComponent.enabled;
        }
        */
    }
    /*
    IEnumerator playerBlink()
    {
        //int i = 0;
        //if(i < 3) { 
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        var renderComponent = player.GetComponent<Renderer>();
        renderComponent.enabled = !renderComponent.enabled;
        yield return new WaitForSeconds(interval);
            //renderComponent.enabled = renderComponent.enabled;
        //    i++;
        //}
    }
    */
}