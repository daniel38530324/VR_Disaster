using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    Color originColor, newColor;
    float timer = 0;
    float duration = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        originColor = new Color(1, 1, 1, 0);
        newColor = new Color(1, 1, 1, 1);
        GetComponent<Image>().color = originColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < duration)
        {
            float t = timer / duration;
            GetComponent<Image>().color = Color.Lerp(originColor, newColor, t);
            timer += Time.deltaTime;
        }
        else
        {
            GetComponent<Image>().color = newColor;
        }
    }
}
