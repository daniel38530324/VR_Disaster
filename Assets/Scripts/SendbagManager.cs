using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendbagManager : MonoBehaviour
{
    [SerializeField] GameObject[] sendbagPoint;
    [SerializeField] Level1Manager level1Manager;
    [SerializeField] GameObject sendbagUI;

    public void UpdateSendbagPoint(int currentNum)
    {
        if(currentNum + 1 <= sendbagPoint.Length - 1)
        {
            sendbagPoint[currentNum + 1].SetActive(true);
            level1Manager.sendbagCount++;
            level1Manager.UpdateMission();
        }
        else
        {
            level1Manager.sendbagCount++;
            level1Manager.UpdateMission();
            sendbagUI.SetActive(true);
            GameManager.instance.DestoryObject(sendbagUI, 5);
        }
    }

    public void ToggleSendbagLight(bool currentState)
    {
        if (currentState)
        {
            foreach (GameObject sendbagPoint in sendbagPoint)
            {
                sendbagPoint.GetComponent<Flashing>().StartGlinting();
            }
        }
        else
        {
            foreach (GameObject sendbagPoint in sendbagPoint)
            {
                sendbagPoint.GetComponent<Flashing>().StopGlinting();
            }
        }
    }
}
