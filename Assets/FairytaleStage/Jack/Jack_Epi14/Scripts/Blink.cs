/*
  * - Name : Blink.cs
  * - Writer : 이윤교
  * - Content : 잭과콩나무 에피소드14 - 화살표 깜빡이는 효과 스크립트
  * 
  * - HISTORY
  * 2021-07-15 : 초기 개발
  * 2021-07-16 : 파일 인코딩 수정 및 주석 처리
  * 2021-07-27 : 주석 처리 수정
  *
  * <Variable>
  * mf_time : 깜빡거리는 속도
  * mf_timer : 경과시간
  * mf_waitingTime : 원하는 특정 시간 지정
  *
  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 미션 화살표 깜빡이게 하는 애니메이션 클래스
public class Blink : MonoBehaviour{
    float mf_time;
    float mf_timer;
    float mf_waitingTime;
    
    //초기설정
    void Start(){
        mf_timer = 0.0f;
        mf_waitingTime = 5.0f; //5초 뒤
    }

    /*깜빡깜빡 거리는 효과*/
    public void Update(){
        mf_timer += Time.deltaTime;
        if(mf_timer > mf_waitingTime){
            if (mf_time < 0.3f){
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); //투명도 1
            }
            else{
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0); //투명도 0
                if (mf_time > 1f){
                    mf_time = 0;
                }
            }
            mf_time += Time.deltaTime;
        }
    }
}
