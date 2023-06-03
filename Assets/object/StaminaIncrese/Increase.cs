using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Increase : MonoBehaviour
{
    private Stamina staminaScript;
    // Start is called before the first frame update
    void Start()
    {
        staminaScript = Stamina.Instance;
        if(staminaScript == null){
            Debug.LogError("Stamina script not found. Make sure it is present in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider item){
        if(item.gameObject.CompareTag("Player")){
            if(staminaScript != null){
                staminaScript.MaxStamina = staminaScript.MaxStamina + 20f;
                Destroy(gameObject);
            }
        }   
    }
}
