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
    private static Zombie _currnetPushAbleZombie;
    public static Zombie currentPushAbleZombie {
        get {
            return _currnetPushAbleZombie;
        }
        set {
            // 현재 PushAble Zombie 가 동일하면 bonusPushPower 업그레이드
            if(_currnetPushAbleZombie == value) {
                value.bonusPushPower += Vector2.right * 10f;
            }
            else if(_currnetPushAbleZombie != null){
                _currnetPushAbleZombie.bonusPushPower = Vector2.zero;
            }

            _currnetPushAbleZombie = value;
        }
    }

    public float jumpCoolTime;
    float currnetJumpTime;
    public bool pushAble;
    public Vector2 maxVelocity;


    public Vector2 jumpPower;
    public Vector2 pushPower;
    public Vector2 boostPoser;

    public Vector2 bonusPushPower;

    [SerializeField] Zombie behindZombie;
    private Vector3 rayStartPos;
    public override void Awake()
    {
        base.Awake();

        gameObject.tag = "Zombie";
        rg = GetComponent<Rigidbody2D>();
       
    }

    void OnEnable()
    {
        gameObject.layer = transform.root.gameObject.layer;
        transform.position += new Vector3(0f, 0f, transform.root.gameObject.transform.position.z);
        //ChangeAllChildLayer(transform);
    }

    void ChangeAllChildLayer(Transform transform)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.layer = gameObject.layer;
            ChangeAllChildLayer(transform.GetChild(i));
        }
    }

    void Update()
    {
        rayStartPos = transform.position + Vector3.up * 0.5f + Vector3.left * 0.7f ;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPos , Vector2.left , 0.3f , gameObject.layer);
        Debug.DrawRay(rayStartPos, Vector2.left * 0.3f , Color.red);
        if(hit.collider == null) {
            base.Move(Vector3.left);
        }   
        else {
            Debug.Log(hit.collider.name);
        }
        
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Zombie"))
        {
            ContactPoint2D contact = collision.contacts[0];
            
            SetBehindZombie(contact.normal.x, collision.gameObject);
            Clime(contact.normal.x);
            Push(contact.normal.y);
            Boost(contact.normal.y);
        }
    }



    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Zombie") )
        {
            if(behindZombie != null) {
                DeletBehindZombie(collision.collider.gameObject);
            }
            
        }
    }
    #region 좀비 순환
    void SetBehindZombie(float nomalX, GameObject Zombie)
    {
        
        if (nomalX < -0.95f)
        { // 뒤에 좀비가있을때 담아놓기
            behindZombie = Zombie.GetComponent<Zombie>();
        }
    }
    void DeletBehindZombie(GameObject Zombie)
    {
        if (behindZombie.gameObject == Zombie)
        { // 뒤에 좀비가 없어질때 실행
            behindZombie = null;
        }
    }
    void Clime(float nomalX)
    {
        if(behindZombie == null) currnetJumpTime -= Time.deltaTime;
        if (nomalX > 0.95f && currnetJumpTime <= 0)
        { // 앞에 좀비가 있을떄 점프 시도
            currnetJumpTime = jumpCoolTime;
            rg.AddForce(jumpPower);
        }

    }
    void Push(float nomalY)
    {
        if (nomalY < -0.95f && pushAble)
        { // 맨아랫줄 첫번째 좀비 위에 좀비가 올라갔을때
            Push();

            Debug.Log("Push " + gameObject.name);
        }

    }

    public void Push()
    {
        
        if (behindZombie != null)
        {
            behindZombie.Push();
        }
        rg.AddForce(pushPower + bonusPushPower);
        

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
