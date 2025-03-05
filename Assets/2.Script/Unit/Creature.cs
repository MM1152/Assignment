using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Stat
{
    // 좀비같은 경우에 Rigidbody Mass값 조절 필요할듯
    // 바닥 , 2층 , 3층 구분
    Animator ani;

    public enum AnimationStatus {
        None, IsAttacking , IsDead , IsIdle
    }

    public virtual void Awake()
    {
        ani = GetComponent<Animator>();
    }

    public virtual void PlayAnimation(AnimationStatus animationStatus){
        ani.SetBool(animationStatus.ToString() , true);
    }


}
