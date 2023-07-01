using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]private Stamina stamina;
    
    float speed;
    float Movespeed = 2.0f;
    float Runspeed = 5.0f;
    float Rotationspeed = 12.0f;
    float Jumpforce = 4.5f;
    float fallSpeedMultiplier = 3f;
    public bool isWalking;
    public bool Isrunning;
    Vector3 Move3d;
    public float h;
    public float v;
    public bool isGrounded;
    private Rigidbody rb;
    public bool  isJumping;
    public bool isFalling;
    private Stamina staminaScript;

    // Start is called before the first frame update
    void Start()
    {
        staminaScript = Stamina.Instance;
        if (staminaScript == null)
        {
            Debug.LogError("Stamina script not found. Make sure it is present in the scene.");
        }

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMotions();
        ApplyFallMultiplier();
        CheckFalling();
    }

    public void PlayerMotions(){
        //Getting axis for movement
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        
        //Adding gotten axis to a vector
        Move3d = new Vector3(h, 0, v).normalized;

        //controls the speed if running or walking
        isWalking = Move3d.magnitude > 0;
      
        if(staminaScript != null){
            if(staminaScript.CurrentStamina >= staminaScript.StaminaThreshold){
                  Isrunning = isWalking && Input.GetKey(KeyCode.LeftShift);

            }
        }

        isJumping = Isrunning && Input.GetKeyDown(KeyCode.Space) && isGrounded; 

        speed = Isrunning ? Runspeed : Movespeed;

        

        //allowing the character to move based on the Move3d vector3 inputs
        transform.Translate(Move3d * speed * Time.deltaTime,Space.World);

        // //to make the character rotate 
        transform.forward = Vector3.Slerp(transform.forward,Move3d,Time.deltaTime * Rotationspeed);

        //jump
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            if(Isrunning){
                Jumpforward();
            }else{
                JumpForce();
            }
        }

    }

     private void ApplyFallMultiplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallSpeedMultiplier - 1) * Time.deltaTime;
        }

    }

    public bool IsWalking(){
        return isWalking;
    }

    private void JumpForce(){
        rb.AddForce(Vector3.up * Jumpforce , ForceMode.Impulse);
    }    

    private void Jumpforward(){
        Vector3 jumpdirection =transform.forward + Vector3.up;
        rb.AddForce(jumpdirection * Jumpforce , ForceMode.Impulse); 
    }

    private void CheckFalling(){
        if(!isGrounded && rb.velocity.y < -0.1f){
            isFalling = true;
        }else{
            isFalling = false;
        }
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision){
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
        }
    }
}
