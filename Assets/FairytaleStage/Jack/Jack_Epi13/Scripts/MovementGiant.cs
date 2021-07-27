/*
 * - Name : Movement_Giant.cs
 * - Writer : 이윤교
 * - Content : 잭과콩나무 에피소드13 - 거인 이동 스크립트
 * 
 * - HISTORY
 * 2021-07-15 : 초기 개발
 * 2021-07-16 : 파일 인코딩 수정 및 주석 처리
 * 2021-07-22 : TTS 기능 추가
 * 2021-07-27 : 주석 처리 수정
 *         
 * <Variable>
 * v3_target : 거인이 걸어가야하는 위치 지정
 * mb_checkPlayOnce : 스크립트 음성이 한번만 실행되게 하기 위한 체크하는 변수
 * sc : 동화 스크립트 나타내기 위한 오브젝트
 * vm : 음성 TTS를 처리하는 오브젝트 연결   
 *
 * <Function>
 * MoveTowards() : 등속 이동, 매개변수로 {현재위치, 목표위치, 속도}를 입력           
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//거인 이동 클래스
public class MovementGiant : MonoBehaviour{
    public Vector3 v3_target; 
    bool mb_checkPlayOnce = false;
    public ScriptControl sc;
    VoiceManager vm;

    //초기설정
    void Start(){
        sc = ScriptControl.GetInstance();
        this.vm = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
    }
    void Update(){  
         if(vm.mb_checkSceneReady){                                                       //tts 준비 작업이 다 끝났다면 
            transform.position = Vector3.MoveTowards(transform.position, v3_target, 0.1f); //거인 이동
            if(!mb_checkPlayOnce){                                                        //스크립트 음성이 한번도 나온적이 없다면
                vm.playVoice(0);                                                           //스크립트 음성 재생
                mb_checkPlayOnce = true;                                                   //스크립트 음성 재생 체크
            }
    }

        
    }
}
