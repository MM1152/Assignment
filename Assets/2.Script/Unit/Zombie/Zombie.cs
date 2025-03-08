using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zombie : Creature
{
    Rigidbody2D rg;

    public float jumpCoolTime;
    float currnetJumpTime;
    public bool pushAble;
    public float pushTime;
    float currnetPushTime;
    public Vector2 maxVelocity;


    public Vector2 jumpPower;
    public Vector2 pushPower;
    public Vector2 boostPoser;

    public Vector2 bonusPushPower;

    [SerializeField] Zombie behindZombie;
    [SerializeField] Zombie underZombie;
    private Vector3 leftStartRay;
    private Vector3 rightStartRay;
    private Vector3 downStartRay;
    public bool push;
    public override void Awake()
    {
        base.Awake();

        gameObject.tag = "Zombie";
        rg = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        hp = 100;
        gameObject.layer = transform.root.gameObject.layer;
        transform.position += new Vector3(0f, 0f, transform.root.gameObject.transform.position.z);
    }


    
    protected void Move()
    {
       /*
        if( rg.velocity.y <= 0 && rg.velocity.y >= -5f && !push) {
            rg.velocity = new Vector2(-1 * speed , rg.velocity.y);
        }
        */
        
        transform.position += Vector3.left * Time.deltaTime * speed;
        
    }
 
    void FixedUpdate()
    {
        leftStartRay = transform.position + Vector3.up * 0.05f + Vector3.left * 0.5f ;
        rightStartRay = transform.position + Vector3.up * 0.7f + Vector3.right * 0.06f;
        downStartRay = transform.position + Vector3.down * 0.1f + Vector3.left * 0.4f ;


        RaycastHit2D leftHit = Physics2D.Raycast(leftStartRay , Vector2.left , 0.05f , layerMask : 1 << gameObject.layer /*| 1 << LayerMask.NameToLayer("Box")*/);
        RaycastHit2D rightHit = Physics2D.Raycast(rightStartRay , Vector2.right , 0.3f , layerMask : 1 << gameObject.layer);
        RaycastHit2D downHit = Physics2D.Raycast(downStartRay , Vector2.down , 0.5f , layerMask : 1 << gameObject.layer);

        Debug.DrawRay(rightStartRay, Vector2.right * 0.3f , Color.red);
        Debug.DrawRay(leftStartRay, Vector2.left * 0.05f , Color.red);
        Debug.DrawRay(downStartRay , Vector2.down * 0.5f , Color.red);

        if(leftHit.collider == null || !leftHit.collider.CompareTag("Zombie")) Move();
        else if(leftHit.collider.CompareTag("Zombie"))  {
            Clime(leftHit.collider.gameObject);
        }            
        
        if(rightHit.collider != null) SetBehindZombie(rightHit.collider.gameObject);
        else behindZombie = null;
        
        
        if(rg.velocity.y > 2) {
            rg.velocity = new Vector2(0f , rg.velocity.y);
        }

        if(pushAble) Debug.Log($"pushAble rg velocity {rg.velocity}"  );
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Zombie"))
        {
            ContactPoint2D contact = collision.contacts[0];

            Push(contact.normal);
            Boost(contact.normal.y);

        }

        
    }
    #region Around Zombie

    void SetBehindZombie( GameObject Zombie)
    {
        // 뒤에 좀비가있을때 담아놓기
        behindZombie = Zombie.GetComponent<Zombie>();   
    }

    void Clime(GameObject gameObject)
    {
        // 앞에 좀비가 있을떄 점프 시도
        if(rg.velocity.y >= 0) currnetJumpTime -= Time.deltaTime;
        
        if(currnetJumpTime <= 0){
            currnetJumpTime = jumpCoolTime;
            rg.AddForce(jumpPower);
        }
        // if(behindZombie != null) {
        //     currnetJumpTime = jumpCoolTime;
        // }
    }

    void Push(Vector2 nomal)
    {
        if (nomal.y < -0.8f && pushAble)
        { // 맨아랫줄 첫번째 좀비 위에 좀비가 올라갔을때
            currnetPushTime -= Time.deltaTime;
            if(currnetPushTime <= 0){
                Push();
                currnetPushTime = pushTime;
            }
        }
    }

    public void Push()
    {
        if (behindZombie != null)
        {
            behindZombie.Push();
        }
        rg.AddForce(pushPower /*+ bonusPushPowerz*/);
    }

    void Boost(float nomalY)
    {
        if (nomalY > 0.9)
        { // 좀비가 좀비를 밟고 걸어갈때
            rg.AddForce(boostPoser);
        }
    }
    #endregion
}

