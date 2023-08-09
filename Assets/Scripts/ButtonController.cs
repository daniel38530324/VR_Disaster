using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private UnityEvent onPressed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Button"))
        {
            onPressed?.Invoke();
            Destroy(gameObject);
        }
    }
}
