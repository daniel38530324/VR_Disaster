using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] Level1Manager level1Manager;
    [SerializeField] GameObject ItemUI;
    [SerializeField] GameObject[] item_Bag;

    private Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();

    private void Start()
    {
        foreach(GameObject item in item_Bag)
        {
            dictionary.Add( item.name, item);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
            level1Manager.itemCount++;
            level1Manager.UpdateMission();
            if(level1Manager.itemCount >= 7)
            {
                ItemUI.SetActive(true);
                GameManager.instance.DestoryObject(ItemUI, 5);
            }

            dictionary[other.name].SetActive(true);
        }
    }

    public void ToggleItemLight(bool currentState)
    {
        if (currentState)
        {
            GetComponentInChildren<Flashing>().StartGlinting();
        }
        else
        {
            GetComponentInChildren<Flashing>().StopGlinting();
        }
    }
}
