using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

enum CardState
{
    Cooling,
    WaittingSun,
    Ready
}

public enum PlantType
{
    SunFlower,
    PeaShooter
}

public class Card : MonoBehaviour
{

    private CardState cardState = CardState.Cooling;
    public PlantType plantType = PlantType.SunFlower;
    public GameObject CardLight;
    public GameObject CardGray;//因为可以直接用SetActive去控制使用，使用GameObject
    public Image CardMask;//要控制fillAmount，使用Image

    [SerializeField]
    public float cdTime  = 2;//冷却时间
    private float cdTimer = 0;//计时器

    public int needSunPoint = 50;




    private void Update()
    {
        switch (cardState)
        {
            case CardState.Cooling:
                CoolingUpdate();
                break;
            case CardState.WaittingSun:
                WaittingSunUpdate();
                break;
            case CardState.Ready:
                ReadyUpdate();
                break;
            default:
                break;
        }


    }
    void CoolingUpdate()//控制冷却渐变
    {
        cdTimer += Time.deltaTime;
        CardMask.fillAmount = (cdTime - cdTimer) / cdTime;
        if (cdTimer >= cdTime)//冷却结束
        {
            TransitionToWaittingSun();//切换到等待状态
        }

    }
    void WaittingSunUpdate()//判断阳光够不够
    {
        if (needSunPoint <= SunManager.Instance.SunPoint)
        {
            TransitionToReady();//够就转换，可以种植
        }

    }

    void ReadyUpdate()
    {

        if (needSunPoint > SunManager.Instance.SunPoint)//阳光不足
        {
            TransitionToWaittingSun();//切换到等待状态
        }

    }
    /// <summary>
    /// 转换函数
    /// </summary>
    void TransitionToWaittingSun() //切换到等待阶段
    {
        cardState = CardState.WaittingSun;

        CardLight.SetActive(false);//禁用light
        CardGray.SetActive(true);//启用Gray
        CardMask.gameObject.SetActive(false);// 禁用Mask
    }

    void TransitionToReady() //切换到可以种植阶段
    {
        cardState = CardState.Ready;

        CardLight.SetActive(true);
        CardGray.SetActive(false);
        CardMask.gameObject.SetActive(false);

    }

    void TransitionToCooling() //切换到冷却状态
    {
        cardState = CardState.Cooling;
        cdTimer = 0;
        CardLight.SetActive(false);
        CardGray.SetActive(true);
        CardMask.gameObject.SetActive(true);

    }

    public void OnClick()
    {
        if (needSunPoint > SunManager.Instance.SunPoint)
            return;
        //消耗阳光并种植
       
       bool isSuccess =   HandManager.instance.AddPlant(plantType);
        if (isSuccess)
        {
            SunManager.Instance.SubSun(needSunPoint);//减去对应阳光
            TransitionToCooling();
        }

    }


        


}
