using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    float holdingTime;
    int damage;
    Vector2 direction;
    public void OnEnable()
    {
        holdingTime = 0f;
    }
    // Update is called once per frame
    public void Setting(int damage , Vector2 direction){
        this.damage = damage;
        this.direction = direction;
    }
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        holdingTime += Time.deltaTime;
        
        if(holdingTime >= 3f) PoolingManager.Instance.ReturnPool(PoolingManager.PoolType.Bullet , this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Stat stat;
        if(collision.TryGetComponent<Stat>(out stat)) {
            stat.Hit(damage);
        }

        PoolingManager.Instance.ReturnPool(PoolingManager.PoolType.Bullet , this.gameObject);
    }
}
