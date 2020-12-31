using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void Missed(){
        // slider.value -= 0.1f;
		GameControl.healthValue -= 0.1f;
    }
    public void Hit(){
        // slider.value += 0.1f;
        if (GameControl.healthValue < 1f){
    		GameControl.healthValue += 0.1f;
        }
    }
}
