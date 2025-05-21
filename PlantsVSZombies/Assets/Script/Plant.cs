using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlantState
{
    Disable,
    Enable
}

public class Plant : MonoBehaviour
{
    PlantState plantState = PlantState.Disable;
    public PlantType plantType = PlantType.SunFlower;


    private void Start()
    {
        TransitionToDisable();
    }


    private void Update()//ֲ��ķ�������Ϻ���ֲ�Ժ������״̬  ���Ͳ���
    {
        switch (plantState)
        {
            case PlantState.Disable:
                DisableUpdate();
                break;
            case PlantState.Enable:
                EnableUpdate();
                break;
            default:
                break;
        }
    }

    private void DisableUpdate()
    {
        
    }
    protected  virtual  void EnableUpdate()
    {
     
    }
    private void TransitionToDisable()//����
    {
        plantState = PlantState.Disable;
        GetComponent<Animator>().enabled = false;


    }

    public  void TransitionToEnable()//��
    {
        plantState = PlantState.Enable;
        GetComponent<Animator>().enabled = true;


    }

 

}
