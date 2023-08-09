using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SquatDown : MonoBehaviour
{
    [SerializeField] Level3Manager level3Manager;
    [SerializeField] Transform camera, squatDown_Point;
    [SerializeField] GameObject Table, cover_UI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(camera.position.y <= squatDown_Point.position.y)
        {
            Table.SetActive(false);
        }
        else
        {
            Table.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SquatDown_Place"))
        {
            level3Manager.cover = true;
            level3Manager.mission.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "¥Mß‰±ª≈È 1/1";
            level3Manager.UpdateSurvivalRate(true, 60);
            level3Manager.CheckMission();
            if(cover_UI)
            {
                cover_UI.SetActive(true);
                Destroy(cover_UI, 5);
            }
        }

        if(other.CompareTag("End"))
        {
            StartCoroutine(End());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SquatDown_Place"))
        {
            level3Manager.cover = false;
            level3Manager.mission.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "¥Mß‰±ª≈È 0/1";
            level3Manager.UpdateSurvivalRate(false, 60);
        }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(4);
        level3Manager.success = true;
        level3Manager.UpdateLevel3State(Level3State.Result);
    }
}
