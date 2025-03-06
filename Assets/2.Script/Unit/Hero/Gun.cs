using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Gun : Stat
{
    public float shootTime;
    float currentShootTime;


    Vector2 newPos;
    float rotZ;
    float clamp;
    void Awake()
    {
        currentShootTime = shootTime;
    }

    void Update()
    {
        
        if(Input.touchCount >= 1) {
            newPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - transform.position;
            rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
            clamp = Math.Clamp(rotZ - 35f , -130f , 60f);
            transform.rotation = Quaternion.Euler(0, 0, clamp);
            
        }
        Shoot();
    }

    void Shoot(){
        currentShootTime -= Time.deltaTime;

        if(currentShootTime <= 0) {
            currentShootTime = shootTime;
            
            GameObject pool = PoolingManager.Instance.ShowPool(PoolingManager.PoolType.Bullet);
            Bullet bullet = pool.GetComponent<Bullet>();

            bullet.Setting(damage , newPos.normalized);
            
            pool.transform.position = transform.position;

            pool.SetActive(true);
        }
    }
}
