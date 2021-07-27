/*
  * - Name : ScriptControl.cs
  * - Writer : 이윤교
  * - Content : 잭과콩나무 에피소드14 - ScriptControl
  * 
  * - HISTORY
  * 2021-07-14 : 초기 개발
  * 2021-07-16 : 파일 인코딩 수정 및 주석 처리
  * 2021-07-27 : 주석 처리 수정
  *
  * <Variable>
  * mg_targetPosition : inspector창에서 walkPos 오브젝트로 지정해서 walkPos 위치로 거인 이동시킴
  * mb_checkPlayOnce : 스크립트 음성이 한번만 실행되게 하기 위한 체크하는 변수
  * sc : 동화 스크립트 나타내기 위한 오브젝트
  * vm : 음성 TTS를 처리하는 오브젝트 연결
  *
  * <Variable>
  * instance : ScriptControl
  * mg_setGameObject : Jack_Script 이름의 게임오브젝트를 찾아 저장
  * mt_setText : mg_setGameObject오브젝트의 Text 컴포넌트를 담음.
  * mn_checkCurrentScr : mt_setText에 나타낼 스크립트의 인덱스
  * ms_setScriptText : 스크립트 갯수 만큼 스크립트 내용을 담는 리스트 
  * mn_setScriptNum : 스크립트 초기 갯수 5개
  *
  * <Function>
  * GetInstance() : instance 존재여부 확인 후 없으면 생성
  * setNextScript() : 스크립트의 다음 문장을 보여줌.
  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Jack_Script 창에 스크립트를 보여줄 예정
public class ScriptControl : MonoBehaviour{
    /*싱글톤 구현*/
    private static ScriptControl instance;     //여러 곳에서 동일한 인스턴스에 대해 공유할 수 있도록 static으로 선언
    private GameObject mg_setGameObject;
    private Text mt_setText;
    private int mn_checkCurrentScr = 0;
    public string[] ms_setScriptText = new string[mn_setScriptNum];
    public const int mn_setScriptNum = 5;
    
    //초기설정
    void Start(){
        mg_setGameObject = GameObject.Find("Jack_Script"); 
        mt_setText = mg_setGameObject.GetComponent<Text>(); 
        mt_setText.text = ms_setScriptText[mn_checkCurrentScr];
    }

    //instance 존재여부 확인 후 없으면 생성하는 함수
    public static ScriptControl GetInstance(){
        if (!instance){                                                                     // 만약 instance가 존재하지 않을 경우
            instance = GameObject.FindObjectOfType(typeof(ScriptControl)) as ScriptControl; // ScriptControl 클래스의 instance를 찾는다
            if (!instance){                                                                 // 찾아봐도 존재하지 않을 경우 새로 만든다 
                GameObject obj = new GameObject("ScriptControl");                           // 이름인 ScriptControl인 오브젝트 새로 생성
                instance = obj.AddComponent(typeof(ScriptControl)) as ScriptControl;        // obj에 ScriptControl 타입의 컴포넌트를 추가
            }
        }
        return instance;                                                                    // instance를 반환한다.
    }

    //스크립트의 다음 문장을 보여주는 함수
    public void setNextScript(){ 
        mn_checkCurrentScr++;                                                               //인덱스를 다음 스크립트로 이동
        mt_setText.text = ms_setScriptText[mn_checkCurrentScr];                             //다음 스크립트를 보여줌
    }
}