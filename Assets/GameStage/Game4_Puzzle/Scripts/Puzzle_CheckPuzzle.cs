/*
  * - Name : Puzzle_CheckPuzzle.cs
  * - Writer : 이윤교
  * - Content : 퍼즐을 다 맞췄는지 확인하고 end 씬 불러오는 스크립트
  * 
  * - HISTORY
  * 2021-07-06 : 초기 개발
  * 2021-07-16 : 파일 인코딩 수정 및 주석 처리
  * 2021-07-27 : 주석 처리 수정
  *
  * <Variable>
  * vm : 음성 TTS를 처리하는 오브젝트 연결 
  * mb_checkVoice : 스크립트 음성이 한번만 실행되게 하기 위한 체크하는 변수
  *
  * <Function>
  * v_EndStage() : end씬 불러오기
  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle_CheckPuzzle : MonoBehaviour {
    VoiceManager vm;
    bool mb_checkVoice = false;

    //초기설정
    void Start(){
        vm = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
    }
    void Update(){
        if(transform.childCount <= 9){          //퍼즐을 다 맞추면
            if(!mb_checkVoice){                 //스크립트 음성이 한번도 나온적이 없다면
                vm.playVoice(0);                //스크립트 음성 재생
                mb_checkVoice = true;           //스크립트 음성 재생 체크
            }
            Destroy(transform.Find("arrow"));   //arrow 오브젝트 없애기
            Invoke("v_EndStage", 2f);           //2초 후 v_Endstage함수 호출
        }
    }

    //end씬 불러오는 함수
    void v_EndStage() {
        SceneManager.LoadScene("end_scene");
    }
}
