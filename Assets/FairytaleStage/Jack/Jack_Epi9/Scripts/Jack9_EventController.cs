/*
 * - Name : Jack9_EventController.cs
 * - Writer : 김명현
 * 
 * - Content :
 * 잭과콩나무 에피소드9 - 이벤트 관리 스크립트
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
 * mg_Sack                          오브젝트 연결을 위한 변수 -> 자루 오브젝트에 접근하기 위한 변수
 * mvm_playVoice                    오브젝트 연결을 위한 변수 -> VoiceManager 오브젝트에 접근하기 위한 변수
 * mg_GenGiantSpeechBubble          말풍선 관련 변수 -> 거인 말풍선 관리를 위한 변수
 * mg_GiantSpeech                   말풍선 관련 변수 -> Jack 말풍선 프리팹과 연결을 위한 변수
 * mb_DontLoopEvent1                이벤트 관리를 위한 변수 -> 이벤트 반복을 피하기 위한 Flag
 * mb_DontLoopEvent2                이벤트 관리를 위한 변수 -> 이벤트 반복을 피하기 위한 Flag
 * mb_EventFlag                     이벤트 관리를 위한 변수 -> 이벤트 반복을 피하기 위한 Flag
 * mn_EventSequence                 이벤트 관리를 위한 변수 -> 이벤트 순서를 관리하는 변수
 * mb_SoundFlag                     이벤트 관리를 위한 변수 -> 처음 씬이 실행될때 음성이 한번만 나오기 위한 Flag
 * mb_StopClickFlag                 이벤트 관리를 위한 변수 -> 클릭으로 다음 이벤트로 넘어가는 것을 방지하기위한 Flag
 * IsSackDestroy                    이벤트 관리를 위한 변수 -> 자루가 파괴되었는지 확인하기위한 Flag
 * mg_ArrowToSackLeft               화살표 관련 변수 -> 자루를 가르키는 왼쪽 화살표 변수
 * mg_ArrowToSackRight              화살표 관련 변수 -> 자루를 가르키는 오른쪽 화살표 변수
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
using UnityEngine.SceneManagement;



public class Jack9_EventController : MonoBehaviour
{
    #region 변수 선언부
    // 오브젝트 연결을 위한 변수
    GameObject mg_GameDirector;                                                                         // 게임 디렉터 오브젝트에 접근하기 위한 변수
    GameObject mg_Sack;                                                                                 // 자루 오브젝트에 접근하기 위한 변수
    VoiceManager mvm_playVoice;                                                                         // VoiceManager 오브젝트에 접근하기 위한 변수

    // 말풍선 관련 오브젝트
    GameObject mg_GenGiantSpeechBubble;
    public GameObject mg_GiantSpeech;

    //이벤트 관리를 위한 변수
    private bool mb_DontLoopEvent1;
    private bool mb_DontLoopEvent2;
    private bool mb_EventFlag;                                                                          // 이벤트를 한번만 작동하기 위한 flag
    private int mn_EventSequence;                                                                       // 이벤트 순서를 관리하는 변수
    private bool mb_SoundFlag;
    private bool StopClickFlag;                                                                         // 클릭으로 다음 이벤트로 넘어가는 것을 방지하기위한 Flag
    private bool IsSackDestroy;

    //화살표 오브젝트
    GameObject mg_ArrowToSackLeft;
    GameObject mg_ArrowToSackRight;
    public GameObject mg_ArrowPrefab;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //오브젝트 연결
        this.mg_GameDirector = GameObject.Find("GameDirector");
        this.mvm_playVoice = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
        this.mg_Sack = GameObject.Find("Sack");

        //이벤트 flag
        mb_DontLoopEvent1 = false;
        mb_DontLoopEvent2 = false;
        StopClickFlag = false;
        IsSackDestroy = false;

        //이벤트 관련
        v_ChangeFlagFalse();
        mn_EventSequence = 0;

        //이벤트 시작
        v_NextMainScript();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && mvm_playVoice.mb_checkSceneReady)
        {
            if (StopClickFlag == false && !(mvm_playVoice.isPlaying()))
            {
                mn_EventSequence += 1;
            }
            v_ChangeFlagTrue();
        }

        if (mn_EventSequence == 0 && mb_SoundFlag == false && mvm_playVoice.mb_checkSceneReady)                    // 처음 씬이 실행되면 기본 스크립트 실행
        {
            mb_SoundFlag = true;
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 1 && this.mb_EventFlag == true && !(mvm_playVoice.isPlaying()) && mb_SoundFlag == true)
        {
            v_ChangeFlagFalse();
            mb_SoundFlag = false;
            v_NoneMainScript();

            v_GenGiantSpeechBubble();
            v_NextGiantScript();
            mvm_playVoice.playVoice(mn_EventSequence);
        }
        else if (mn_EventSequence == 2 && this.mb_EventFlag == true && mb_SoundFlag == false)
        {
            v_ChangeFlagFalse();
            mb_SoundFlag = true;
            v_RemoveGiantSpeechBubble();

            v_NextMainScript();
            mvm_playVoice.playVoice(mn_EventSequence);
            mb_DontLoopEvent1 = true;
        }
        else if (mn_EventSequence == 3 && this.mb_EventFlag == true && mb_DontLoopEvent1 == true)
        {
            v_ChangeFlagFalse();

            v_NextEventScript();
            mg_Sack.GetComponent<Jack9_Sack>().ChangSackFlagTrue();
            StopClickFlag = true;

            mb_DontLoopEvent1 = false;
            mb_DontLoopEvent2 = true;
            mvm_playVoice.playVoice(mn_EventSequence);
            v_GenArrowToSack1();
            v_GenArrowToSack2();
        }
        else if (mn_EventSequence == 3 && IsSackDestroy == true && mb_DontLoopEvent2 == true)
        {
            v_ChangeFlagFalse();
            StopClickFlag = false;
            mb_DontLoopEvent2 = false;

            v_NextMainScript();

            this.mg_GameDirector.GetComponent<Jack9_Gentreasure>().v_GenTreasure();
            mvm_playVoice.playVoice(mn_EventSequence+1);
            v_RemoveArrowToSack1();
            v_RemoveArrowToSack2();
        }
        else if (mn_EventSequence == 4 && IsSackDestroy == true)
        {
            v_ChangeFlagFalse();


            SceneManager.LoadScene("Jack_Epi10");

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
    
    //거인 스크립트 함수
    private void v_NextGiantScript()
    {
        this.mg_GameDirector.GetComponent<ScriptManager>().v_NextScript(1);
    }
    private void v_NoneGiantScript()
    {
        this.mg_GameDirector.GetComponent<ScriptManager>().v_NoneScript(1);
    }
    
    //말풍선 생성 함수
    private void v_GenGiantSpeechBubble()
    {
        mg_GenGiantSpeechBubble = Instantiate(mg_GiantSpeech) as GameObject;
        mg_GenGiantSpeechBubble.transform.position = new Vector3(0, 3.3f, 0);
    }

    //말풍선 삭제 함수
    private void v_RemoveGiantSpeechBubble()
    {
        this.mg_GameDirector.GetComponent<ScriptManager>().v_NoneScript(1);
        Destroy(this.mg_GenGiantSpeechBubble);
    }
    
    public void v_IsSackDestroy()
    {
        IsSackDestroy = true;
    }

    public void v_GenArrowToSack1()
    {
        if (mg_ArrowToSackLeft == null)
        {
            mg_ArrowToSackLeft = Instantiate(mg_ArrowPrefab) as GameObject;
            mg_ArrowToSackLeft.transform.position = new Vector3(-0.87f, -0.57f, 0);
        }
    }
    public void v_GenArrowToSack2()
    {
        if (mg_ArrowToSackRight == null)
        {
            mg_ArrowToSackRight = Instantiate(mg_ArrowPrefab) as GameObject;
            mg_ArrowToSackRight.transform.position = new Vector3(5, -0.57f, 0);
            mg_ArrowToSackRight.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void v_RemoveArrowToSack1()
    {
        if (mg_ArrowToSackLeft != null)
        {
            Destroy(mg_ArrowToSackLeft);
        }
    }
    public void v_RemoveArrowToSack2()
    {
        if (mg_ArrowToSackRight != null)
        {
            Destroy(mg_ArrowToSackRight);
        }
    }
    #endregion
}
