using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // float speed = 2f;
    // float rotateSpeed = 10f;
    // bool walkinganim;
    // float speedvalue;
    // float directionvalue;
    // public Vector3 direction;
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // public void Update()
    // {
    //     float h = Input.GetAxis("Horizontal");
    //     float v = Input.GetAxis("Vertical");

    //     Vector3 direction = new Vector3(h, 0 ,v).normalized;

    //     transform.Translate(direction * speed * Time.deltaTime,Space.World);

    //     walkinganim = direction != Vector3.zero;

    //     if(direction.magnitude > 0.01){
    //         Quaternion targetRotate =  Quaternion.LookRotation(direction);
    //         transform.rotation =  Quaternion.Slerp(transform.rotation,targetRotate,rotateSpeed * Time.deltaTime);
    //         float speedvalue = direction.magnitude;
    //         float directionvalue = Vector3.Dot(transform.forward, direction.normalized);

    //     }

        
    //     // Debug.Log(direction);
    // }
    // public float speedValue()
    // {
    //     return speedvalue;
    // }

    // public float directionValue()
    // {
    //     return directionvalue;
    // }

    public float speed = 2f;
    public float rotateSpeed = 10f;
    public bool IsWalking;
    public float speedvalue;
    public float directionvalue;
    public Vector3 direction;
    [SerializeField] private Playeranimator playeranimator;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0 ,v).normalized;

        transform.Translate(direction * speed * Time.deltaTime,Space.World);

        if(direction.magnitude > 0.01){
            Quaternion targetRotate =  Quaternion.LookRotation(direction);
            transform.rotation =  Quaternion.Slerp(transform.rotation,targetRotate,rotateSpeed * Time.deltaTime);
            Debug.Log("i wanna fking cry");
        }

        float speedValue = direction.z;
        speedvalue = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
         float directionValue = Mathf.Sign(h);
        IsWalking = direction != Vector3.zero;
    }

    public bool iswalking(){
        return IsWalking;
    }

    public float speedValue(){
        return speedvalue;
    }

    public float directionValue(){
        return directionvalue;
    }
}
