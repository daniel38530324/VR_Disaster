using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HintLight : MonoBehaviour
{
    public static bool lightState = true;

    private Outline outline;
    private Flashing flashing;

    private bool stateToggle = true;
    private bool outlineLock = true;
   
    private void Awake()
    {
        outline = GetComponent<Outline>();
        //flashing = GetComponent<Flashing>();
    }
    void Start()
    {
        lightState = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (lightState)
        {
            if(stateToggle)
            {
                if(TryGetComponent<Flashing>(out flashing))
                {
                    flashing.StartGlinting();
                }
                
                stateToggle = false;
            }
        }
        else
        {
            if (!stateToggle)
            {
                if (TryGetComponent<Flashing>(out flashing))
                {
                    flashing.StopGlinting();
                }            
                stateToggle = true;
            }
        }
        
    }

    public void ToggleLightState(bool currentState)
    {
        lightState = currentState;
        stateToggle = currentState;
    }

    public void ToggleOutLine(bool currentState)
    {
        if(outlineLock)
        {
            outline.enabled = currentState;
        }
        else
        {
            outline.enabled = false;
        }
    }

    public void ToggleOutLineLock(bool currentLock)
    {
        outlineLock = currentLock;
    }
}
