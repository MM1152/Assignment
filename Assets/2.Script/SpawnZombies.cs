using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombies : MonoBehaviour
{
    public float spawnTimer;
    public float currnetSpawnTimer;
    
    [SerializeField] Transform[] spawnFloor;
    void Update()
    {
        currnetSpawnTimer -= Time.deltaTime;
        Spawn();
    }

    public void Spawn(){
        if(currnetSpawnTimer > 0) return;

        currnetSpawnTimer = spawnTimer;
        GameObject zombie = PoolingManager.Instance.ShowPool(PoolingManager.PoolType.ZombieMelee);
        if(zombie == null) {
            Debug.Log("Fail to Load Zombie");
            return;
        }
        int spawnindex = Random.Range(0 , 2);
        
        zombie.transform.SetParent(spawnFloor[spawnindex]);
        zombie.transform.position = spawnFloor[spawnindex].transform.Find("SpawnPos").transform.position;
        zombie.SetActive(true);
    }
}
