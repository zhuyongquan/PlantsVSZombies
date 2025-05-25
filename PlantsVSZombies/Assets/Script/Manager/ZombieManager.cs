using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum SpawnState
{ 
    NotStart,
    Spawning,
    End
}

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance { get; private set; }
    private SpawnState spawnState = SpawnState.NotStart;
    public Transform[] spawnPointList;
    public GameObject ZombiePrafab;
    private List<Zombie> zombieList= new List<Zombie>();//�ִ�Ľ�ʬ

    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        StartSpawn();
    }
    private void Update()
    {
        if (spawnState == SpawnState.End && zombieList.Count ==0) 
        {
            GameManager.instance.GameEndSucces();
        }
    }


    public void StartSpawn()
    {

        spawnState = SpawnState.Spawning;
        StartCoroutine(SpawnZombie());

    }


    public void Pause()
    {
        spawnState = SpawnState.End;
        foreach(Zombie zombie in zombieList) 
        {
            zombie.TransitionToPause();



        }


    }




    IEnumerator SpawnZombie() 
    {
        //�����ɵ�һ��
        for (int i = 0; i < 5; i++)
        {
            SpawARandomZombie();
            yield return new WaitForSeconds(3);


        }
         yield return new WaitForSeconds(3);//ÿ����Ϣ3s

        //�ڶ���
        for (int i = 0; i < 10; i++)
        {
            SpawARandomZombie();
            yield return new WaitForSeconds(3);


        }
   


         yield return new WaitForSeconds(3);//ÿ����Ϣ3s

       //���һ��������������
        AudioManager.instance.PlayClip(Config.last);
        //������
        for (int i = 0; i < 15; i++)
        {
            SpawARandomZombie();
            yield return new WaitForSeconds(3);


        }

        spawnState = SpawnState.End;



    }
// �������һֻ��ʬ
      private void SpawARandomZombie() 
       {
        if (spawnState == SpawnState.Spawning)
        {
         int index = Random.Range(0, spawnPointList.Length);
            GameObject go = GameObject.Instantiate(ZombiePrafab, spawnPointList[index].position,Quaternion.identity);
            zombieList.Add(go.GetComponent<Zombie>());
            
            go.GetComponent<SpriteRenderer>().sortingOrder = spawnPointList[index].GetComponent<SpriteRenderer>().sortingOrder;
        }

       }


    public void RemovZombie(Zombie zombie)
    {
        zombieList.Remove(zombie);
    }


}
