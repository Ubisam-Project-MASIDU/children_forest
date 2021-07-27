/*
  * - Name : CollideBeanStalk.cs
  * - Writer : 이윤교
  * - Content : 잭과콩나무 에피소드14 - 도끼질 하며 콩나무 자르기 스크립트
  * 
  * - HISTORY
  * 2021-07-15 : 초기 개발
  * 2021-07-16 : 파일 인코딩 수정 및 주석 처리
  * 2021-07-26 : 효과음 추가 완료
  * 2021-07-27 : 주석 처리 수정
  *
  * <Variable>
  * mg_Click : 미션유도클릭
  * mg_Giant : 거인
  * mn_checkAxing : 도끼질 횟수
  * mb_checkEnd : Epi14 내용 끝났는지 확인
  * GiantSound : 거인 떨어지면서 나는 소리 
  * AxSound : 도끼질 하는 소리
  * sc : 동화 스크립트 나타내기 위한 오브젝트
  * vm : 음성 TTS를 처리하는 오브젝트 연결
  *
  * <Function>
  * 자식 오브젝트(child)에 접근하는 방법 : transform.GetChild(인덱스) -> 인덱스는 위에서 0부터 시작한다.
  * OnTriggerEnter2D(Collider2D cCollideObject) : 오브젝트간 충돌이 일어날때 처음 한번만 호출되는 함수
  * gotoEpi15Scene() : Epi15 씬으로 이동하는 함수
  * PlayGiant() : 거인 비명소리 재생하는 함수
  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//도끼질 하며 콩나무 자르기 클래스
public class CollideBeanStalk : MonoBehaviour{
    private GameObject mg_Click; 
    private GameObject mg_Giant; 
    private int mn_checkAxing = 0; 
    private bool mb_checkEnd = false; 
    private AudioSource GiantSound;  
    private AudioSource AxSound; 
    
    private ScriptControl sc;
    VoiceManager vm;

    //초기설정
    void Start(){
        mg_Click = GameObject.Find("Click (1)");                                //Click (1) 게임 오브젝트를 찾아서 mg_Click 변수에 저장
        GameObject g_initBean = transform.GetChild(mn_checkAxing).gameObject;   // 부모 오브젝트의 스크립트에서 자식 오브젝트를 가져와서 g_initBean오브젝트에 저장
        g_initBean.SetActive(true);                                             //g_initBean 오브젝트 활성화

        GiantSound = GameObject.Find("GiantSound").GetComponent<AudioSource>();
        AxSound = GameObject.Find("AxSound").GetComponent<AudioSource>();

        sc = ScriptControl.GetInstance();                                       // Instance 리턴 받아 사용
        this.vm = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
    }

    void Update(){
        float temp = 0f;
        if(mn_checkAxing > 8) {
            //giant fall.. to y : -1
            if (!mb_checkEnd){                                                  //Epi14 내용 진행중이면
                Destroy(mg_Click);                                              //mg_Click 오브젝트 없애기
                mg_Giant.transform.position = Vector2.MoveTowards(mg_Giant.transform.position, new Vector2(2f, 0.3f), 2f * Time.deltaTime);
                temp = Mathf.Abs(mg_Giant.transform.position.y - 0.3f);
                PlayGiant();                                                     //거인 고함 재생
                if( temp <= 0.1f && !mb_checkEnd) {
                Destroy(mg_Giant);                                              //mg_Giant 오브젝트 없애기
                    Invoke("gotoEpi15Scene", 3.5f);                             //1초 후 gotoEpi15Scene 함수 수행
                    mb_checkEnd = true;                                         //Epi14 내용 끝
                }
            }
        }
        else if(mn_checkAxing == 8) {                                           //콩나무가 다 잘렸으면
            mg_Giant = transform.Find("giant").gameObject;                      //mg_Giant 오브젝트 찾아서
            mg_Giant.SetActive(true);                                           //mg_Giant 오브젝트 활성화
            sc.setNextScript();                                                 //2번째 스크립트 불러오기
            vm.playVoice(2);                                                    //2번째 스크립트 음성 재생
            mn_checkAxing++;                                                    //도끼질 횟수 추가
        }
    }

    //도끼질하면서 콩나무 모습이 바뀌는 함수
    void OnTriggerExit2D(Collider2D cCheckCollidedObject) {
        if(mn_checkAxing < 8) {
            GameObject g_axedBean = transform.GetChild(mn_checkAxing).gameObject; // 부모 오브젝트의 스크립트에서 자식 오브젝트를 가져와서 g_axedBean오브젝트에 저장
            g_axedBean.SetActive(false);                                          //g_axedBean 오브젝트 비활성화
            mn_checkAxing++;                                                      //도끼질 횟수 추가
            AxSound.Play();                                                       //도끼질 효과음 재생
            GameObject g_initBean = transform.GetChild(mn_checkAxing).gameObject; //부모 오브젝트의 스크립트에서 자식 오브젝트를 가져와서 g_initBean오브젝트에 저장
            g_initBean.SetActive(true);                                           //g_initBean 오브젝트 활성화
        }
    }

    //Epi15 씬으로 이동하는 함수
    void gotoEpi15Scene() {
        SceneManager.LoadScene("Jack_Epi15");                                     //Jack_Epi15 씬 로드
    }

    //거인 비명소리 재생하는 함수
    void PlayGiant(){
        GiantSound.Play();
    }
}
