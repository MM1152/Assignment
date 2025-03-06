using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public Text text;
    void Awake()
    {
        text = GetComponent<Text>(); 
    }
    public void Setting(int damage , Vector2 pos){
        transform.position = Vector2.up + pos;
        text.text = damage + "";
    }
    private void OnEnable() {
        StartCoroutine(WaitForAnimation());
    }
    IEnumerator WaitForAnimation(){
        yield return new WaitForSeconds(0.4f);
        PoolingManager.Instance.ReturnPool(PoolingManager.PoolType.Damage , this.gameObject);
    }
}
