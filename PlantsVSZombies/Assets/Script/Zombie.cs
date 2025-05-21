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
    public int AtkValue = 30;//������
    public float atkDuration = 2;//����Ƶ��
    private float atkTimer = 0;
    private Plant currentEatPlant; //��ǰ���ڳԵ�ʳ��

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
    void EatUpdate()//��ʬ�Ĺ�����Ϊ
    {
        atkTimer += Time.deltaTime;
        if (atkTimer > atkDuration && currentEatPlant != null) 
        {
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


}
