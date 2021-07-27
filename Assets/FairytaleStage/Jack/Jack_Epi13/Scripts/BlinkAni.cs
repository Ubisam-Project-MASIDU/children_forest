/*
 * - Name : Movement_Giant.cs
 * - Writer : 이윤교
 * - Content : 잭과콩나무 에피소드13 - 화살표 깜빡이는 효과 스크립트
 * 
 * - HISTORY
 * 2021-07-14 : 초기 개발
 * 2021-07-27 : 주석 처리 수정
 *
 * <Variable>
 * f_blink : 깜빡거리는 속도          
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 미션 화살표 깜빡이게 하는 애니메이션 클래스
public class BlinkAni : MonoBehaviour{
    float f_blink; 

    /*깜빡깜빡 거리는 효과*/
    public void Update(){
        if (f_blink < 0.5f){
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); //투명도 1
        }
        else{
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0); //투명도 0
            if (f_blink > 1f){
                f_blink = 0;
            }
        }
        f_blink += Time.deltaTime;
    }
}
