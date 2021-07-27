/*
 * - Name : FadeIn.cs
 * - Writer : 최대준
 * - Content : FadeIn은 BGM이 자연스럽게 들리고 전환되도록 시작될 때 천천히 정해진 소리 볼륨만큼 볼륨을 올리도록 조정하는 역할을 한다.
 * - History -
 * 2021-07-19 : 구현
 * 2021-07-27 : 주석 처리.
 *
 * - FadeIn Member Variable 
 *
 * mas_playBGM : BGM을 출력할 소리원.
 * md_fadeInSeconds : 서서히 몇초에 걸쳐 정해진 볼륨만큼 fade in 할지 정하는 변수로 인스펙터 창에서 설정할 수 있다.
 * mb_isFadeIn : fade in 중인지 확인해주는 flag성 변수.
 * md_fadeDeltaTime : fade in 한지 얼마나 시간이 흘렀는지를 담는 변수.
 * md_checkVolumn : 소리원에 설정된 볼륨의 값을 가져와 저장하는 변수.
 * 
 * - FadeIn Member Function
 *
 * Start() : 소리원 컴포넌트, 볼륨 값들을 초기화한다.
 * Update() : 지정한 시간초까지 볼륨을 서서히 높이는 함수.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 소리의 볼륨을 서서히 높이는 행위를 정의한 클래스이다.
public class FadeIn : MonoBehaviour {
    private AudioSource mas_playBGM;
    public double md_fadeInSeconds = 1.0;
    private bool mb_isFadeIn = true;
    private double md_fadeDeltaTime = 0;
    private float md_checkVolumn;

// 소리원, 볼륨 값을 가져와 저장한다.
    void Start() {
        mas_playBGM = GetComponent<AudioSource>();
        md_checkVolumn = mas_playBGM.volume;
        Debug.Log(md_checkVolumn.ToString());
    }

// 볼륨을 서서히 높인다.
    void Update() {
        if(mb_isFadeIn) {
            md_fadeDeltaTime += Time.deltaTime;
            if(md_fadeDeltaTime >= md_fadeInSeconds) {
                md_fadeDeltaTime = md_fadeInSeconds;
                mb_isFadeIn = false;
            }
            mas_playBGM.volume = md_checkVolumn * (float)(md_fadeDeltaTime / md_fadeInSeconds);
        }
    }
}
