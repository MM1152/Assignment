using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Stat
{
    public virtual void Update(){
        Die();
    }

    protected void Die(){
        if (hp <= 0) {
            if(this.GetType() == typeof(Zombie)) {
                PoolingManager.Instance.ReturnPool(PoolingManager.PoolType.ZombieMelee , this.gameObject);
            }
            
        }
    }
}
