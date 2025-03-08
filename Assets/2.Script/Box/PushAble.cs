using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PushAble : MonoBehaviour
{
    public float checkTime;
    float currentCheckTime;

    Zombie currnetZombie;
    Zombie checkZombie;
    void Update()
    {
        currentCheckTime -= Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie")) {
            Zombie collsionZombie = collision.GetComponent<Zombie>();
            
            collsionZombie.pushAble = true;
            

            if(currnetZombie != null && currnetZombie == checkZombie) {
                currnetZombie.bonusPushPower += Vector2.right * 10f;
            }
            else if(currnetZombie != null){
                currnetZombie.bonusPushPower = Vector2.zero;
                checkZombie = currnetZombie;
            }

            currnetZombie = collsionZombie;
        }
    }

    // void OnTriggerStay2D(Collider2D collision) {
    //     if(currnetZombie != null && currnetZombie.push && currentCheckTime <= 0) {
    //         currentCheckTime = checkTime;
    //         if(checkZombie == currnetZombie) {
    //             currnetZombie.bonusPushPower += currnetZombie.pushPower;
    //         }
    //         else {
    //             currnetZombie.bonusPushPower = Vector2.zero;
    //             checkZombie = currnetZombie;
    //         }
    //     } 
    // }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie")) {
            collision.GetComponent<Zombie>().pushAble = false;
        }
    }
}
