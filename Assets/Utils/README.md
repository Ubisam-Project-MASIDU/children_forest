# Utils
***
 - 작성 및 제작 : 최대준
 - 언어 : C#
***
 - Update Log
     - 21.07.07 : FixedStartScene.cs 스크립트 제작.
     - 21.07.12 : TTS.cs 스크립트 작업 시작.
     - 21.07.19 : TTS.cs 스크립트 제작.
     - 21.07.19 : VoiceManager.cs 스크립트 제작.
     - 21.07.19 : SoundManager.cs 스크립트 제작.
     - 21.07.22 : ImgSizeResize.cs 스크립트 제작.
     - 21.07.23 : BGMmanager.cs 스크립트 제작.
***
- Utils 구성 정보
> 🗂 *Scripts*
>   ⌙공통적으로 사용되는 스크립트 코드들로 구성되어 있는 폴더.
>   
> > ⌙📄 FixedStartScene.cs
> > > 게임 실행시에 intro 씬으로 고정시켜주는 스크립트.
> > > 
> > ⌙📄 TTS.cs
> > > 게임 시에 필요한 단어나 대화를 음성으로 읽어줄 수 있는 TTS 기술을 이용하기 위해서 Google TTS API 서버와 통신을 담당하는 클래스가 들어있는 스크립트.
> > > 
> > ⌙📄 VoiceManager.cs
> > > TTS 클래스의 사용법은 음성 텍스트, 음성 커스터마이징 세팅등을 이용해 클래스 인스턴스를 만들어 API 서버와 통신을 하는데, 그것을 씬에 적용하기 편리하게 한번 래핑해주는 오브젝트 클래스 스크립트이다.
> > > 
> > ⌙📄 SoundManager.cs
> > > 씬에서 사용되는 효과음을 출력해주기 위한 오브젝트 클래스 스크립트이다.
> > > 
> > ⌙📄 ImgSizeResize.cs
> > > 다양한 스크린 사이즈에 맞춰 씬의 배경의 크기를 맞춰주는 스크립트이다.
> > > 
> > ⌙📄 BGMmanager.cs
> > > SoundManager와 작동 성질이 달라서 구분 지어 코드를 짜게 되었다. SoundManager에서 출력하는 음성은 씬이 전환되면, 끊어져도 상관없지만, BGMmanager에서 출력되는 배경음은 씬이 전환되어도 살아 있어야 하기 때문에 성질이 달라 구분 지어 코드를 짜게 되었다.
> > > 
> 🗂 *Prefab*
>   ⌙공통적으로 사용되는 프리팹 오브젝트들로 구성되어 있는 폴더.
>   
> > ⌙📄 backController.prefab
> > > 게임 스테이지에서 나가는 백 버튼을 일반화시킨 프리팹이다.
> > > 
> > ⌙📄 BackgroundCanvas.prefab
> > > 위에서 언급한 ImgSizeResize 스크립트를 적용시켜 스크린 사이즈에 배경 사이즈를 맞춰 일반화시킨 프리팹이다.
> > > 
> > ⌙📄 JackBackgroundCanvas.prefab
> > > 잭과 콩나무에서는 배경 뿐만 아니라 버튼 또한 일반 게임 스테이지와 조금 다르기 때문에 잭과 콩나무 스테이지에서 사용하기 위해 일반화시킨 프리팹이다.
> > > 
> > ⌙📄 Loading.prefab
> > > 로딩 화면을 일반화시킨 프리팹이다.
> > > 
> > ⌙📄 SoundManager.prefab
> > > SoundManager 스크립트에서 정의한 내용을 수행하는 오브젝트를 일반화시킨 프리팹으로, 인스펙터 창에 입력된 효과음 클립을 출력한다.
> > > 
> > ⌙📄 VoiceManager.prefab
> > > VoiceManager 스크립트에서 정의한 내용을 수행하는 오브젝트를 일반화시킨 프리팹으로, 인스펙터 창에 입력된 음성을 출력한다.
> > > 

***

 - 참고사항

1. VoiceManager 프리팹을 사용하는 경우

    - 사용하려는 씬에 VoiceManager 프리팹을 드래그하여 적용시키고, VoiceManager 오브젝트의 인스펙터 창에서 원하는 

2. 스크립트를 띄우는 방법

    - 메인 스크립트인 Jack3_EventController.cs 에서 각 스크립트를 오브젝트를 통해 연결을하고 v_NextScript() 함수를 작동하면 다음 스크립트가 나타난다.

3. 대화를 지우는 방법

    - v_NoneScript()함수를 사용하면 된다.

***

