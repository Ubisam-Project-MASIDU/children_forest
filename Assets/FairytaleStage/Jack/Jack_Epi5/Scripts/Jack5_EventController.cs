/*
 * - Name : Jack5_EventController.cs
 * - Writer : 김명현
 * 
 * - Content :
 * 잭과콩나무 에피소드5 - 이벤트 관리 스크립트
 * 게임진행 이벤트를 총괄적으로 관리하기 위한 스크립트
 * 
 * - Update Log -
 * 1. 2021-07-14 : 제작 완료
 * 2. 2021-07-16 : 화살표 기능 추가 및 인코딩형식 변경
 * 3. 2021-07-23 : 음성기능 추가 및 주석 작성
 * 4. 2021-07-27 : 중복되는 스크립트 정리
 * 
 * - Variable 
 * mg_GameDirector                  오브젝트 연결을 위한 변수 -> 게임 디렉터 오브젝트에 접근하기 위한 변수
 * mg_Jack                          오브젝트 연결을 위한 변수 -> Jack 오브젝트에 접근하기 위한 변수
 * mvm_playVoice                    오브젝트 연결을 위한 변수 -> VoiceManager 오브젝트에 접근하기 위한 변수
 * mg_GenJackSpeechBubble           말풍선 관련 변수 -> Jack 말풍선 프리팹과 연결을 위한 변수
 * mg_JackSpeech                    말풍선 관련 변수 -> Jack 말풍선 프리팹과 연결을 위한 변수
 * mb_EventFlag                     이벤트 관리를 위한 변수 -> 같은 이벤트 반복을 피하기 위한 Flag
 * mn_EventSequence                 이벤트 관리를 위한 변수 -> 이벤트 순서를 관리하는 변수
 * mb_SoundFlag                     이벤트 관리를 위한 변수 -> 처음 씬이 실행될때 음성이 한번만 나오기 위한 Flag
 * mb_StopClickFlag                 이벤트 관리를 위한 변수 -> 클릭으로 다음 이벤트로 넘어가는 것을 방지하기위한 Flag
 * mg_ArrowToJack                   화살표 관련 변수 -> Jack을 가르키는 변수
 * mg_ArrowToEndPoint               화살표 관련 변수 -> 끝 지점을 가르키는 변수
 * mg_ArrowPrefab                   화살표 관련 변수 -> 화살표 프리팹 연결을 위한 변수
 * 
 * - Function
 * v_ChangeFlagFalse()              Flag 변경 함수 -> False로 설정
 * v_ChangeFlagTrue()               Flag 변경 함수 -> True로 설정
 * v_NextMainScript()               메인 스크립트 함수 -> 다음 메인 스크립트를 출력
 * v_NoneMainScript()               메인 스크립트 함수 -> 메인 스크립트 내용을 지워 아무것도 출력안되게 설정
 * v_NextEventScript()              이벤트 스크립트 함수 -> 다음 이벤트 스크립트를 출력
 * v_NoneEventScript()              이벤트 스크립트 함수 -> 이벤트 스크립트 내용을 지워 아무것도 출력안되게 설정
 * v_NextJackScript()               잭 스크립트 함수 -> 다음 Jack 스크립트를 출력
 * v_NoneJackScript()               잭 스크립트 함수 -> Jack 스크립트 내용을 지워 아무것도 출력안되게 설정
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Jack5_EventController : MonoBehaviour
{
    #region 변수 선언부
    //오브젝트 연결을 위한 변수 선언
    GameObject mg_GameDirector;                                                 // 게임 디렉터 오브젝트에 접근하기 위한 변수
    GameObject mg_Jack;                                                         // Jack 오브젝트에 접근하기 위한 변수
    VoiceManager mvm_playVoice;                                                 // VoiceManager 오브젝트에 접근하기 위한 변수

    // 말풍선 관련 변수
    GameObject mg_GenJackSpeechBubble;                                          // Jack 말풍선 프리팹과 연결을 위한 변수
    public GameObject mg_JackSpeech;                                            // Jack 말풍선 프리팹과 연결을 위한 변수

    // 이벤트 관리를 위한 변수
    private bool mb_EventFlag;                                                  // 이벤트를 한번만 작동하기 위한 flag
    private int mn_EventSequence = 0;                                           // 이벤트 순서를 관리하는 변수
    private bool mb_SoundFlag;                                                  // 소리가 한번만 나오게 하기위한 Flag
    private bool StopClickFlag;                                                 // 클릭으로 다음 이벤트로 넘어가는 것을 방지하기위한 Flag

    // 화살표 관련 변수
    GameObject mg_ArrowToJack;                                                  // Jack을 가르키는 변수
    GameObject mg_ArrowToEndPoint;                                              // 끝 지점을 가르키는 변수
    public GameObject mg_ArrowPrefab;                                           // 화살표 프리팹 연결을 위한 변수
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //오브젝트 연결
        this.mg_GameDirector = GameObject.Find("GameDirector");
        this.mg_Jack = GameObject.Find("Jack");
        this.mvm_playVoice = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();

        //이벤트 flag
        StopClickFlag = false;
        mb_SoundFlag = false;
        v_ChangeFlagFalse();

        //드래그 금지 함수
        v_TurnOFFMouseDrag();
    }

    // Update is called once per frame
    void Update()
    {
        // 클릭시 다음 이벤트로 넘어가게끔 설정해줌
        if (Input.GetMouseButtonDown(0) && !(mvm_playVoice.isPlaying()) && mvm_playVoice.mb_checkSceneReady)
        {
            if (StopClickFlag == false)
            {
                mn_EventSequence += 1;
            }
            v_ChangeFlagTrue();
        }

        // 전체적인 이벤트
        if(mn_EventSequence == 0 && mb_SoundFlag == false && mvm_playVoice.mb_checkSceneReady)                    // 처음 씬이 실행됬을때
        {
            v_NextMainScript();
            mb_SoundFlag = true;
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 1 && this.mb_EventFlag == true)                                            // 화면이 한번 터치되고 다음 이벤트로 넘어감
        {
            v_ChangeFlagFalse();

            v_NextMainScript();
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 2 && this.mb_EventFlag == true)                                            // 화면이 두번 터치되고 다음 이벤트로 넘어감
        {
            v_ChangeFlagFalse();

            v_NextMainScript();
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 3 && this.mb_EventFlag == true)
        {
            v_ChangeFlagFalse();

            v_NoneMainScript();

            v_GenJackSpeechBubble();
            v_NextJackScript();
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 4 && this.mb_EventFlag == true)
        {
            v_ChangeFlagFalse();

            v_NextJackScript();
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 5 && this.mb_EventFlag == true)
        {
            v_ChangeFlagFalse();

            v_RemoveJackSpeechBubble();

            v_NextMainScript();
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 6 && this.mb_EventFlag == true)
        {
            v_ChangeFlagFalse();

            v_TurnOnMouseDrag();

            v_NextEventScript();
            mvm_playVoice.playVoice(mn_EventSequence);
        }

        // 잭 옮기는 이벤트중 옳지않은곳으로 이동시 원래위치로 복귀
        if (this.mg_Jack.GetComponent<CharacterMovesWhenDragging>().b_CheckMouseUp() == true)                      
        {
            mg_Jack.transform.position = new Vector3(-6.22f, -3.69f, 0);
        }

        // 드래그상태에 맞게 화살표 이펙트등장
        if (mg_Jack.GetComponent<CharacterMovesWhenDragging>().b_CheckDragging() == false && mn_EventSequence >= 6)
        {
            v_GenArrowToJack();
            v_RemoveArrowToEndPoint();
        }
        else if (mg_Jack.GetComponent<CharacterMovesWhenDragging>().b_CheckDragging() == true && mn_EventSequence >= 6)
        {
            v_RemoveArrowToJack();
            v_GenArrowToEndPoint();
        } 
    }

    #region 함수 선언부
    //Flag 변경 함수
    private void v_ChangeFlagFalse()
    {
        this.mb_EventFlag = false;
    }
    private void v_ChangeFlagTrue()
    {
        this.mb_EventFlag = true;
    }

    //메인 스크립트 함수
    private void v_NextMainScript()
    {
        this.mg_GameDirector.GetComponent<ScriptManager>().v_NextScript(0);
    }
    private void v_NoneMainScript()
    {
        this.mg_GameDirector.GetComponent<ScriptManager>().v_NoneScript(0);
    }

    //이벤트 스크립트 함수
    private void v_NextEventScript()
    {
        this.mg_GameDirector.GetComponent<ScriptManager>().v_NextScript(0);
    }
    private void v_NoneEventScript()
    {
        this.mg_GameDirector.GetComponent<ScriptManager>().v_NoneScript(0);
    }
    
    //잭 스크립트 함수
    private void v_NextJackScript()
    {
        this.mg_GameDirector.GetComponent<ScriptManager>().v_NextScript(1);
    }
    private void v_NoneJackScript()
    {
        this.mg_GameDirector.GetComponent<ScriptManager>().v_NoneScript(1);
    }

    //말풍선 생성 함수
    private void v_GenJackSpeechBubble()
    {
        mg_GenJackSpeechBubble = Instantiate(mg_JackSpeech) as GameObject;
        mg_GenJackSpeechBubble.transform.position = new Vector3(-3, -1, 0);
    }

    //말풍선 삭제 함수
    private void v_RemoveJackSpeechBubble()
    {
        this.mg_GameDirector.GetComponent<ScriptManager>().v_NoneScript(1);
        Destroy(this.mg_GenJackSpeechBubble);
    }

    //드래그 활성화
    private void v_TurnOnMouseDrag()
    {
        this.mg_Jack.GetComponent<CharacterMovesWhenDragging>().v_ChangeDragFlagTrue();
    }

    //드래그 비활성화
    private void v_TurnOFFMouseDrag()
    {
        this.mg_Jack.GetComponent<CharacterMovesWhenDragging>().v_ChangeDragFlagFalse();
    }

    public void v_GenArrowToJack()
    {
        if (mg_ArrowToJack == null)
        {
            mg_ArrowToJack = Instantiate(mg_ArrowPrefab) as GameObject;
            mg_ArrowToJack.transform.position = new Vector3(-4.8f, -2.5f, 0);
            mg_ArrowToJack.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void v_GenArrowToEndPoint()
    {
        if (mg_ArrowToEndPoint == null)
        {
            mg_ArrowToEndPoint = Instantiate(mg_ArrowPrefab) as GameObject;
            mg_ArrowToEndPoint.transform.position = new Vector3(3.1f, 3.6f, 0);
            mg_ArrowToEndPoint.GetComponent<SpriteRenderer>().flipX = false;
            mg_ArrowToEndPoint.GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    public void v_RemoveArrowToJack()
    {
        if (mg_ArrowToJack != null)
        {
            Destroy(mg_ArrowToJack);
        }
    }
    public void v_RemoveArrowToEndPoint()
    {
        if (mg_ArrowToEndPoint != null)
        {
            Destroy(mg_ArrowToEndPoint);
        }
    }
    #endregion
}
