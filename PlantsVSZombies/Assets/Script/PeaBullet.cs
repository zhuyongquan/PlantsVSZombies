using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{

    private float speed = 3;//速度
    private int atkValue = 10;//单颗豌豆的伤害值
    public GameObject peaBulletHitPrafab;

    public void SetATKValue(int atkValue) 
    {
        this.atkValue = atkValue;
    }
    public void SetSpeed(float speed) //设置速度
    { 
    this.speed = speed;
    
    }

    private void Start()
    {
        Destroy(gameObject,8);//豌豆最多存在10s
    }


    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);//  将物体沿其 X 轴以每秒 xxx 个单位的速度向前移动。
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")//豌豆打到了僵尸
        {
            Destroy(this.gameObject);
            collision.GetComponent<Zombie>().TakeDamage(atkValue);
            GameObject go =  GameObject.Instantiate(peaBulletHitPrafab,transform.position,Quaternion.identity);
            Destroy(go,1);
        }
    }










}
