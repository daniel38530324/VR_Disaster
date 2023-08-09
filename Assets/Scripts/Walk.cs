using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    float timer = 0;

    private void OnEnable()
    {
        GetComponent<AudioSource>().PlayOneShot(audioClips[0]);
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        for(int i  = 0; i < audioClips.Length; i++)
        {
            if (timer >= audioClips[i].length)
            {
                timer = 0;
                if (i < audioClips.Length - 1)
                {
                    GetComponent<AudioSource>().PlayOneShot(audioClips[i + 1]);
                }
                else
                {
                    GetComponent<AudioSource>().PlayOneShot(audioClips[0]);
                }
                
            }
        }
    }
}
