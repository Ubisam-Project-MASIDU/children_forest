/*
 * - Name : MoveMom.cs
 * - Writer : 이윤교  * - Content : 잭과콩나무 에피소드14 - 엄마 이동 스크립트
 * 
 * - HISTORY
 * 2021-07-15 : 초기 개발
 * 2021-07-16 : 파일 인코딩 수정 및 주석 처리
 * 2021-07-22 : TTS 기능 추가
 * 2021-07-27 : 주석 처리 수정
 *
 * <Variable>
 * mg_Jack : 잭 
 * mg_Ax : 도끼
 * mg_Mom : 어머니
 * v3_MomPos : 어머니 위치 저장
 * mb_checkGetAxe : 잭의 도끼 습득 유무
 * sc : 동화 스크립트 나타내기 위한 오브젝트
 * vm : 음성 TTS를 처리하는 오브젝트 연결
 * mf_timer : 경과 시간
 * mf_waitingTime : 특정 시간
 * mb_checkPlayOnce : 스크립트 음성이 한번만 실행되게 하기 위한 체크하는 변수
 *
 * <Fucntion> 
 * MoveTowards() : 등속 이동, 매개변수로 {현재위치, 목표위치, 속도}를 입력  
 * OnTriggerEnter2D(Collider2D cCollideObject) :오브젝트간 충돌이 일어날때 처음 한번만 호출되는 함수
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//어머니가 등장하는 클래스
public class MoveMom : MonoBehaviour{
    GameObject mg_Jack, mg_Ax, mg_Mom;
    Vector3 v3_MomPos;
    float mf_timer;
    float mf_waitingTime;
    bool mb_checkPlayOnce = false;
    VoiceManager vm;
    void Start() {
        mg_Ax = GameObject.Find("Ax"); 
        mg_Jack = GameObject.Find("Jack");
        mg_Mom = GameObject.Find("Mom");
        v3_MomPos = new Vector3(-3,-1.14f,0);
        mf_timer = 0.0f;
        mf_waitingTime = 2f; //2초뒤
        this.vm = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
    }

    // 2초뒤 엄마 등장
    void Update(){
        if(vm.mb_checkSceneReady) {                                                                                         //tts 준비 작업이 다 끝났다면 
            mf_timer += Time.deltaTime;
            if (mf_timer > mf_waitingTime){
                mg_Mom.transform.position = Vector3.MoveTowards(mg_Mom.transform.position, v3_MomPos, 2f * Time.deltaTime); //어머니 이동
            }
            if(!mb_checkPlayOnce) {
                vm.playVoice(0);                                                                                            //스크립트 음성 재생 
                mb_checkPlayOnce = true;                                                                                    //스크립트 음성 재생 체크
            }
        }
    }

    //엄마가 등장하면 미션 유도 클릭버튼 생성
    void OnTriggerEnter2D(Collider2D cCollideObject){
        if (cCollideObject.tag == "Mom"){                                                                                   //엄마가 등장
            Color tempColor = mg_Ax.GetComponent<SpriteRenderer>().color;                                                   //도끼 투명도 1로 변경하면서 등장
            tempColor.a = 1f;
            mg_Ax.GetComponent<SpriteRenderer>().color = tempColor;
        }
    }
}
