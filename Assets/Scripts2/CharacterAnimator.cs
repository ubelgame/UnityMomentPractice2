using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{   
    private Animator animator;
    [SerializeField]private Character character;

    string IS_WALKING = "isWalking";
    string VELOCITYY = "velocityY";
    string IS_JUMPING = "isJumping";
    string IS_RUNJUMPING = "isRunJumping";
    float horizontal;
    float vertical;
    private Vector3 newMove3d;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationSet();
    }

    void AnimationSet(){
        horizontal = character.h;
        vertical = character.v;

        vertical = character.Isrunning && character.isWalking ? 1f : 0;

        animator.SetBool(IS_WALKING,character.IsWalking());
        animator.SetFloat(VELOCITYY,vertical);
        animator.SetBool(IS_JUMPING,!character.isGrounded);
        // animator.SetBool(IS_RUNJUMPING,character.Isrunning);


    }
}
