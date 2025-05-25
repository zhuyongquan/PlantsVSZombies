using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ZombieState 
{ 
    Move,
    Eat,
    Die,
    Pause
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
    public int HP = 100;
    private int currentHP ;  //当前HP
    public GameObject zombieHeadPrefab;
    private bool haveHead = true;//让头掉落动画只执行一次

    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHP = HP;
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
            AudioManager.instance.PlayClip(Config.eat);

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
       else if (collision.tag == "House") //碰到了房子
        {
            GameManager.instance.GameEndFail();
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


   public  void TransitionToPause()
    {
        zombieState = ZombieState.Pause;
        anim.enabled = false;
        rgd.bodyType = RigidbodyType2D.Static;
    }




    public void TakeDamage(int damage)
    {
        if (currentHP <= 0) return;
        this.currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = -1;
            Dead();
        }
        float hpPercent = currentHP *1f / HP;
        anim.SetFloat("HPPercent", hpPercent);
        if (hpPercent < 0.5f && haveHead)//当僵尸血量过半
        {
            haveHead = false;
            GameObject go = GameObject.Instantiate(zombieHeadPrefab,transform.position,Quaternion.identity);//僵尸头掉落
            Destroy(go,2);
        }

    }
    private void Dead()
    {
        if (zombieState == ZombieState.Die) return;
       
        zombieState = ZombieState.Die;
        GetComponent<Collider2D>().enabled = false;
        ZombieManager.Instance.RemovZombie(this);
        Destroy(this.gameObject,2);

    }


}
