using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{

    private float speed = 3;
    public void SetSpeed(float speed) 
    { 
    this.speed = speed;
    
    }




     void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);//  将物体沿其 X 轴以每秒 xxx 个单位的速度向前移动。
    }












}
