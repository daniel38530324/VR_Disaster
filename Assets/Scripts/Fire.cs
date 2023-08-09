using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] Level2Manager level2Manager;

    private Vector3 myScale;
    private float durationTime = 5, timer = 0;
    private bool turnOff;

    private void Start()
    {
        myScale = transform.localScale;
    }

    private void Update()
    {
        if(turnOff)
        {
            if (timer < durationTime)
            {
                timer += Time.deltaTime;
                transform.localScale = Vector3.Lerp(myScale, Vector3.zero, timer / durationTime);
            }
            else
            {
                if(CompareTag("WiresFire"))
                {
                    level2Manager.FirePass(Level2State.WiresOnFire);
                }
                else if(CompareTag("PotFire"))
                {
                    level2Manager.FirePass(Level2State.PotOnFire);
                }
                else if(CompareTag("BedFire"))
                {
                    level2Manager.success = true;
                    level2Manager.timeState = false;
                    level2Manager.UpdateLevel2State(4);
                }
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DryPowder"))
        {
            if(CompareTag("WiresFire") || CompareTag("PotFire"))
            {
                turnOff = true;
            }
        }
        else if(other.CompareTag("CO2"))
        {
            if (CompareTag("WiresFire") || CompareTag("PotFire"))
            {
                turnOff = true;
            }
        }
        else if(other.CompareTag("Bubble"))
        {
            if (CompareTag("PotFire") || CompareTag("BedFire"))
            {
                turnOff = true;
            }
        }
    }


    /*
    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Bubble"))
        {
            Debug.Log("123");
        }
    }

    private void OnParticleTrigger()
    {
        
    }*/
}
