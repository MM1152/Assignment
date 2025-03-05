using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] protected int hp;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    public virtual void Hit(int damage){
        hp -= damage;
    }

}
