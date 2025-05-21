using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager instance{get ;private set;}

    public List<Plant>  plantPrefabList;//��������ֲ��Prefab��List

    private Plant currentPlant;//��ǰҪ��ֲ��ֲ��
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        FollowCursor();//��ֲ���������ƶ�
    }



    public bool AddPlant(PlantType plantType  )//��ֲ���õ�����
    {
        if (currentPlant != null) return false;//���ж�������û��ֲ��


        Plant plantPrefab = GetPlantPrefab(plantType);
        if (plantPrefab == null) 
        {
            print("Ҫ��ֲ��ֲ�ﲻ����");return false;
        }
        currentPlant = GameObject.Instantiate(plantPrefab);//ʵ��������
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

    void FollowCursor()//�����淽��
    {
        if(currentPlant == null) return;
        Vector3 mouseWorldPosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);//�����λ��ת����������
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
