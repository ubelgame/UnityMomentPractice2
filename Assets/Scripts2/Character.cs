using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]private Stamina stamina;
    [SerializeField]Transform cam;
    
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
        
         // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //show coursor
        if(Input.GetKeyDown(KeyCode.LeftAlt)){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
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
        h = Input.GetAxis("Horizontal") * speed;
        v = Input.GetAxis("Vertical") *speed;
 
        //camera transform
        Vector3 cameraforward = cam.forward;
        Vector3 cameraright = cam.right;

        cameraforward.y = 0;
        cameraright.y = 0;

        //camera and movement rotation retargeting
        Vector3 cameraforwardretargeting = v * cameraforward;
        Vector3 camerarightretargeting = h * cameraright;
        
        //Adding gotten axis to a vector
        Move3d = cameraforwardretargeting + camerarightretargeting;
        Move3d = Move3d.normalized;

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
        transform.Translate(Move3d  * Time.deltaTime,Space.World);
        // rb.velocity = new Vector3(Move3d.x, rb.velocity.y, Move3d.z).normalized;

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
