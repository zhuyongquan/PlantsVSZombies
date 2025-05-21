using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plant
{
    public float shootDuration = 2;//���ʱ����
    private float shootTimer = 0;
    public Transform shootPointTransform;//��λ���λ��
    public PeaBullet peaBulletPrafab;
    public float BulletSpeed = 5;



    protected override void EnableUpdate()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer > shootDuration)
        {
            Shoot();
            shootTimer = 0;

        }

    }

    void Shoot()
    {
        PeaBullet pb =  GameObject.Instantiate(peaBulletPrafab, shootPointTransform.position,Quaternion.identity);
        pb.SetSpeed(BulletSpeed);

    }





}
