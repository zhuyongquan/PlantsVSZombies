using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager instance{get ;private set;}

    public List<Plant>  plantPrefabList;//用来保存植物Prefab的List

    private Plant currentPlant;//当前要种植的植物
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        FollowCursor();//让植物跟随鼠标移动
    }



    public bool AddPlant(PlantType plantType  )//把植物拿到手上
    {
        if (currentPlant != null) return false;//先判断手上有没有植物


        Plant plantPrefab = GetPlantPrefab(plantType);
        if (plantPrefab == null) 
        {
            print("要种植的植物不存在");return false;
        }
        currentPlant = GameObject.Instantiate(plantPrefab);//实例化出来
        return true;


    }

    private Plant GetPlantPrefab(PlantType plantType) 
    {
        foreach (Plant plant in plantPrefabList)
        {
            if (plant.plantType == plantType)
            {
                return plant; 
            }
               
        }
        return null;


    }

    void FollowCursor()//鼠标跟随方法
    {
        if(currentPlant == null) return;
        Vector3 mouseWorldPosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);//把鼠标位置转换世界坐标
        mouseWorldPosition.z = -0.1f;
        currentPlant.transform.position = mouseWorldPosition;
    }

    public void OnCellClick(Cell cell) 
    {
        if (currentPlant == null) return;
        bool isSuccess = cell.AddPlant(currentPlant);

        if (isSuccess) 
        { 
            currentPlant = null;    
        }
       
    }

}
