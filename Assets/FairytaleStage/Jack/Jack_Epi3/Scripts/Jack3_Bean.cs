/*
 * - Name : Jack3_Bean.cs
 * - Writer : 김명현
 * 
 * - Content
 * 콩 오브젝트 스크립트
 * 드래그 해제시 원래위치로 돌아가게끔 설정
 * 
 * - History
 * 1. 2021-07-28 : 초안 작성
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 드래그 해제시 원래위치로 돌아가게끔 설정
/// </summary>
public class Jack3_Bean : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<CharacterMovesWhenDragging>().b_CheckMouseUp() == true)
        {
            this.transform.position = new Vector3(5, -3.5f, 0);
        }
    }
}
