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
  *
  * <Variable>
  * mb_classifyWhetherAns : 퍼즐 매칭 되기 전 상태 저장 변수
  * sNextSprite : 퍼즐 매칭 된 후 대체될 스프라이트
  * mv2_initPos : 퍼즐 매칭 실패 후 되돌아 갈 위치 저장 변수
  * 
  * <Function>
  * Input.GetMouseButtonUp() : 유저가 주어진 마우스 버튼에서 손을 뗏을 때 true를 반환. 버튼이 0이면 좌클릭, 1이면 우클릭, 2이면 중앙을 클릭한 것.
  * OnTriggerEnter2D(Collider2D cCollideObject) : 유니티의 collider 컴포넌트를 주었을 때 호출되는 함수로, 이름과 같이 collider들이 부딪혔을 때 호출되어 함수안에 어떤 작업을 할지를 적어주는 함수
  */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Matching_Puzzle : MonoBehaviour{
    public bool mb_classifyWhetherAns = false;
    public Sprite sNextSprite;
    Vector2 mv2_initPos;
    private void Start(){
        if(!mb_classifyWhetherAns){                                                                             //퍼즐 매칭 되기 전 상태이면
            transform.position = new Vector3(Random.Range(19, 26), Random.Range(3, 11), 0);                     //자리 랜덤 선정
            mv2_initPos = transform.position;                                                                   //초기 자리 저장 (퍼즐을 맞추지 못할 경우 제자리로 돌아가기 위함)
        }
    }
    void Update(){
        if(!mb_classifyWhetherAns){                                                                              // 매칭 되지 못할 경우
            transform.position = Vector3.MoveTowards(this.transform.position, mv2_initPos, 10f * Time.deltaTime); //처음 지정된 랜덤자리로 다시 되돌아감.
        }
    }
    void OnTriggerStay2D(Collider2D cCollideObject){
        if(Input.GetMouseButtonUp(0)){                                                                          //손을 뗐을 때
            if (cCollideObject.name[cCollideObject.name.Length - 1] == this.name[this.name.Length - 1]){        //정답이면
                if (mb_classifyWhetherAns){                                                                     //answer부분변경
                    Color tempColor = gameObject.GetComponent<SpriteRenderer>().color;                          //흐렷던 퍼즐조각을 선명하게 변경
                    tempColor.a = 1f;
                    gameObject.GetComponent<SpriteRenderer>().color = tempColor;    
                }
                else{                                                                                           
                    Destroy(this.gameObject);
                }
            }
        }
    }
}

