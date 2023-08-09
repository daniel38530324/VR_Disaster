using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hand : MonoBehaviour
{
    public GameObject handPrefab; //手部預置物
    public InputDeviceCharacteristics inputDeviceCharacteristics; //輸入裝置的特徵
    public bool hideHandOnSelect = false;

    private InputDevice _tragetDevice;
    private Animator _handAnimator;
    private SkinnedMeshRenderer _hashMesh;
    
    public void HideHandOnSelect()
    {
        if(hideHandOnSelect)
        {
            _hashMesh.enabled = !_hashMesh.enabled; //將元件開關狀態設為相反
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        initializeHand();
    }

    void initializeHand()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices); //透過輸入裝置特徵去取得對應裝置

        if(devices.Count > 0) //應為取得(左手 or 右手)裝置只會有一個
        {
            _tragetDevice = devices[0];

            GameObject spawnHand = Instantiate(handPrefab, transform);
            _handAnimator = spawnHand.GetComponent<Animator>();
            _hashMesh = spawnHand.GetComponentInChildren<SkinnedMeshRenderer>(); //取得子物件的元件
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!_tragetDevice.isValid) //判斷設備是否有效
        {
            initializeHand();
        }
        else
        {
            updateHand();
        }
    }

    void updateHand()
    {
        if(_tragetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) //取得對應 CommonUsages 類別中按鈕的訊息 (CommonUsages類別擁有裝置按鈕的屬性)
        {
            _handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            _handAnimator.SetFloat("Trigger", 0);
        }

        if(_tragetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            _handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            _handAnimator.SetFloat("Grip", 0);
        }
    }
}
