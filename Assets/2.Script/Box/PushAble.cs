using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAble : MonoBehaviour
{
    Zombie pushAbleUnit;
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie")) {
            collision.GetComponent<Zombie>().pushAble = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie")) {
            collision.GetComponent<Zombie>().pushAble = false;
        }
    }
}
