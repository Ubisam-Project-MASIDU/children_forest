/*
  * - Name : End_controlStage.cs
  * - Writer : 이윤교
  * - Content : select stage 씬과 연결해 주는 스크립트
  * 
  * - HISTORY
  * 2021-07-22 : 초기 개발 및 주석처리
  * 2021-07-27 : 주석처리 수정
  *            
  * <Function>
  * v_changeSelectStage() : select_stage 씬 로드
  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//select stage 씬으로 이동하는 클래스
public class End_controlStage : MonoBehaviour{
    void Start() {
        Invoke("v_changeSelectStage", 4f);            //4초 후 v_changeSelectStage 함수 실행
    }
    void v_changeSelectStage() {
        SceneManager.LoadScene("select_stage_scene"); //select_stage_scene 로드
    }
}
