using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckInZombies : MonoBehaviour
{
    Gun gun;
    void Awake()
    {
        gun = transform.parent.GetComponentInChildren<Gun>();    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Zombie") ) {
            gun?.IntriggerZombies.Add(collision.GetComponent<Zombie>());
        }        
    }
}
