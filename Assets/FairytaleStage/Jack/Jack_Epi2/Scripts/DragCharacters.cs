/*
 * - Name : DragCharacters.cs
 * - Writer : 류시온
 * - Content : 잭과콩나무 에피소드2 - 잭,소 드래그 스크립트
 * - History -
 * 2021-07-20 : 작성
 * 2021-07-27 : 피드백에 의한 주석 변경.
 *
 * DragCharacters Member Variables
 * 
 * null
 * 
 * DragCharacters Member Functions
 *
 * OnTriggerEnter2D() :오브젝트간 충돌이 일어날때 처음 한번만 호출되는 함수
 * OnMouseDrag() : 게임오브젝트를 마우스 드래그로 이동시키는 함수
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 씬에 등장하는 캐릭터를 플레이어가 클릭하여 드래그하는 것을 감지하는 클래스이다.
public class DragCharacters : MonoBehaviour
{
    // 드래그할시에 캐릭터를 마우스 위치로 옮기도록 하였다.
    private void OnMouseDrag()
    {
        Vector2 v2_checkMousePos = new Vector2(Input.mousePosition.x,
        Input.mousePosition.y);
        Vector2 v2_checkworldObjPos = Camera.main.ScreenToWorldPoint(v2_checkMousePos);
        this.transform.position = v2_checkworldObjPos;
    }
}
