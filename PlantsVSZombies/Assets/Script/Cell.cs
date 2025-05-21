using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Plant currentPlant;


    private void OnMouseDown()//检测鼠标点击
    {
        HandManager.instance.OnCellClick(this);//当Cell被点击
    }

    public bool AddPlant(Plant plant) 
    {
        if (currentPlant != null) return false;
        currentPlant = plant;
        currentPlant.transform.position = transform.position;
        plant.TransitionToEnable();
        return true;
    }

 


}
