using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] protected int hp;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] private Transform damageCanvas;

    protected virtual void Awake(){
        damageCanvas = GameObject.Find("DamageCanvas").transform;
    }
    public virtual void Hit(int damage){
        hp -= damage;
        
        GameObject pool = PoolingManager.Instance.ShowPool(PoolingManager.PoolType.Damage);
        pool.GetComponent<Damage>().Setting(damage , transform.position);
        pool.transform.SetParent(damageCanvas);

        pool.SetActive(true);
    }

}
