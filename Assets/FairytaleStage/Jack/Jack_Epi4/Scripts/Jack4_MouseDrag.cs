/*
 * - Name : Jack4_MouseDrag.cs
 * - Writer : 김명현
 * 
 * - Content
 * 잭과콩나무 에피소드4 - 마우스 이벤트 스크립트
 * 마우스 드래그시 오브젝트가 따라 움직이게 수정
 * 마우스에서 손을 뗄 경우 오브젝트 원래위치로 이동
 * 
 * - Update Log -
 * 2021-07-13 : 작성 완료
 * 2021-07-23 : 주석변경
 *                  
 * - Variable 
 * mv2_mouseDragPosition                마우스 위치를 저장하는 벡터
 * mv2_worldObjectPosition              카메라의 월드좌표로 변환을 위한 벡터
 * mb_flag                              원하는시점에 드래그를 활성화하기 위한 flag
 * - Function()
 * OnMouseDrag()                        오브젝트를 드래그한 경우
 * OnMouseUp()                          오브젝트에서 손을 떼는 경우
 * v_ChangeFlagTrue()                   Flag 값 True
 * v_ChangeFlagTrue()                   Flag 값 False
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 마우스 조작관련 이벤트를 처리해주는 스크립트
/// </summary>
public class Jack4_MouseDrag : MonoBehaviour
{
    private bool mb_flag;
    private bool mb_BeanPositionFlag;
    private SoundManager msm_soundManager;
    GameObject mg_ScriptManager;
    private bool PlayOnce;

    // Start is called before the first frame update
    void Start()
    {
        mb_BeanPositionFlag = false;
        msm_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        this.mg_ScriptManager = GameObject.Find("GameDirector");
        PlayOnce = false;
    }

    /// <summary>
    /// 오브젝트를 드래그한 경우 오브젝트가 마우스 위치따라 이동
    /// </summary>
    private void OnMouseDrag()
    {
        if (mb_flag == true)
        {
            Vector2 mv2_mouseDragPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 mv2_worldObjectPosition = Camera.main.ScreenToWorldPoint(mv2_mouseDragPosition);
            this.transform.position = mv2_worldObjectPosition;
            Debug.Log("오브젝트 드래그");
            if (PlayOnce == false)
            {
                msm_soundManager.playSound(0);
                PlayOnce = true;
            }
        }

        if(this.tag == "Bean")
        {
            this.mg_ScriptManager.GetComponent<Jack4_EventController>().DragFalgTrue();
        }
    }

    //마우스에서 손을 뗄 경우 원래위치로 돌아가게끔 설정
    private void OnMouseUp()
    {
        Debug.Log("오브젝트에서 손 뗌");
        if (this.tag == "Bean")
        {
            if(mb_BeanPositionFlag == false)
            {
                this.transform.position = new Vector3(-3, -4.5f, 0);
            }
            else
            {
                this.transform.position = new Vector3(5.2f, -3.5f, 0);
            }
            this.mg_ScriptManager.GetComponent<Jack4_EventController>().DragFalgFalse();
            if(mb_flag == true)
            {
                msm_soundManager.playSound(2);
            }
        }
        PlayOnce = false;
    }

    public void v_ChangeFlagTrue()
    {
        mb_flag = true;
    }
    public void v_ChangeFlagFalse()
    {
        mb_flag = false;
    }

    public void v_BeanPositionFlagTrue()
    {
        mb_BeanPositionFlag = true;
    }
}
