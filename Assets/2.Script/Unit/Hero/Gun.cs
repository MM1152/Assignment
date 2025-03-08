using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Stat
{
    public float shootTime;
    float currentShootTime;

    
    Vector2 newPos;
    float rotZ;
    float clamp;

    public Zombie targetZombie;
    public List<Zombie> IntriggerZombies = new List<Zombie>();
    public override void Awake()
    {
        currentShootTime = shootTime;
    }

    void Update()
    {
        
        if(Input.touchCount >= 1) { // TouchShoot
            newPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - transform.position;
            LookAt(newPos);
            targetZombie = null;
        }
        else if ( targetZombie == null  && IntriggerZombies.Count > 0){ // AutoShoot
            targetZombie = FindNearTarget();
        }
        else if( targetZombie != null && !targetZombie.gameObject.activeSelf) {
            targetZombie = FindNearTarget();
        }
        else if(targetZombie != null ) {
            newPos = targetZombie.transform.position - transform.position;
            LookAt(newPos);
        }
        Shoot();
    }

    void Shoot(){
        if(Input.touchCount == 0 && targetZombie == null) return ;

        currentShootTime -= Time.deltaTime;

        if(currentShootTime <= 0) {
            currentShootTime = shootTime;
            for(int i = 0; i < 5; i++) {
                GameObject pool = PoolingManager.Instance.ShowPool(PoolingManager.PoolType.Bullet);
                Bullet bullet = pool.GetComponent<Bullet>();

                bullet.Setting(damage , (newPos + new Vector2(0 , UnityEngine.Random.Range(-0.5f , 0.5f))).normalized);
                pool.transform.position = transform.position;
                pool.SetActive(true);
            }

        }
    }

    Zombie FindNearTarget(){
        float minDistance = 999999f;
        int index = -1;
        for(int i = 0; i < IntriggerZombies.Count; i++) {
            if(!IntriggerZombies[i].gameObject.activeSelf) {
                IntriggerZombies.RemoveAt(i);
                i--;
                continue;
            }

            float distance = Vector2.Distance( IntriggerZombies[i].transform.position , transform.position );
            if(minDistance > distance) {
                minDistance = distance;
                index = i;
            }
        }

        if(index != -1) return IntriggerZombies[index];
        
        return null;
    }
    
    float LookAt(Vector2 pos){
        rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        clamp = Math.Clamp(rotZ - 35f , -130f , 60f);
        transform.rotation = Quaternion.Euler(0, 0, clamp);
        
        return clamp;
    }
}
