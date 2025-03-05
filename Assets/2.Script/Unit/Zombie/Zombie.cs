using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Zombie : Creature
{
    Rigidbody2D rg;

    float jumpCoolTime = 2f;
    public bool pushAble;
    public Vector2 maxVelocity;


    public Vector2 jumpPower;
    public Vector2 pushPower;
    public Vector2 boostPoser;

    [SerializeField] Zombie behindZombie;

    float upgradePushPower = 1;
    public override void Awake()
    {
        base.Awake();

        gameObject.tag = "Zombie";
        rg = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        gameObject.layer = transform.root.gameObject.layer;
        transform.position += new Vector3(0f , 0f , transform.root.gameObject.transform.position.z);
        //ChangeAllChildLayer(transform);
    }

    void ChangeAllChildLayer(Transform transform){
        for(int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.layer = gameObject.layer;
            ChangeAllChildLayer(transform.GetChild(i));
        }
    }

    void Update()
    {
        if (!pushAble) {
            Move(Vector3.left);
            upgradePushPower = 1;
        }

        jumpCoolTime -= Time.deltaTime;
    }

    void Move(Vector3 movePos)
    {
        transform.position += movePos * speed * Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Zombie"))
        {
            ContactPoint2D contact = collision.contacts[0];

            SetBehindZombie(contact.normal.x , collision.gameObject);
            Clime(contact.normal.x);
            Push(contact.normal.y);
            Boost(contact.normal.y);
        }
    }
    #region 좀비 순환
    void SetBehindZombie(float nomalX , GameObject Zombie)
    {
        if (nomalX < -0.95f)
        { // 뒤에 좀비가있을때 담아놓기
            behindZombie = Zombie.GetComponent<Zombie>();
        }
    }
    void Clime(float nomalX)
    {
        if (nomalX > 0.95f && jumpCoolTime <= 0)
        { // 앞에 좀비가 있을떄 점프 시도
            jumpCoolTime = 1f;
            rg.velocity = Vector2.zero;
            rg.AddForce(jumpPower);
        }
    }
    void Push(float nomalY)
    {
        if (nomalY < -0.95f && pushAble)
        { // 맨아랫줄 첫번째 좀비 위에 좀비가 올라갔을때
            rg.AddForce(pushPower * upgradePushPower);
            upgradePushPower += 0.001f;
            if (behindZombie != null)
            {
                behindZombie.rg.AddForce(pushPower * upgradePushPower);
            }
        }
    }
    void Boost(float nomalY)
    {
        if (nomalY > 0.95)
        { // 좀비가 좀비를 밟고 걸어갈때
            rg.AddForce(boostPoser);
        }
    }
    #endregion
}
