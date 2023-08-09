using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum Level3State
{
    Exercise,
    Test,
    Result
}
public class Level3Manager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("Mission")]
    public GameObject mission;
    [SerializeField] TextMeshProUGUI timer_Text;
    [SerializeField] Slider survivalRate_Slider;
    [NonSerialized] public bool refuge, cover, gas;

    [Header("Object")]
    [SerializeField] Transform player;
    [SerializeField] GameObject warn_UI, result_Pass_UI, result_Fail_UI, refuge_UI, test_UI, runUI, refuge_Object, cover_Object, gas_Object, door, end, walk;
    [SerializeField] Transform refuge_SpawnPoint, playerCamera, cam;
    [SerializeField] Image result_Pass_Image, result_Fail_Image;

    public bool success = true;

    Level3State level3State;
    bool timerState, runState;
    float timer = 20;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        UpdateLevel3State(Level3State.Exercise);
        //player.position = new Vector3(6.174f, 1.833409f, -5.96f);
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("關卡");
    }

    void Update()
    {
        if (player.GetComponent<CharacterController>().velocity.z <= 0.2f && player.GetComponent<CharacterController>().velocity.z >= -0.2f)
        {
            walk.SetActive(false);
        }
        else
        {
            walk.SetActive(true);
        }

        if (timerState)
        {
            timer -= Time.deltaTime;
            timer_Text.text = timer.ToString("0");
            if (timer <= 0)
            {
                if(runState)
                {
                    StartCoroutine(Run());
                }
                else
                {
                    success = false;
                    UpdateLevel3State(Level3State.Result);
                }
                
            }
        }

        transform.position = playerCamera.position;
        transform.rotation = playerCamera.rotation;
    }

    public void UpdateLevel3State(Level3State newState)
    {
        level3State = newState;

        switch(newState)
        {
            case Level3State.Exercise:

                break;
            case Level3State.Test:
                timerState = true;
                mission.SetActive(false);
                timer_Text.transform.parent.gameObject.SetActive(true);
                refuge_Object.SetActive(false);
                refuge_Object.transform.position = refuge_SpawnPoint.position;
                refuge_Object.transform.rotation = refuge_SpawnPoint.rotation;
                refuge_Object.SetActive(true);
                survivalRate_Slider.transform.parent.gameObject.SetActive(true);
                warn_UI.SetActive(true);
                player.position = new Vector3(0.41f, 1.787f, -6.06f);
                test_UI.SetActive(true);
                Destroy(test_UI, 5);
                gas_Object.GetComponent<Gas>().Init();
                AudioManager.Instance.Stop();
                AudioManager.Instance.PlaySound("地震警報");
                break;
            case Level3State.Result:
                timerState = false;
                AudioManager.Instance.Stop();
                if(success)
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

    public void UpdateRefuge(bool state)
    {
        refuge = state;
        if(state)
        {
            mission.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "攜帶避難包 1/1";
            if(refuge_UI)
            {
                refuge_UI.SetActive(true);
                Destroy(refuge_UI, 5);
            }
            UpdateSurvivalRate(true, 20);
        }
        else
        {
            mission.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "攜帶避難包 0/1";
            UpdateSurvivalRate(false, 20);
        }
    }

    public void UpdateSurvivalRate(bool trigger, int value)
    {
        if (level3State == Level3State.Test)
        {
            if(trigger)
            {
                survivalRate_Slider.value += value;
            }
            else
            {
                survivalRate_Slider.value -= value;
            }
        }
    }

    public void CheckMission()
    {
        if(level3State == Level3State.Exercise)
        {
            if(refuge && cover && gas)
            {
                StartCoroutine(Test());
            }
        }
        else if(level3State == Level3State.Test)
        {
            if (survivalRate_Slider.value >= 60)
            {
                runState = true;
            }
        }
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(5);
        UpdateLevel3State(Level3State.Test);
        yield return new WaitForSeconds(5);
        warn_UI.GetComponent<AudioSource>().Play();
    }

    IEnumerator Run()
    {
        timerState = false;
        timer_Text.transform.parent.gameObject.SetActive(false);
        survivalRate_Slider.transform.parent.gameObject.SetActive(false);
        refuge_Object.SetActive(false);
        cover_Object.SetActive(false);
        gas_Object.SetActive(false);
        cam.gameObject.SetActive(true);
        AudioManager.Instance.PlaySound("地震來臨");
        yield return new WaitForSeconds(5);
        cam.gameObject.SetActive(false);
        door.SetActive(true);
        runUI.SetActive(true);
        Destroy(runUI, 5);
        end.SetActive(true);
    }

    IEnumerator ResultData(GameObject result)
    {
        yield return new WaitForSeconds(10);
        result.SetActive(true);
    }
}