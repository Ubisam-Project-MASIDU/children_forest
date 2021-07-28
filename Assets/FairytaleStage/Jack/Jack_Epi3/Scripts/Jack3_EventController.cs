/*
 * - Name : Jack3_EventController.cs
 * - Writer : 김명현
 * 
 * - Content :
 * 잭과콩나무 에피소드3 - 이벤트 관리 스크립트
 * 게임진행 이벤트를 총괄적으로 관리하기 위한 스크립트
 * 
 * - Update Log -
 * 2021-07-13 : 제작 완료
 * 2021-07-16 : 화살표 기능 추가 및 인코딩형식 변경
 * 2021-07-21 : 주석 변경
 * 2021-07-23 : 음성기능 추가 및 주석 작성
 * 
 * - Variable 
 * mg_ScriptManager                 게임 디렉터 오브젝트에 접근하기 위한 변수
 * mg_ArrowToBean                   콩을 가르키는 화살표
 * mg_ArrowToCow                    소를 가르키는 화살표
 * mg_ArrowToGF                     할아버지를 가르키는 화살표
 * mg_ArrowToJack                   잭을 가르키는 화살표
 * mg_ArrowPrefab                   화살표 프리팹 연결을 위한 변수
 * mg_GenGFSpeechBubble             할아버지 말풍선 오브젝트 조작을 위한 변수
 * mg_GFSpeech                      할아버지 말풍선 프리팹과 연결을 위한 변수
 * mg_GenJackSpeechBubble           잭 말풍선 관련 오브젝트
 * mg_JackSpeech                    잭 말풍선 관련 오브젝트
 * mb_EventFlag                     이벤트를 한번만 작동하기 위한 flag
 * mn_EventSequence                 이벤트 순서를 관리하는 변수
 * mg_Cow                           마우스 드래그 관련 오브젝트
 * mg_Bean                          마우스 드래그 관련 오브젝트
 * mb_BeanToJack                    이벤트 성공확인을 위한 flag
 * mb_CowToGF                       이벤트 성공확인을 위한 flag
 * mb_PlaySound                     씬이 시작되었을때 음성이 한번만 나오게 하기위한 Flag
 * 
 * - Function
 * v_ChangeFlagFalse()              Flag 변경 함수 -> 스토리 관련 이벤트가 한번만 작동되게 하기위한 Flag값 False로 변경
 * v_ChangeFlagTrue()               Flag 변경 함수 -> 스토리 관련 이벤트가 한번만 작동되게 하기위한 Flag값 True로 변경
 * v_TurnOnMouseDrag()              Flag 변경 함수 -> 오브젝트 드래그 활성화
 * v_TurnOFFMouseDrag()             Flag 변경 함수 -> 오브젝트 드래그 비활성화
 * v_NoneScript()                   스크립트(대사) 함수 -> 모든 텍스트값을 공백으로 설정하여 출력되는 스크립트(대사)를 없게끔 설정, 생성된 말풍선들 모두 삭제
 * v_NextMainScript()               스크립트(대사) 함수 -> 다음 스크립트(대사)를 메인 스크립트 위치에 출력
 * v_NextJackScript()               스크립트(대사) 함수 -> 다음 스크립트(대사)를 Jack 스크립트 위치에 출력 및 Jack 말풍선 생성
 * v_NextGFScript()                 스크립트(대사) 함수 -> 다음 스크립트(대사)를 할아버지 스크립트 위치에 출력 및 할아버지 말풍선 생성
 * v_GenArrowToBean()               화살표 관련 함수 -> 콩을 가르키는 화살표 생성
 * v_GenArrowToCow()                화살표 관련 함수 -> 소를 가르키는 화살표 생성
 * v_GenArrowToGF()                 화살표 관련 함수 -> 할아버지를 가르키는 화살표 생성
 * v_GenArrowToJack()               화살표 관련 함수 -> Jack을 가르키는 화살표 생성
 * v_RemoveArrowToGF()              화살표 관련 함수 -> 할아버지를 가르키는 화살표 삭제
 * v_RemoveArrowToJack()            화살표 관련 함수 -> Jack을 가르키는 화살표 삭제
 * v_RemoveArrowToBean()            화살표 관련 함수 -> 콩을 가르키는 화살표 삭제
 * v_RemoveArrowToCow()             화살표 관련 함수 -> 소를 가르키는 화살표 삭제
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jack3_EventController : MonoBehaviour
{
    #region 변수 선언부

    //오브젝트 연결을 위한 변수 선언
    GameObject mg_ScriptManager;                                                                    // 게임 디렉터 오브젝트에 접근하기 위한 변수
    GameObject mg_Cow;                                                                              // 소 오브젝트 연결을 위한 변수
    GameObject mg_Bean;                                                                             // 콩 오브젝트 연결을 위한 변수  
    VoiceManager mvm_playVoice;                                                                                // 씬이 시작되었을때 음성이 한번만 나오게 하기위한 Flag

    // 화살표 관련 변수
    GameObject mg_ArrowToBean;                                                                      // 콩을 가르키는 화살표
    GameObject mg_ArrowToCow;                                                                       // 소를 가르키는 화살표
    GameObject mg_ArrowToGF;                                                                        // 할아버지를 가르키는 화살표
    GameObject mg_ArrowToJack;                                                                      // 잭을 가르키는 화살표
    public GameObject mg_ArrowPrefab;                                                               // 화살표 프리팹 연결을 위한 변수

    //말풍선 관련 변수 선언
    GameObject mg_GenGFSpeechBubble;                                                                // 할아버지 말풍선 오브젝트 조작을 위한 변수
    public GameObject mg_GFSpeech;                                                                  // 할아버지 말풍선 프리팹과 연결을 위한 변수
    GameObject mg_GenJackSpeechBubble;                                                              // 잭 말풍선 오브젝트 조작을 위한 변수
    public GameObject mg_JackSpeech;                                                                // 잭 말풍선 프리팹과 연결을 위한 변수

    // 이벤트 관련 Flag 선언
    private bool mb_EventFlag = false;                                                              // 이벤트를 한번만 작동하기 위한 flag
    private int mn_EventSequence = 0;                                                               // 이벤트 순서를 관리하는 변수
    private bool mb_PlaySound = false;                                                              // 처음 씬이 실행될때 음성이 한번만 나오기 위한 Flag
    
    #endregion

    void Start(){
        // 변수를 오브젝트 연결하는 부분
        this.mg_ScriptManager = GameObject.Find("Jack3_GameDirector");
        this.mg_Cow = GameObject.Find("Jack3_Cow");
        this.mg_Bean = GameObject.Find("Jack3_Bean");
        this.mvm_playVoice = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
    }

    void Update(){
        if (Input.GetMouseButtonDown(0) && !(mvm_playVoice.isPlaying()) && mvm_playVoice.mb_checkSceneReady)               // 음성이 끝나고 화면을 클릭시 다음으로 넘어감
        {                         
            mn_EventSequence += 1;
            v_ChangeFlagTrue();
        }

        //드래그 상태에 따른 화살표 이펙트 효과 처리 부분
        if (mg_Cow != null)
        {
            if (mg_Cow.GetComponent<CharacterMovesWhenDragging>().b_CheckDragging() == true)
            {
                v_RemoveArrowToCow();
                v_GenArrowToGF();
                v_RemoveArrowToBean();
            }
            else if (mg_Bean != null)
            {
                if (mg_Bean.GetComponent<CharacterMovesWhenDragging>().b_CheckDragging() == true)
                {
                    v_RemoveArrowToBean();
                    v_GenArrowToJack();
                    v_RemoveArrowToCow();
                }
                else if (mn_EventSequence >= 8)
                {
                    v_RemoveArrowToGF();
                    v_RemoveArrowToJack();
                    if (mg_Cow != null)
                        v_GenArrowToCow();
                    if (mg_Bean != null)
                        v_GenArrowToBean();
                }
            }
            else if (mn_EventSequence >= 8)
            {
                v_RemoveArrowToGF();
                v_RemoveArrowToJack();
                if (mg_Cow != null)
                    v_GenArrowToCow();
                if (mg_Bean != null)
                    v_GenArrowToBean();
            }
        }
        else if(mg_Bean != null)
        {
            if (mg_Bean.GetComponent<CharacterMovesWhenDragging>().b_CheckDragging() == true)
            {
                v_RemoveArrowToBean();
                v_GenArrowToJack();
                v_RemoveArrowToCow();
            }
            else if (mn_EventSequence >= 8)
            {
                v_RemoveArrowToGF();
                v_RemoveArrowToJack();
                if (mg_Cow != null)
                    v_GenArrowToCow();
                if (mg_Bean != null)
                    v_GenArrowToBean();
            }
        }
        else
        {
            SceneManager.LoadScene("Jack_Epi4");
        }

        //전체적인 이벤트
        if (mn_EventSequence == 0 && mb_PlaySound == false && mvm_playVoice.mb_checkSceneReady)                // 처음 씬이 실행되면 기본 스크립트 실행
        {
            mb_PlaySound = true;
            mvm_playVoice.playVoice(mn_EventSequence);
            v_NextMainScript();
            v_TurnOFFMouseDrag();
        }
        if (mn_EventSequence == 1 && this.mb_EventFlag == true){                                    // 화면을 1번 터치하면 진행
            v_ChangeFlagFalse();                                                                    // 이벤트를 한번만 실행하기위한 flag값 False로 변경
            v_NoneScript();
            v_NextGFScript();
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 2 && mb_EventFlag == true){                                    // 화면을 2번 터치하면 진행
            v_ChangeFlagFalse();
            v_NoneScript();
            v_NextJackScript();
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 3 && mb_EventFlag == true){                                    // 화면을 3번 터치하여 진행
            v_ChangeFlagFalse();
            v_NoneScript();
            v_NextGFScript();
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 4 && mb_EventFlag == true){
            v_ChangeFlagFalse();
            v_NoneScript();
            Invoke("v_NextGFScript", 0.1f);
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 5 && mb_EventFlag == true){
            v_ChangeFlagFalse();
            v_NoneScript();
            v_NextMainScript();
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 6 && mb_EventFlag == true){
            v_ChangeFlagFalse();
            v_NoneScript();
            v_NextJackScript();
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 7 && mb_EventFlag == true){
            v_ChangeFlagFalse();
            v_NoneScript();
            v_NextMainScript();
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 8 && mb_EventFlag == true)
        {
            v_ChangeFlagFalse();
            v_NoneScript();
            v_NextMainScript();
            v_TurnOnMouseDrag();
            v_GenArrowToBean();
            v_GenArrowToCow();
            mvm_playVoice.playVoice(mn_EventSequence);
        }
    }
    #region 함수 선언부

    /// <summary>
    /// Flag 변경 함수 -> 스토리 관련 이벤트가 한번만 작동되게 하기위한 Flag값 False로 변경
    /// </summary>
    private void v_ChangeFlagFalse(){                                                                   
        this.mb_EventFlag = false;
    }
    /// <summary>
    /// Flag 변경 함수 -> 스토리 관련 이벤트가 한번만 작동되게 하기위한 Flag값 True로 변경
    /// </summary>
    private void v_ChangeFlagTrue(){
        this.mb_EventFlag = true;
    }
    /// <summary>
    /// Flag 변경 함수 -> 오브젝트 드래그 활성화
    /// </summary>
    private void v_TurnOnMouseDrag()                                                                        // 드래그 활성화
    {
        if (mg_Cow != null)
            this.mg_Cow.GetComponent<CharacterMovesWhenDragging>().v_ChangeDragFlagTrue();
        if (mg_Bean != null)
            this.mg_Bean.GetComponent<CharacterMovesWhenDragging>().v_ChangeDragFlagTrue();
    }
    /// <summary>
    /// Flag 변경 함수 -> 오브젝트 드래그 비활성화
    /// </summary>
    private void v_TurnOFFMouseDrag()                                                                       // 드래그 기능 잠금
    {
        if (mg_Cow != null)
            this.mg_Cow.GetComponent<CharacterMovesWhenDragging>().v_ChangeDragFlagFalse();
        if (mg_Bean != null)
            this.mg_Bean.GetComponent<CharacterMovesWhenDragging>().v_ChangeDragFlagFalse();
    }
    /// <summary>
    /// 스크립트(대사) 함수 -> 모든 텍스트값을 공백으로 설정하여 출력되는 스크립트(대사)를 없게끔 설정, 생성된 말풍선들 모두 삭제
    /// </summary>
    private void v_NoneScript()
    {
        if (mg_GenGFSpeechBubble != null)
        {
            mg_ScriptManager.GetComponent<ScriptManager>().v_NoneScript(2);
            Destroy(mg_GenGFSpeechBubble);
        }
        if (mg_GenJackSpeechBubble != null)
        {
            this.mg_ScriptManager.GetComponent<ScriptManager>().v_NoneScript(1);
            Destroy(this.mg_GenJackSpeechBubble);
        }
        mg_ScriptManager.GetComponent<ScriptManager>().v_NoneScript(0);
        mg_ScriptManager.GetComponent<ScriptManager>().v_NoneScript(1);
        mg_ScriptManager.GetComponent<ScriptManager>().v_NoneScript(2);
    }
    /// <summary>
    /// 스크립트(대사) 함수 -> 다음 스크립트(대사)를 메인 스크립트 위치에 출력
    /// </summary>
    private void v_NextMainScript(){                                                                        // 다음 메인 스크립트를 출력
        mg_ScriptManager.GetComponent<ScriptManager>().v_NextScript(0);
    }
    /// <summary>
    /// 스크립트(대사) 함수 -> 다음 스크립트(대사)를 Jack 스크립트 위치에 출력 및 Jack 말풍선 생성
    /// </summary>
    private void v_NextJackScript(){                                                                        // 다음 Jack 스크립트 출력
        if (mg_GenJackSpeechBubble == null)
        {
            mg_GenJackSpeechBubble = Instantiate(mg_JackSpeech) as GameObject;
            mg_GenJackSpeechBubble.transform.position = new Vector3(-0.5f, -1, 0);
        }
        mg_ScriptManager.GetComponent<ScriptManager>().v_NextScript(1);
    }
    /// <summary>
    /// 스크립트(대사) 함수 -> 다음 스크립트(대사)를 할아버지 스크립트 위치에 출력 및 할아버지 말풍선 생성
    /// </summary>
    private void v_NextGFScript(){                                                                          // 다음 할아버지 스크립트 출력
        if (mg_GenGFSpeechBubble == null)
        {
            mg_GenGFSpeechBubble = Instantiate(mg_GFSpeech) as GameObject;
            mg_GenGFSpeechBubble.transform.position = new Vector3(4, 0.5f, 0);
        }
        mg_ScriptManager.GetComponent<ScriptManager>().v_NextScript(2);
    }
    /// <summary>
    /// 화살표 관련 함수 -> 콩을 가르키는 화살표 생성
    /// </summary>
    public void v_GenArrowToBean()                                                                          // 콩을 가르키는 화살표 생성
    {
        if (mg_ArrowToBean == null)
        {
            mg_ArrowToBean = Instantiate(mg_ArrowPrefab) as GameObject;
            mg_ArrowToBean.transform.position = new Vector3(3.5f, -2.5f, 0);
        }
    }
    /// <summary>
    /// 화살표 관련 함수 -> 소를 가르키는 화살표 생성
    /// </summary>
    public void v_GenArrowToCow()                                                                           // 소를 가르키는 화살표 생성
    {
        if (mg_ArrowToCow == null)
        {
            mg_ArrowToCow = Instantiate(mg_ArrowPrefab) as GameObject;
            mg_ArrowToCow.transform.position = new Vector3(-3.5f, -1, 0);
            mg_ArrowToCow.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    /// <summary>
    /// 화살표 관련 함수 -> 할아버지를 가르키는 화살표 생성
    /// </summary>
    public void v_GenArrowToGF()                                                                            // 할아버지를 가르키는 화살표 생성
    {
        if (mg_ArrowToGF == null)
        {
            mg_ArrowToGF = Instantiate(mg_ArrowPrefab) as GameObject;
            mg_ArrowToGF.transform.position = new Vector3(5.5f, 0, 0);
        }
    }
    /// <summary>
    /// 화살표 관련 함수 -> Jack을 가르키는 화살표 생성
    /// </summary>
    public void v_GenArrowToJack()                                                                          // Jack을 가르키는 화살표 생성
    {
        if (mg_ArrowToJack == null)
        {
            mg_ArrowToJack = Instantiate(mg_ArrowPrefab) as GameObject;
            mg_ArrowToJack.transform.position = new Vector3(-1.5f, -2, 0);
            mg_ArrowToJack.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    /// <summary>
    /// 화살표 관련 함수 -> 할아버지를 가르키는 화살표 삭제
    /// </summary>
    public void v_RemoveArrowToGF()                                                                         // 할아버지를 가르키는 화살표 삭제
    {
        if(mg_ArrowToGF != null)
        {
            Destroy(mg_ArrowToGF);
        }
    }
    /// <summary>
    /// 화살표 관련 함수 -> Jack을 가르키는 화살표 삭제
    /// </summary>
    public void v_RemoveArrowToJack()                                                                       // Jack을 가르키는 화살표 삭제
    {
        if (mg_ArrowToJack != null)
        {
            Destroy(mg_ArrowToJack);
        }
    }
    /// <summary>
    /// 화살표 관련 함수 -> 콩을 가르키는 화살표 삭제
    /// </summary>
    public void v_RemoveArrowToBean()                                                                       // 콩을 가르키는 화살표 삭제
    {
        if(this.mg_ArrowToBean != null)
        {
            Destroy(this.mg_ArrowToBean);
        }
    }
    /// <summary>
    /// 화살표 관련 함수 -> 소를 가르키는 화살표 삭제
    /// </summary>
    public void v_RemoveArrowToCow()                                                                        // 소를 가르키는 화살표 삭제
    {
        if (this.mg_ArrowToCow)
        {
            Destroy(this.mg_ArrowToCow);
        }
    }
    #endregion
}
