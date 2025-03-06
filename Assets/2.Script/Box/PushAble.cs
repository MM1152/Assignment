using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PushAble : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie")) {
            Zombie collsionZombie = collision.GetComponent<Zombie>();
            collsionZombie.pushAble = true;
            Zombie.currentPushAbleZombie = collsionZombie;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie")) {
            collision.GetComponent<Zombie>().pushAble = false;
        }
    }
}
