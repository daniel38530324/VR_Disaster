using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Gas : MonoBehaviour
{
    [SerializeField] Level3Manager level3Manager;
    [SerializeField] GameObject fire, gas_UI;

    bool trigger = true, testTrigger;

    // Update is called once per frame
    void Update()
    {
        if(trigger)
        {
            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, 90, 0)) >= 90)
            {
                trigger = false;
                fire.SetActive(false);
                level3Manager.gas = true;
                level3Manager.mission.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "關閉瓦斯爐 1/1";
                level3Manager.UpdateSurvivalRate(true, 20);
                level3Manager.CheckMission();
                if(gas_UI)
                {
                    StartCoroutine(GasUI());
                }
            }
            else
            {
                fire.SetActive(true);
                level3Manager.gas = false;
                level3Manager.mission.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "關閉瓦斯爐 0/1";
                if(testTrigger)
                {
                    testTrigger = false;
                    level3Manager.UpdateSurvivalRate(false, 20); 
                }
                
            }
        }
    }

    public void Init()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
        trigger = true;
        testTrigger = true;
    }

    IEnumerator GasUI()
    {
        yield return new WaitForSeconds(3);
        gas_UI.SetActive(true);
        Destroy(gas_UI, 5);
    }
}
