/*
 * - Name : Movement_Giant.cs
 * - Writer : 이윤교
 * - Content : 잭과콩나무 에피소드13 - 잭 원하는 위치로 드래그하는 스크립트
 * 
 * - HISTORY
 * 2021-07-14 : 초기 개발
 * 2021-07-16 : 파일 인코딩 수정 및 주석 처리
 * 2021-07-27 : 주석 처리 수정
 *
 * <Function>
 * OnTriggerEnter2D(Collider2D cCollideObject) : 오브젝트간 충돌이 일어날때 처음 한번만 호출되는 함수
 * OnMouseDrag() : 게임오브젝트를 드래그로 이동시키는 함수
 *           
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//잭을 원하는 위치로 드래그하는 함수
public class DragJack : MonoBehaviour{
    //오브젝트간 충돌이 일어날때 처음 한번만 호출되는 함수
    void OnTriggerEnter2D(Collider2D cCollideObject){
        if(cCollideObject.tag == "Jack"){                // 충돌 오브젝트의 태그가 잭이면 
            OnMouseDrag();                               // Jack을 드래그가능하게 함
        }
        else if(cCollideObject.tag == "Door"){          //충돌 오브젝트의 태그가 문이면
            SceneManager.LoadScene("Jack_Epi14");       //다음 씬 Epi14으로 이동
        }
    }

    //게임오브젝트를 드래그로 이동시키는 함수
    void OnMouseDrag(){
        Vector2 v2mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 v2worldObjPos = Camera.main.ScreenToWorldPoint(v2mousePosition);
        this.transform.position = v2worldObjPos;
    }
}