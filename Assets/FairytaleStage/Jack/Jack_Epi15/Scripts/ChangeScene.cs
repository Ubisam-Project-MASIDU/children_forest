/*
 * - Name : ChangeScene.cs
 * - Writer : 이윤교
 * - Content : 잭과콩나무 에피소드15 - 잭과콩나무 동화 다시 듣기 버튼과 게임선택 씬으로 이동하는 버튼 스크립트
 * 
 * - HISTORY
 * 2021-07-20 : 초기 개발
 * 2021-07-27 : 주석 처리 수정
 *
 * <Function>
 * ReStart() : Jack_Epi1 씬 로드 함수 (동화 다시 재생 버튼)
 * Select() : select_stage 씬 로드 함수 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//잭과콩나무 동화 다시 듣기 버튼과 게임선택 씬으로 이동하는 버튼 스크립트
public class ChangeScene : MonoBehaviour{
    //Jack_Epi1 씬 로드 함수 (동화 다시 재생 버튼)
    public void ReStart(){
        SceneManager.LoadScene("Jack_Epi1");
    }

    //select_stage 씬 로드 함수 
    public void Select(){
        SceneManager.LoadScene("select_stage_scene");
        var obj = GameObject.Find("BGMmanager"); 
        if(obj != null) { //BGM 끄기
            Destroy(obj);
        }
    }
}
