using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Result
{
    Level1Pass,
    Level2Pass,
    Level3Pass,
    Level1_Window,
    Level1_Sendbag,
    Level1_Item
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void DestoryObject(GameObject gameObject , int num)
    {
        Destroy(gameObject, num);
    }

    public void DisableObject(GameObject gameObject, int num)
    {
        DisableObject_Delay(gameObject, num);
    }

    IEnumerator DisableObject_Delay(GameObject gameObject, int num)
    {
        yield return new WaitForSeconds(num);
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
