using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CameraShake : MonoBehaviour
{
    /*
    [SerializeField] private float shakeMagnitude = 0.7f;
    [SerializeField] private float dampingSpeed = 1.0f;

    private InputDevice targetDevice;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private float shakeDuration = 0f;

    private InputFeatureUsage<Vector3> headsetPosition = CommonUsages.headsetPosition;
    private InputFeatureUsage<Quaternion> headsetRotation = CommonUsages.headsetRotation;

    private void Start()
    {
        originalPos = transform.localPosition;
        originalRot = transform.localRotation;
        targetDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
    }

    private void Update()
    {
        if (shakeDuration > 0f)
        {
            Vector3 newPos = originalPos + Random.insideUnitSphere * shakeMagnitude;
            Quaternion newRot = new Quaternion(
                originalRot.x + Random.Range(-shakeMagnitude, shakeMagnitude) * .2f,
                originalRot.y + Random.Range(-shakeMagnitude, shakeMagnitude) * .2f,
                originalRot.z + Random.Range(-shakeMagnitude, shakeMagnitude) * .2f,
                originalRot.w + Random.Range(-shakeMagnitude, shakeMagnitude) * .2f);

            Vector3 headsetPos;
            Quaternion headsetRot;
            var nodeState = new XRNodeState();
            if (InputDevices.GetDeviceAtXRNode(XRNode.Head).TryGetFeatureValue(headsetPosition, out headsetPos))
            {
                nodeState.TryGetFeatureValue(headsetRotation, out headsetRot);
                newPos = headsetPos + headsetRot * newPos;
                newRot = headsetRot * newRot;
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, Time.deltaTime * dampingSpeed);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, newRot, Time.deltaTime * dampingSpeed);
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = originalPos;
            transform.localRotation = originalRot;
        }
    }

    public void TriggerShake(float duration)
    {
        shakeDuration = duration;
    }
    */
}
