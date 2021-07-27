/*
  * - Name : Drag.cs
  * - Writer : 이윤교
  * - Content : 드래그 스크립트
  * 
  * - HISTORY
  * 2021-07-06 : 초기 개발
  * 2021-07-19 : 코드 수정
  * 2021-07-21 : 주석 처리
  * 2021-07-22 : 주석 처리 수정
  *
  * <Variable>
  * auSource : 마우스를 클릭했을 때 나는 효과음
  *
  * <Function>
  * OnMouseDrag() : 게임오브젝트를 드래그로 이동시키는 함수
  *
  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour{
  AudioSource auSource;
  //초기설정
  void Start(){
    auSource = GetComponent<AudioSource>();
  }

  //게임오브젝트를 드래그로 이동시키는 함수
  void OnMouseDrag(){
    Vector2 v2mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    Vector2 v2worldObjPos = Camera.main.ScreenToWorldPoint(v2mousePosition);
    this.transform.position = v2worldObjPos;
  }
  
  //마우스를 클릭했을때 
  void OnMouseDown(){ 
    auSource.Play();
  }

}