using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBarController : MonoBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("EXPSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = NewGame.EXP;
    }
}
