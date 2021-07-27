/*
  * - Name : DragAx.cs
  * - Writer : 이윤교
  * - Content : 잭과콩나무 에피소드14 - 엄마가 잭에게 도끼를 주는 스크립트
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
  * mg_Click : 미션유도 클릭
  * mb_checkGetAxe : 잭의 도끼 습득 유무
  * sc : 동화 스크립트 나타내기 위한 오브젝트
  * vm : 음성 TTS를 처리하는 오브젝트 연결
  *
  * <Function>
  * OnTriggerEnter2D(Collider2D cCollideObject) : 오브젝트간 충돌이 일어날때 처음 한번만 호출되는 함수
  * OnMouseDrag() : 게임오브젝트를 드래그로 이동시키는 함수
  * gotoEpi9Scene() : Epi9로 씬 이동을 위한 함수
  *            
  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//엄마가 잭에게 도끼를 주는 클래스
public class DragAx : MonoBehaviour{   
    public GameObject mg_Jack; 
    public GameObject mg_Ax; 
    public GameObject mg_Click; 
    private bool mb_checkGetAxe = false;
    public ScriptControl sc;
    VoiceManager vm;

    //초기설정
    void Start(){
        mg_Click = GameObject.Find("Click");                                                //Click 게임 오브젝트를 찾아서 mg_Click 변수에 저장
        sc = ScriptControl.GetInstance();
        this.vm = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
    }
    public void OnMouseDrag(){
        Vector2 v2mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 v2worldObjPos = Camera.main.ScreenToWorldPoint(v2mousePosition);
        Destroy(mg_Click);                                                                  //도끼를 드래그 하면 미션유도클릭 없애기
        this.transform.position = v2worldObjPos;
    }
    void OnTriggerEnter2D(Collider2D cCollideObject){
        if (cCollideObject.tag == "Jack" && !mb_checkGetAxe){                               //잭이 도끼를 가지게 되면
            SpriteRenderer rend =  mg_Jack.GetComponent<SpriteRenderer>();
            rend.flipX = false;                                                             //잭이 콩나무 바라보기 -> 좌우대칭
            transform.position = mg_Jack.transform.position;                                //도끼위치를 잭위치로 배치
            vm.playVoice(1);                                                                //1번째 스크립트 음성 재생   
            sc.setNextScript();                                                             //1번째 스크립트 보여줌
            mb_checkGetAxe = true;                                                          //잭이 도끼를 습득 체크
        }
    }
}
