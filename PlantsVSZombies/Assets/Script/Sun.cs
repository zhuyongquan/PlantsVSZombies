using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//���  �����˶����ߺ�·��

public class Sun : MonoBehaviour
{
    public float moveDuration = 1;
    public int point = 50;

    public void LinearTo(Vector3 targetPos)
    {
        transform.DOMove(targetPos, moveDuration);
    }

    public void JumpTo(Vector3 targetPos) //��������Ķ���
    {
        targetPos.z = -1;
        Vector3 centerPos = (transform.position + targetPos)/2;
        float distance = Vector3.Distance(transform.position, targetPos);
        targetPos.y += (distance / 2);//���������·��
        transform.DOPath(new Vector3[] { transform.position, centerPos, targetPos }, 
            moveDuration,PathType.CatmullRom).SetEase(Ease.OutQuad);//·�� ʱ�� �������ͣ�ƽ�����ߣ�


    }

    public void OnMouseDown()
    {
      
        transform.DOMove(SunManager.Instance.GetSunPointTextPosition(), moveDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(
            ()=>
            { 
            Destroy(this.gameObject);
            SunManager.Instance.AddSun(point);
            }
            );

    }



}
