/*
 * - Name : EndPoint.cs
 * - Writer : 김명현
 * - Content : 
 * 잭과콩나무 에피소드5 - 콩나무 끝지점 오브젝트 스크립트
 * 콩과 어머니객체 충돌처리를 위한 스크립트
 * 
 * - History -
 * 1. 2021-07-15 : 초안 작성
 * 
 * - Variable
 * mg_EventManager                          감독 오브젝트 연결을 위한 오브젝트
 * 
 * - Function
 * OnTriggerEnter2D(Collider2D cCollidObj)  충돌감지 함수
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jack5_EndPoint : MonoBehaviour
{
    GameObject mg_EventManager;

    // Start is called before the first frame update
    void Start()
    {
        this.mg_EventManager = GameObject.Find("GameDirector");
    }

    /// <summary>
    /// 오브젝트 충돌시 작동되는 함수
    /// </summary>
    /// <param name="cCollidObj">충돌된 객체</param>
    void OnTriggerEnter2D(Collider2D cCollidObj)
    {
        SceneManager.LoadScene("Jack_Epi6");
    }
}
