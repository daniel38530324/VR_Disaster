using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Sendbag : MonoBehaviour
{
    [SerializeField] int currentNum;
    [SerializeField] SendbagManager sendbagManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Sendbag"))
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<BoxCollider>().enabled = false;
            other.GetComponent<HintLight>().enabled = false;
            other.GetComponent<Flashing>().StopGlinting();
            other.GetComponent<Flashing>().enabled = false;
            other.GetComponent<Outline>().enabled = false;
            other.GetComponent<XRGrabInteractable>().enabled = false;
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
            sendbagManager.UpdateSendbagPoint(currentNum);
            Destroy(gameObject);
        }
    }
}
