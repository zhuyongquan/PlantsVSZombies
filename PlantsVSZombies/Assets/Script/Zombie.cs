using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ZombieState 
{ 
    Move,
    Eat,
    Die
}


public class Zombie : MonoBehaviour
{
    ZombieState zombieState = ZombieState.Move; 
    private Rigidbody2D rgd;
    public float moveSpeed = 1.5f;
    private Animator anim;
    public int AtkValue = 30;//攻击力
    public float atkDuration = 2;//攻击频率
    private float atkTimer = 0;
    private Plant currentEatPlant; //当前正在吃的食物

    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (zombieState)
        {
            case ZombieState.Move:
                MoveUpdate();
                break;
            case ZombieState.Eat:
                EatUpdate();
                break;
            case ZombieState.Die:
                break;
            default:
                break;
        }
       
    }


    void MoveUpdate() 
    { 
     rgd.MovePosition(rgd.position + Vector2.left * moveSpeed * Time.deltaTime);
    
    }
    void EatUpdate()//僵尸的攻击行为
    {
        atkTimer += Time.deltaTime;
        if (atkTimer > atkDuration && currentEatPlant != null) 
        {
            currentEatPlant.TakeDamage(AtkValue);
            atkTimer = 0;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)//触发检测，僵尸和植物有无碰撞
    {
        if (collision.tag == "Plant") //碰到了植物
        {
            anim.SetBool("isAttacking",true);//攻击动画
            TransitionToEat();
            currentEatPlant = collision.GetComponent<Plant>();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)//触发检测，僵尸和植物有无碰撞
    {
        if (collision.tag == "Plant") //碰到了植物
        {
            anim.SetBool("isAttacking", false);//不攻击动画
            zombieState = ZombieState.Move;
        }

    }
    void TransitionToEat() 
    {
        zombieState = ZombieState.Eat;
        atkTimer = 0;
    }


}
