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
    SunFlwer,
    PeaShooter
}

public class Card : MonoBehaviour
{

    private CardState cardState = CardState.Cooling;
    public PlantType plantType = PlantType.SunFlwer;
    public GameObject CardLight;
    public GameObject CardGray;
    public Image CardMask;
   
    [SerializeField]
    public float cdTime  = 2;//��ȴʱ��
    private float cdTimer = 0;//��ʱ��

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
    void CoolingUpdate()//������ȴ����
    {
        cdTimer += Time.deltaTime;
        CardMask.fillAmount = (cdTime - cdTimer) / cdTime;
        if (cdTimer >= cdTime)//��ȴ����
        {
            TransitionToWaittingSun();//�л����ȴ�״̬
        }

    }
    void WaittingSunUpdate()//�ж����⹻����
    {
        if (needSunPoint <= SunManager.Instance.SunPoint)
        {
            TransitionToReady();//����ת��
        }

    }

    void ReadyUpdate()
    {

        if (needSunPoint > SunManager.Instance.SunPoint)
        {
            TransitionToWaittingSun();//�л����ȴ�״̬
        }

    }
    /// <summary>
    /// ת������
    /// </summary>
    void TransitionToWaittingSun() //�л����ȴ��׶�
    {
        cardState = CardState.WaittingSun;

        CardLight.SetActive(false);//����light
        CardGray.SetActive(true);//����Gray
        CardMask.gameObject.SetActive(false);// ����Mask
    }

    void TransitionToReady() //�л���������ֲ�׶�
    {
        cardState = CardState.Ready;

        CardLight.SetActive(true);
        CardGray.SetActive(false);
        CardMask.gameObject.SetActive(false);

    }

    void TransitionToCooling() //�л�����ȴ״̬
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
        //�������Ⲣ��ֲ
        SunManager.Instance.SubSun(needSunPoint);//��ȥ��Ӧ����
        HandManager.instance.AddPlant(plantType);
        TransitionToCooling();

    }


        


}
