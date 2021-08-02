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
  * 2021-07-29 : 오브젝트 드래그가 아닌 UI 드래그에 맞춰 인터페이스 적용
  *
  * <Variable>
  * auSource : 마우스를 클릭했을 때 나는 효과음
  * uiCamera : 메인 카메라 오브젝트
  * mb_ClassifyPuzzle : 정답 판에 있는 퍼즐은 드래그가 되지 않도록 하기 위한 bool 변수
  *
  * <Function>
  *
  * 2021-07-29 : OnDrag() : UI 오브젝트에 맞는 이벤트 함수로, 현재 우리 UI캔버스는 메인 카메라에 맞춰 있기 때문에, Input.mousePosition 위치를 사용하지 못하고, 메인 카메라 오브젝트의 ScreenToWorldPoint() 함수에 한번 거쳐서 사용하여야 한다.
  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
  AudioSource auSource;
  Camera uiCamera;
  bool mb_ClassifyPuzzle;
  SoundManager soundManager;
  //초기설정
  void Start() {
    mb_ClassifyPuzzle = gameObject.GetComponent<Puzzle_Matching_Puzzle>().mb_classifyWhetherAns;
    auSource = GetComponent<AudioSource>();
    soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    uiCamera = Camera.main;
  } 
 
  public void OnDrag(PointerEventData eventData) {
    if (!mb_ClassifyPuzzle) {
      var screenPoint = new Vector3(Input.mousePosition.x,Input.mousePosition.y,100.0f); // z값을 Plane Distance 값을 줘야 합니다!! 
      transform.position = uiCamera.ScreenToWorldPoint(screenPoint); // 그리고 좌표 변환을 하면 끝!

    }
  }
  public void OnBeginDrag(PointerEventData eventData) {
      soundManager.playSound(0);
  }
  public void OnEndDrag(PointerEventData eventData) {
      soundManager.playSound(1);
  }
}