using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public enum PoolType {
        None , ZombieMelee , Bullet
    }

    Dictionary<PoolType , GameObject> datas = new Dictionary<PoolType , GameObject>(); 
    Dictionary<PoolType , Queue<GameObject>> pools = new Dictionary<PoolType, Queue<GameObject>>(); 

    private static PoolingManager instance;
    public static PoolingManager Instance { get { return instance; } }


    void Awake()
    {
        if(instance == null) {
            instance = this;
            GameObject[] poolsDatas = Resources.LoadAll<GameObject>("Pools");
            for(int i = 0; i < poolsDatas.Length; i++) {
                PoolType poolType = Enum.Parse<PoolType>(poolsDatas[i].name);
                
                pools.Add(poolType , new Queue<GameObject>());
                datas.Add(poolType , poolsDatas[i]);
            }
        }
    }


    public GameObject ShowPool(PoolType poolType) {
        Debug.Log(poolType);
        if(!pools.ContainsKey(poolType)) {
            Debug.Log("Fail To find pool type");
            return null;
        }

        GameObject pool;

        if(pools[poolType].Count > 0) {
            pool = pools[poolType].Dequeue();
            return pool;
        }
        else {
            pool = Instantiate(datas[poolType] , transform);
            pool.SetActive(false);
            return pool;
        }
    }


    public void ReturnPool(PoolType poolType , GameObject returnData){
        if(!pools.ContainsKey(poolType)) {
            return;
        }

        pools[poolType].Enqueue(returnData);
        returnData.transform.SetParent(transform);
        returnData.SetActive(false);
    }
}
