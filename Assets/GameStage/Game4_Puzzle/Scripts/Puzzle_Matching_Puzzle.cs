/*
  * - Name : Puzzle_Matching_Puzzle.cs
  * - Writer : 이윤교
  * - Content : 퍼즐을 맞추는 스크립트
  * 
  * - HISTORY
  * 2021-07-06 : 초기 개발
  * 2021-07-19 : 코드 수정
  * 2021-07-21 : 주석 처리
  * 2021-07-22 : 주석 처리 수정
  * 2021-07-29 : null exception 처리, 로직 수정
  *
  * <Variable>
  * mb_classifyWhetherAns : 퍼즐 매칭 되기 전 상태 저장 변수
  * sNextSprite : 퍼즐 매칭 된 후 대체될 스프라이트
  * mv2_initPos : 퍼즐 매칭 실패 후 되돌아 갈 위치 저장 변수
  * mgo_CheckPuzzle : CheckPuzzle 오브젝트에 저장된 스크립트 클래스에 변수값을 수정하기 위해서 필요한 변수
  *
  * <Function>
  * Input.GetMouseButtonUp() : 유저가 주어진 마우스 버튼에서 손을 뗏을 때 true를 반환. 버튼이 0이면 좌클릭, 1이면 우클릭, 2이면 중앙을 클릭한 것.
  * OnTriggerEnter2D(Collider2D cCollideObject) : 유니티의 collider 컴포넌트를 주었을 때 호출되는 함수로, 이름과 같이 collider들이 부딪혔을 때 호출되어 함수안에 어떤 작업을 할지를 적어주는 함수
  * OnBeginDrag() : 해당 오브젝트를 드래그하기 시작햇을 때 한번만 호출되는 함수. 첫 위치를 저장한다.
  * OnEndDrag() : 해당 오브젝트의 드래그를 끝낼 때 호출되는 함수. 원래 자리로 돌아간다.
  *
  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Puzzle_Matching_Puzzle : MonoBehaviour, IBeginDragHandler, IEndDragHandler {
    public bool mb_classifyWhetherAns = false;
    public Sprite sNextSprite;
    public int mn_PuzzleId;
    public Vector2 mv2_initPos;
    Puzzle_CheckPuzzle mgo_CheckPuzzle;

    private void Start() {
        mgo_CheckPuzzle = GameObject.Find("CheckPuzzle").GetComponent<Puzzle_CheckPuzzle>();

    }

    void OnTriggerStay2D(Collider2D cCollideObject){
        if(Input.GetMouseButtonUp(0)){
            if (cCollideObject.GetComponent<Puzzle_Matching_Puzzle>() != null) {
                if (cCollideObject.GetComponent<Puzzle_Matching_Puzzle>().mn_PuzzleId == this.mn_PuzzleId){        //정답이면
                    if (mb_classifyWhetherAns) {                                                                     //answer부분변경
                        Color tempColor = gameObject.GetComponent<Image>().color;                          //흐렷던 퍼즐조각을 선명하게 변경
                        tempColor.a = 1f;
                        gameObject.GetComponent<Image>().color = tempColor;
                        GameObject.Find("CheckPuzzle").GetComponent<Puzzle_CheckPuzzle>().setAnswerPuzzle();  
                        gameObject.GetComponent<BoxCollider2D>().enabled = false;   
                    }
                    else {                     
                        Color tempColor = gameObject.GetComponent<Image>().color;                          //흐렷던 퍼즐조각을 선명하게 변경
                        tempColor.a = 0f;
                        gameObject.GetComponent<Image>().color = tempColor;
                        gameObject.GetComponent<Drag>().enabled = false;   
                        gameObject.GetComponent<BoxCollider2D>().enabled = false;   
                    }
                }
            }                                                                          //손을 뗐을 때

        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        mv2_initPos = transform.position;          
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.position = mv2_initPos;  
    }
}

