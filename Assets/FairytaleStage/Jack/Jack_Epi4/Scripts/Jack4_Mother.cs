/*
 * - Name : Jack4_Mother.cs
 * - Writer : 김명현
 * 
 * - Content :
 * 잭과콩나무 에피소드4 - 어머니 오브젝트 스크립트
 * 어머니와 콩의 객체 충돌처리를 위한 스크립트
 * 
 *  * - Update Log -
 * 2021-07-13 : 제작 완료
 * 2021-07-23 : 주석 변경
 *          
 * - Variable
 * mg_EventManager                          감독 오브젝트 연결을 위한 변수
 * mg_Bean                                  콩 오브젝트 연결을 위한 변수
 * 
 * - Function
 * OnTriggerEnter2D(Collider2D cCollidObj)  충돌감지 함수
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 어머니 객체의 충돌처리를 위한 스크립트
/// </summary>
public class Jack4_Mother : MonoBehaviour
{
    GameObject mg_EventManager;
    GameObject mg_Bean;

    public Sprite[] MotherImage = new Sprite[2];

    // Start is called before the first frame update
    void Start()
    {
        this.mg_EventManager = GameObject.Find("GameDirector");
        this.mg_Bean = GameObject.Find("Bean");
    }

    /// <summary>
    /// 충돌이 일어나고 있는동안 작동되는 함수
    /// </summary>
    /// <param name="cCollidObj">충돌된 오브젝트</param>
    void OnTriggerEnter2D(Collider2D cCollidObj)
    {
        Debug.Log("충돌 감지");
        if (cCollidObj.tag == "Bean")
        {
            cCollidObj.gameObject.transform.position = new Vector3(5.2f, -3.5f, 0);
            //Destroy(cCollidObj.gameObject);
            this.mg_EventManager.GetComponent<Jack4_EventController>().v_BeanToMother();
            this.mg_Bean.GetComponent<Jack4_MouseDrag>().v_BeanPositionFlagTrue();
        }
    }
    /// <summary>
    /// 어머니 이미지를 화난 이미지로 변경해주는 함수
    /// </summary>
    public void ChangeMotherAngry()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = MotherImage[1];
    }
}
