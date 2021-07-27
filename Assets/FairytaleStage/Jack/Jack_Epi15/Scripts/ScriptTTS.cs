/*
 * - Name : ScriptTTS.cs
 * - Writer : 이윤교
 * - Content : 잭과콩나무 에피소드15 - TTS 적용 스크립트
 * 
 * - HISTORY
 * 2021-07-15 : 초기 개발
 * 2021-07-16 : 파일 인코딩 수정 및 주석 처리
 * 2021-07-27 : 주석 처리 수정
 *
 * <Variable>
 * vm : 음성 TTS를 처리하는 오브젝트 연결 
 * mb_checkPlayOnce : 스크립트 음성이 한번만 실행되게 하기 위한 체크하는 변수
 *
 * <Function>
 * PlayScream() : 잭 비명소리 재생하는 함수
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스크립트에 TTS 적용 클래스
public class ScriptTTS : MonoBehaviour{
    VoiceManager vm;
    bool mb_checkPlayOnce = false;

    // 초기설정
    void Start(){
        this.vm = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
    }

    void Update(){
        if(vm.mb_checkSceneReady){                 //tts 준비 작업이 다 끝났다면
            if(!mb_checkPlayOnce){                 //스크립트 음성이 한번도 나온적이 없다면
                vm.playVoice(0);                    //다음 스크립트 음성 재생   
                mb_checkPlayOnce = true;            //스크립트 음성 재생 체크
            }
        }
    }
}
