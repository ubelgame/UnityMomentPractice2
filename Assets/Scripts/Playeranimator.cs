using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeranimator : MonoBehaviour
{
    [SerializeField] private Player player;
    private const string IS_WALKING = "isWalking";
    private const string speed = "speed";
    private const string direction = "direction";

    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // float snappedh;
        // float snappedv;

        // if(player.speedvalue > 0 && player.speedvalue < 0.55f){
        //     snappedh = 0.5f;
        // }
        // else if(player.speedvalue > 0.55f){
        //     snappedh = 1f;
        // }
        // else if(player.speedvalue < 0 && player.speedvalue > -0.55f){
        //     snappedh = -0.5f;
        // }
        // else if(player.speedvalue < -0.55f){
        //     snappedh = -1f;
        // }
        // else{
        //     snappedh = 0;
        // }

        // if(player.direction.magnitude > 0){
            animator.SetBool(IS_WALKING,player.iswalking());
            animator.SetFloat(speed,player.speedValue());
            animator.SetFloat(direction,player.directionValue());

            // if(player.direction.x = 1 && Input.GetKey("shift")){
            //     player.speed = 7;
            //     animator.SetFloat(speed,0.5f);
            // }
        
        // }
        // else{
        //     animator.SetBool(IS_WALKING,player.iswalking());
        //     animator.SetFloat(speed,0);
        //     animator.SetFloat(direction,0);
        //     Debug.Log("Your thing here");
        // }

        // Debug.Log(player.directionValue());

    }
}
