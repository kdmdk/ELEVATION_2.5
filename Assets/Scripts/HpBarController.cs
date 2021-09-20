using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    Slider feverSlider;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.maxValue = NewGame.MAXLIFE;
        Debug.Log(NewGame.MAXLIFE);

        feverSlider = GameObject.Find("FeverSlider").GetComponent<Slider>();
        feverSlider.maxValue = NewGame.FeverTime;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = NewGame.Life;
        if (NextFloor.isFever)
        {
            feverSlider.value = NewGame.FeverTime - NextFloor.countTime;
        }
    }
}
