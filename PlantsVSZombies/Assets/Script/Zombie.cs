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
    public int AtkValue = 30;//������
    public float atkDuration = 2;//����Ƶ��
    private float atkTimer = 0;
    private Plant currentEatPlant; //��ǰ���ڳԵ�ʳ��
    public int HP = 100;
    private int currentHP ;  //��ǰHP
    public GameObject zombieHeadPrefab;
    private bool haveHead = true;//��ͷ���䶯��ִֻ��һ��

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
    void EatUpdate()//��ʬ�Ĺ�����Ϊ
    {
        atkTimer += Time.deltaTime;
        if (atkTimer > atkDuration && currentEatPlant != null) 
        {
            AudioManager.instance.PlayClip(Config.eat);

            currentEatPlant.TakeDamage(AtkValue);
            atkTimer = 0;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)//������⣬��ʬ��ֲ��������ײ
    {
        if (collision.tag == "Plant") //������ֲ��
        {
            anim.SetBool("isAttacking",true);//��������
            TransitionToEat();
            currentEatPlant = collision.GetComponent<Plant>();
        }
       else if (collision.tag == "House") //�����˷���
        {
            GameManager.instance.GameEndFail();
        }




    }

    private void OnTriggerExit2D(Collider2D collision)//������⣬��ʬ��ֲ��������ײ
    {
        if (collision.tag == "Plant") //������ֲ��
        {
            anim.SetBool("isAttacking", false);//����������
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
        if (hpPercent < 0.5f && haveHead)//����ʬѪ������
        {
            haveHead = false;
            GameObject go = GameObject.Instantiate(zombieHeadPrefab,transform.position,Quaternion.identity);//��ʬͷ����
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
