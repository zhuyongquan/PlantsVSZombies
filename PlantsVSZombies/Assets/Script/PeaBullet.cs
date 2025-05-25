using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{

    private float speed = 3;//�ٶ�
    private int atkValue = 10;//�����㶹���˺�ֵ
    public GameObject peaBulletHitPrafab;

    public void SetATKValue(int atkValue) 
    {
        this.atkValue = atkValue;
    }
    public void SetSpeed(float speed) //�����ٶ�
    { 
    this.speed = speed;
    
    }

    private void Start()
    {
        Destroy(gameObject,8);//�㶹������10s
    }


    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);//  ���������� X ����ÿ�� xxx ����λ���ٶ���ǰ�ƶ���
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")//�㶹���˽�ʬ
        {
            Destroy(this.gameObject);
            collision.GetComponent<Zombie>().TakeDamage(atkValue);
            GameObject go =  GameObject.Instantiate(peaBulletHitPrafab,transform.position,Quaternion.identity);
            Destroy(go,1);
        }
    }










}
