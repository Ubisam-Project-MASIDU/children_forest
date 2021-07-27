/*
 * - Name : Drag_Jack.cs
 * - Writer : 이윤교
 * - Content : 잭과콩나무 에피소드8 - 거인을 피해 옷장 뒤로 잭을 숨기기 위한 잭 드래그하는 스크립트
 * 
 * - HISTORY
 * 2021-07-14 : 초기 개발
 * 2021-07-16 : 파일 인코딩 수정 및 주석 처리
 * 2021-07-22 : TTS 기능 추가
 * 2021-07-27 : 주석 처리 수정
 *
 * <Variable>
 * mg_Jack : 잭 오브젝트
 * sc : 동화 스크립트 나타내기 위한 오브젝트
 * vm : 음성 TTS를 처리하는 오브젝트 연결
 *
 * <Function>
 * OnTriggerEnter2D(Collider2D cCollideObject) : 오브젝트간 충돌이 일어날때 처음 한번만 호출되는 함수
 * OnMouseDrag() : 게임오브젝트를 드래그로 이동시키는 함수
 * gotoEpi9Scene() : Epi9로 씬 이동을 위한 함수
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//거인을 피해 옷장 뒤로 잭을 숨기기 위한 잭 드래그하는 클래스
public class Drag_Jack : MonoBehaviour{   
    public GameObject mg_Jack;
    public ScriptControl sc;
    VoiceManager vm;

    //초기설정
    void Start(){
        sc = ScriptControl.GetInstance();                                          // ScriptControl에서 Instance 리턴 받아 사용
        this.vm = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();

    }

    //오브젝트간 충돌이 일어날때 처음 한번만 호출되는 함수
    void OnTriggerEnter2D(Collider2D cCollideObject){                               //오브젝트간 충돌이 일어나면
        OnMouseDrag();                                                              //드래그 허용
        if(cCollideObject.tag == "Closet"){                                         //Jack이 옷장 뒤에 숨으면
            sc.setNextScript();                                                     //다음 스크립트 제시
            vm.playVoice(1);                                                        //다음 스크립트에 해당하는 tts 실행
        }
        if(!vm.isPlaying()) {                                                       //tts재생이 끝나면
            Invoke("gotoEpi9Scene", 5f);                                            //5초 후 gotoEpi9Scene 함수 수행 
        }
    }

    //게임오브젝트를 드래그로 이동시키는 함수
    void OnMouseDrag(){
                Vector2 v2mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 v2worldObjPos = Camera.main.ScreenToWorldPoint(v2mousePosition);
                mg_Jack.transform.position = v2worldObjPos;
    }

    //Epi9로 씬 이동을 위한 함수
    void gotoEpi9Scene() {
        SceneManager.LoadScene("Jack_Epi9");                                        //Jack_Epi9 씬 로드
    }
}
