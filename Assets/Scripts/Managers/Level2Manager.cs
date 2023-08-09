using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum Level2State
{
    Explain,
    WiresOnFire,
    PotOnFire,
    BedOnFire,
    Result
}

public class Level2Manager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("Mission")]
    [SerializeField] TextMeshProUGUI mission_Text;
    [SerializeField] TextMeshProUGUI timer_Text;

    [Header("Object")]
    [SerializeField] Transform player;
    [SerializeField] GameObject wiresOnFire_UI, walk;
    [SerializeField] GameObject wiresOnFire_Pass_UI, potOnFire_UI, potOnFire_Pass_UI, bedOnFire_UI, bedOnFire_Pass_UI, wiresFire, potFire, bedFire, result_Pass_UI, result_Fail_UI;
    [SerializeField] Image result_Pass_Image, result_Fail_Image;

    [NonSerialized] public bool success = true, timeState;

    float timer = 33;
    Level2State level2State;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        UpdateLevel2State(0);
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Ãö¥d");
    }

    private void Update()
    {
        //Debug.Log("X = " + player.GetComponent<CharacterController>().velocity.x);
        //Debug.Log("Z = " + player.GetComponent<CharacterController>().velocity.z);
        if (player.GetComponent<CharacterController>().velocity.z <= 0.2f && player.GetComponent<CharacterController>().velocity.z >= -0.2f)
        {
            walk.SetActive(false);
        }
        else
        {
            walk.SetActive(true);
        }

        if (timeState)
        {
            timer -= Time.deltaTime * 0.5f;
            timer_Text.text = timer.ToString("0");

            if(timer <= 0)
            {
                timeState = false;
                success = false;
                UpdateLevel2State(4);
            }
        }
    }

    public void UpdateLevel2State(int newState)
    {
        level2State = (Level2State)newState;

        switch(level2State)
        {
            case Level2State.Explain:
                break;
            case Level2State.WiresOnFire:
                AudioManager.Instance.Stop();
                wiresOnFire_UI.SetActive(true);
                Destroy(wiresOnFire_UI, 5);
                wiresFire.SetActive(true);
                mission_Text.transform.parent.gameObject.SetActive(true);
                mission_Text.text = "·À±¼¹q½u¤õ·½";
                timeState = true;
                break;
            case Level2State.PotOnFire:
                potOnFire_UI.SetActive(true);
                Destroy(potOnFire_UI, 5);
                potFire.SetActive(true);
                mission_Text.text = "·À±¼ªoÁç¤õ·½";
                timer = 33;
                timeState = true;
                break;
            case Level2State.BedOnFire:
                bedOnFire_UI.SetActive(true);
                Destroy(bedOnFire_UI, 5);
                bedFire.SetActive(true);
                mission_Text.text = "·À±¼§É¤õ·½";
                timer = 33;
                timeState = true;
                break;
            case Level2State.Result:
                AudioManager.Instance.Stop();
                if (success)
                {
                    result_Pass_UI.SetActive(true);
                    StartCoroutine(ResultData(result_Pass_Image.gameObject));
                }
                else
                {
                    result_Fail_UI.SetActive(true);
                    StartCoroutine(ResultData(result_Fail_Image.gameObject));
                }
                break;
        }
    }

    public void FirePass(Level2State currentState)
    {
        switch (currentState)
        {
            case Level2State.WiresOnFire:
                StartCoroutine(FirePass(wiresOnFire_Pass_UI, 2));
                break;
            case Level2State.PotOnFire:
                StartCoroutine(FirePass(potOnFire_Pass_UI, 3));
                break;
            case Level2State.BedOnFire:
                StartCoroutine(FirePass(bedOnFire_Pass_UI, 4));
                break;
        }
    }
    IEnumerator FirePass(GameObject firePass, int nextState)
    {
        timeState = false;
        firePass.SetActive(true);
        yield return new WaitForSeconds(5);
        Destroy(firePass);
        UpdateLevel2State(nextState);
    }

    IEnumerator ResultData(GameObject result)
    {
        yield return new WaitForSeconds(8);
        result.SetActive(true);
    }

}
