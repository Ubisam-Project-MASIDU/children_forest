/*
  * - Name : Movement_Giant.cs
  * - Writer : 이윤교
  * - Content : 잭과콩나무 에피소드8 - 잭 뒤를 쫓도록 거인이 움직이는 클래스/
  * 
  * - HISTORY
  * 2021-07-14 : 초기 개발
  * 2021-07-16 : 파일 인코딩 수정 및 주석 처리
  * 2021-07-22 : TTS 기능 추가
  * 2021-07-27 : 주석 처리 수정
  *
  * <Variable>
  * mg_targetPosition : inspector창에서 walkPos 오브젝트로 지정해서 walkPos 위치로 거인 이동시킴
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

//잭 뒤를 쫓도록 거인이 움직이는 클래스
public class Movement_Giant : MonoBehaviour{
  public GameObject mg_targetPosition;
  bool mb_checkPlayOnce = false;
  public ScriptControl sc;
  VoiceManager vm;
    GameObject mg_Jack;
    bool Flag = false;

  //초기설정
  void Start(){
    sc = ScriptControl.GetInstance();
    this.vm = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
        mg_Jack = GameObject.Find("Jack");
    }
  
  void Update(){
    if(vm.mb_checkSceneReady){                                                                                             //tts 준비 작업이 다 끝났다면 
      transform.position = Vector3.MoveTowards(gameObject.transform.position, mg_targetPosition.transform.position, 0.1f); //거인 이동 
      if(!mb_checkPlayOnce){                                                                                               //스크립트 음성이 한번도 나온적이 없다면
        vm.playVoice(0);                                                                                                   //스크립트 음성 재생
        mb_checkPlayOnce = true;                                                                                           //스크립트 음성 재생 체크
      }
      else if (!vm.isPlaying() && Flag == false)
            {
                mg_Jack.GetComponent<Drag_Jack>().ChangeDragFlagTrue();
                Flag = true;
            }
    }
  }
}