using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour 
{
    [SerializeField] protected int hp;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] private Transform damageCanvas;

    public virtual void Awake(){
        damageCanvas = GameObject.Find("DamageCanvas").transform;
    }
    public virtual void Hit(int damage){
        hp -= damage;
        
        GameObject pool = PoolingManager.Instance.ShowPool(PoolingManager.PoolType.Damage);
        if(pool == null) {
            return;
        }
        pool.transform.SetParent(damageCanvas);
        pool.GetComponent<Damage>().Setting(damage , this);
        

        pool.SetActive(true);
    }

}
