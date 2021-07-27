/*
 * - Name : Movement_Jack.cs
 * - Writer : 이윤교
 * - Content : 잭과콩나무 에피소드13 - 잭 이동 스크립트
 * 
 * - HISTORY
 * 2021-07-15 : 초기 개발
 * 2021-07-16 : 파일 인코딩 수정 및 주석 처리
 * 2021-07-22 : TTS 기능 추가
 * 2021-07-26 : 효과음 추가 완료
 * 2021-07-27 : 주석 처리 수정
 *
 * <Variable>
 * v3_target : 작이 걸어가야하는 위치 지정
 * mb_checkPlayOnce : 스크립트 음성이 한번만 실행되게 하기 위한 체크하는 변수
 * mb_checkPlayVoice : 첫번째 스크립트 재생이 되었는지 체크하는 변수
 * sc : 동화 스크립트 나타내기 위한 오브젝트
 * vm : 음성 TTS를 처리하는 오브젝트 연결 
 * ScreamSound : 잭 비명소리
 *
 * <Function>
 * PlayScream() : 잭 비명소리 재생하는 함수
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//잭 이동 클래스
public class MovementJack : MonoBehaviour{
    public Vector3 v3_target;
    bool mb_checkPlayOnce = false;
    bool mb_checkPlayVoice = false;
    public ScriptControl sc;
    VoiceManager vm;
    private AudioSource ScreamSound;

    //초기설정
    void Start(){
        sc = ScriptControl.GetInstance();
        this.vm = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
        ScreamSound = GameObject.Find("ScreamSound").GetComponent<AudioSource>();
    Invoke("PlayScream",1f);                                                                //1초 후 PlayScream 함수 재생
    }
    void Update(){
        if(vm.mb_checkSceneReady) {                                                         //tts 준비 작업이 다 끝났다면 
            transform.position = Vector3.MoveTowards(transform.position, v3_target, 0.2f);  // 잭 이동
            if(!mb_checkPlayOnce) {                                                         //스크립트 음성이 한번도 나온적이 없다면
                vm.playVoice(0);                                                            //스크립트 음성 재생   
                mb_checkPlayOnce = true;                                                    //스크립트 음성 재생 체크
            }
            if(!vm.isPlaying() && !mb_checkPlayVoice){                                      //현재 스크립트 음성이 끝나고 다음 스크립트 음성이 재생된 적이 없다면
                sc.setNextScript();                                                         //다음 스크립트 보여줌
                vm.playVoice(1);                                                            //다음 스크립트 음성 재생   
                mb_checkPlayVoice = true;                                                   //스크립트 음성 재생 체크
            }
        }
    }

    // 잭 비명소리 재생하는 함수
    void PlayScream(){
        ScreamSound.Play();
    }
}
