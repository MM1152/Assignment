using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingMap : MonoBehaviour
{
    SpriteRenderer sp;
    public float size;
    public float speed;
    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }

    void Scroll(){
        transform.position += Vector3.left * Time.deltaTime * speed;

        if(sp.transform.position.x <= -size) sp.transform.position = new Vector3(size , transform.position.y);
    }
}
