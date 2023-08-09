using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private Level1Manager level1Manager;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject[] WindowPoint, windowLine;
    [SerializeField] private GameObject windowUI;

    private Vector3 startPoint, endPoint;
    private int currentPoint = 0;
    private int currentWindowLine = 0;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, transform.position);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WindowPoint")
        {
            lineRenderer.enabled = true;
            currentPoint++;

            if (currentPoint <= WindowPoint.Length - 1)
            {
                WindowPoint[currentPoint].SetActive(true);
                if(currentPoint == 8)
                {
                    level1Manager.windowCount++;
                    level1Manager.UpdateMission();
                }
            }
            else
            {
                windowUI.SetActive(true);
                level1Manager.windowCount++;
                level1Manager.UpdateMission();
                //GameManager.instance.DisableObject(windowUI, 5);
                GameManager.instance.DestoryObject(windowUI, 5);
            }
            WindowPoint[currentPoint - 1].SetActive(false);

            if (currentPoint % 2 != 0)
            {
                startPoint = WindowPoint[currentPoint - 1].transform.position;
                AudioManager.Instance.PlaySound("½¦±a");
                //endPoint = transform.position;
            }
            else
            {
                lineRenderer.enabled = false;
                windowLine[currentWindowLine].SetActive(true);
                currentWindowLine++;
                AudioManager.Instance.StopSound("½¦±a");
            }
        }
    }

    public void ToggleWindowLight(bool currentState)
    {
        if(currentState)
        {
            foreach (GameObject windowPoint in WindowPoint)
            {
                windowPoint.GetComponent<Flashing>().StartGlinting();
            }
        }
        else
        {
            foreach (GameObject windowPoint in WindowPoint)
            {
                windowPoint.GetComponent<Flashing>().StopGlinting();
            }
        }
       
    }
}
