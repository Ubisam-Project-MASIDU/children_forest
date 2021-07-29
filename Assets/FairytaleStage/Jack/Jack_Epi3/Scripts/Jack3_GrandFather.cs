/*
 * - Name : Jack3_GFScript.cs
 * - Writer : 김명현
 * 
 * - Content :
 * 잭과콩나무 에피소드3 - 할아버지 오브젝트 스크립트
 * 할아버지와 소의 객체 충돌처리를 위한 스크립트
 * 
 *  * - Update Log -
 * 2021-07-13 : 제작 완료
 * 2021-07-23 : 주석 변경
 *          
 * - Variable
 * 
 * - Function
 * OnTriggerEnter2D(Collider2D cCollidObj)  충돌감지 함수
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack3_GrandFather : MonoBehaviour
{
    /// <summary>
    /// 오브젝트 충돌시 작동 함수
    /// </summary>
    /// <param name="cCollidObj">충돌한 객체</param>
    void OnTriggerEnter2D(Collider2D cCollidObj)
    {
        //Debug.Log("충돌 감지");
        if (cCollidObj.tag == "Jack3_Cow")                                                              // 할아버지객체와 소객체가 충돌한 경우
        {
            Destroy(cCollidObj.gameObject);                                                             // 충돌한 객체 삭제
        }
    }
}
