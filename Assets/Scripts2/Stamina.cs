using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField]private Character character;
    // Start is called before the first frame update
    public float MaxStamina = 100f;
    public float CurrentStamina;
    float StaminaDrainrate = 20f;
    float StaminaRegenrate = 10f;
    public float StaminaThreshold = 20f;
    float rem;
    float ram;

    public static Stamina Instance { get; private set; }

    // Rest of the code...

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of Stamina script detected. Only one instance allowed.");
            Destroy(gameObject);
        }
    }
    void Start()
    {
        CurrentStamina = MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if(character.Isrunning){
            CurrentStamina -= StaminaDrainrate * Time.deltaTime;
        }
        if(CurrentStamina <= 0 ){
            CurrentStamina = 0f;
            character.Isrunning = false;
        }

       //for stamina regeneration while not running
       if(!character.Isrunning && CurrentStamina <= MaxStamina){
            CurrentStamina += StaminaRegenrate * Time.deltaTime;

            // CurrentStamina = Mathf.RoundToInt(CurrentStamina);
            // MaxStamina = Mathf.RoundToInt(MaxStamina);

            Mathf.Clamp(CurrentStamina,0f,MaxStamina);
            rem = CurrentStamina;
            ram = MaxStamina;
       }    

       Debug.Log(rem);
    }
}
