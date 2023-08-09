using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum Level1State
{
    WatchNews,
    NewsHint,
    Buy,
    Layout,
    Result
}

public class Level1Manager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("Mission")]
    public int windowCount; 
    public int sendbagCount;
    public int itemCount;

    [Header("Object")]
    [SerializeField] Transform player;
    [SerializeField] GameObject news, walk;
    [SerializeField] GameObject newsHint_UI, list_UI, spawnPoint, result_Pass_UI, result_Fail_UI;
    [SerializeField] TextMeshProUGUI text_window, text_sendbag, text_item;
    [SerializeField] Toggle[] toggles;
    [SerializeField] GameObject[] items;
    [SerializeField] ResultData resultData;
    [SerializeField] Image result_Pass_Image, result_Fail_Image;
    

    Level1State level1State;
    private int index = 0;

    private void Awake()
    {
        if(GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        UpdateLevel1State(Level1State.WatchNews);
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("關卡");
    }

    private void Update()
    {
        if (player.GetComponent<CharacterController>().velocity.z <= 0.2f && player.GetComponent<CharacterController>().velocity.z >= -0.2f)
        {
            walk.SetActive(false);
        }
        else
        {
            walk.SetActive(true);
        }
    }

    public void UpdateLevel1State(Level1State newState)
    {
        level1State = newState;

        switch (newState)
        {
            case Level1State.WatchNews:
                news.SetActive(true);
                StartCoroutine(Couter_WatchNews(16));
                break;
            case Level1State.NewsHint:
                newsHint_UI.SetActive(true);
                break;
            case Level1State.Buy:
                list_UI.SetActive(true);
                break;
            case Level1State.Layout:
                break;
            case Level1State.Result:
                Result result;
                AudioManager.Instance.Stop();
                if (windowCount >= 2 && sendbagCount >= 9 && itemCount == 7)
                {
                    result_Pass_UI.SetActive(true);
                    result = global::Result.Level1Pass;
                    StartCoroutine(ResultData(result_Pass_Image.gameObject));
                    result_Pass_Image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = resultData.result[(int)result];
                    
                }
                else
                {
                    result_Fail_UI.SetActive(true);
                    StartCoroutine(ResultData(result_Fail_Image.gameObject));
                }
                /*
                else if(windowCount != 2 && sendbagCount != 9 && itemCount != 7)
                {
                    result_Fail_UI.SetActive(true);
                    result = global::Result.Level1_Window;
                    result_Fail_Image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "失敗!";
                    result_Fail_Image.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = resultData.result[(int)result] + "\n" +resultData.result[(int)result+1] + "\n" + resultData.result[(int)result+2];
                    StartCoroutine(ResultData(result_Fail_Image.gameObject));
                }
                else if(windowCount != 2 && sendbagCount != 9)
                {
                    result_Fail_UI.SetActive(true);
                    result = global::Result.Level1_Window;
                    result_Fail_Image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "失敗!";
                    result_Fail_Image.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = resultData.result[(int)result] + "\n" + resultData.result[(int)result + 1];
                    StartCoroutine(ResultData(result_Fail_Image.gameObject));
                }
                else if(windowCount != 2 && itemCount != 7)
                {
                    result_Fail_UI.SetActive(true);
                    result = global::Result.Level1_Window;
                    result_Fail_Image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "失敗!";
                    result_Fail_Image.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = resultData.result[(int)result] + "\n" + resultData.result[(int)result + 2];
                    StartCoroutine(ResultData(result_Fail_Image.gameObject));
                }
                else if(sendbagCount != 9 && itemCount != 7)
                {
                    result_Fail_UI.SetActive(true);
                    result = global::Result.Level1_Sendbag;
                    result_Fail_Image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "失敗!";
                    result_Fail_Image.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = resultData.result[(int)result] + "\n" + resultData.result[(int)result + 1];
                    StartCoroutine(ResultData(result_Fail_Image.gameObject));
                }
                else if(windowCount != 2)
                {
                    result_Fail_UI.SetActive(true);
                    result = global::Result.Level1_Window;
                    result_Fail_Image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "失敗!";
                    result_Fail_Image.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = resultData.result[(int)result];
                    StartCoroutine(ResultData(result_Fail_Image.gameObject));
                }
                else if(sendbagCount != 9)
                {
                    result_Fail_UI.SetActive(true);
                    result = global::Result.Level1_Sendbag;
                    result_Fail_Image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "失敗!";
                    result_Fail_Image.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = resultData.result[(int)result];
                    StartCoroutine(ResultData(result_Fail_Image.gameObject));
                }
                else if(itemCount != 7)
                {
                    result_Fail_UI.SetActive(true);
                    result = global::Result.Level1_Item;
                    result_Fail_Image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "失敗!";
                    result_Fail_Image.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = resultData.result[(int)result];
                    StartCoroutine(ResultData(result_Fail_Image.gameObject));
                }*/
                break;
        }
    }

    IEnumerator Couter_WatchNews(int num)
    {
        yield return new WaitForSeconds(num);
        //news.SetActive(false);
        UpdateLevel1State(Level1State.NewsHint);
    }

    public void CloseNewHint()
    {
        UpdateLevel1State(Level1State.Buy);
    }

    public void CheckList()
    {
        foreach(Toggle toggle in toggles)
        {
            if(toggle.isOn)
            {
                items[index].SetActive(true);
                //Instantiate(items[index], spawnPoint.transform.position, items[index].transform.rotation);
            }
            index++;
        }
        UpdateLevel1State(Level1State.Layout);
    }

    public void UpdateMission()
    {
        text_window.text = "貼窗戶 " + windowCount + "/2";
        text_sendbag.text = "堆沙包 " + sendbagCount + "/9";
        text_item.text = "避難包 " + itemCount + "/7";
    }

    public void Result()
    {
        UpdateLevel1State(Level1State.Result);
    }

    IEnumerator ResultData(GameObject result)
    {
        yield return new WaitForSeconds(8);
        result.SetActive(true);
    }

}
