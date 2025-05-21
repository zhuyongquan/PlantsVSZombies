using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{
    public float produceDuration = 5;//生产阳光的时间间隔
    private float produceTimer = 0;
    private Animator anim;
    public GameObject sunPrefab;
    public float jumpMinDistance = 0.3f;
    public float jumpMaxDistance = 2.0f;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected  override void EnableUpdate()
    {
        produceTimer += Time.deltaTime;
        //print("111111111111");
        if (produceTimer > produceDuration)
        {
            produceTimer = 0;
            anim.SetTrigger("isGlowing");

        }
    }



    public void ProduceSun()
    {
        GameObject go = GameObject.Instantiate(sunPrefab,transform.position,Quaternion.identity);
        //生成阳光崩出来以后的随机位置
        float distance =  Random.Range(jumpMinDistance, jumpMaxDistance);
        distance =  Random.Range(0, 2) < 1? -distance : distance;
        Vector3 position = transform.position;
        position.x += distance;
        go.GetComponent<Sun>().JumpTo(position);

    }
}
