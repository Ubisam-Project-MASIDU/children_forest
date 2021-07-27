using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private AudioSource audioSource;
    //public bool fadeIn;
    // fade in 시간 설정 1s
    public double fadeInSeconds = 1.0;
    private bool isFadeIn = true;
    private double fadeDeltaTime = 0;

    private float md_checkVolumn;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        md_checkVolumn = audioSource.volume;
        Debug.Log(md_checkVolumn.ToString());
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(isFadeIn)
        {
            fadeDeltaTime += Time.deltaTime;
            if(fadeDeltaTime >= fadeInSeconds)
            {
                fadeDeltaTime = fadeInSeconds;
                isFadeIn = false;
            }
            audioSource.volume = md_checkVolumn * (float)(fadeDeltaTime / fadeInSeconds);
        }
        
    }
}
