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
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler {
  AudioSource auSource;
  Camera uiCamera;
  bool mb_ClassifyPuzzle;
  //초기설정
  void Start() {
    mb_ClassifyPuzzle = gameObject.GetComponent<Puzzle_Matching_Puzzle>().mb_classifyWhetherAns;
    auSource = GetComponent<AudioSource>();
    uiCamera = Camera.main;
  } 
  public void OnDrag(PointerEventData eventData) {
    if (!mb_ClassifyPuzzle) {
      var screenPoint = new Vector3(Input.mousePosition.x,Input.mousePosition.y,100.0f); // z값을 Plane Distance 값을 줘야 합니다!! 
      transform.position = uiCamera.ScreenToWorldPoint(screenPoint); // 그리고 좌표 변환을 하면 끝!
    }

  }
}