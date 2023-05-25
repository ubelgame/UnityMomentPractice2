using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float speed = 2f;
    float rotateSpeed = 10f;
    // Start is called before the first frame update
    void Start()
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
        }

        // Debug.Log(direction);
    }
}
