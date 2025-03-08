using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public Text text;
    Stat stat;
    void Awake()
    {
        text = GetComponent<Text>(); 
    }
    public void Setting(int damage , Stat stat){
        this.stat = stat;
        text.text = damage + "";
    }
    private void OnEnable() {
        StartCoroutine(WaitForAnimation());
    }

    void Update()
    {
        transform.position = stat.transform.position + Vector3.up * 1.5f;
    }

    IEnumerator WaitForAnimation(){
        yield return new WaitForSeconds(0.4f);
        PoolingManager.Instance.ReturnPool(PoolingManager.PoolType.Damage , this.gameObject);
    }
}
