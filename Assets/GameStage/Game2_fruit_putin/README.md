# 과일 넣기 
***
 - 작성 및 제작 : 최대준
 - 언어 : C#
***
 - Update Log
     - 21.07.06 : 코드 작성완료
     - 21.07.08 : 엔딩씬 연결
     - 21.07.16 : 인코딩형식 UTF8로 수정
     - 21.07.19 : 해상도 변경에 따른 텍스트 크기 수정
     - 21.07.20 : 과일마다 클릭할 시 해당 과일 음성 출력.
     - 21.07.26 : 과일마다 클릭하면 효과음 출력.
***
 - 구동화면 및 내용

<img src = "https://user-images.githubusercontent.com/69896751/126114122-93dd17d5-abb3-4f2f-aa98-6c48316fa167.png" width="500" height="220">


    - 씬에 설정된 과일을 바구니에 드래그하여 과일을 바구니에 담는다.
    - 남은 과일이 없다면 게임이 종료된다.
    

***


- FruitPutIn 구성 정보
  - Image
    - 씬에 사용되는 이미지들 저장
  - Scripts
    - ControlFruit.cs : 씬에 생성된 과일의 상호작용 스크립트.
    - InitializeStage : 씬에 과일을 생성하는 스크립트.
  - Prefab
    - Fruit.prefab : 과일 오브젝트의 설정을 일반화시켜 놓은 파일.
    - target_basket.prefab : 과일이 담길 바구니 오브젝트의 설정을 일반화시켜 놓은 파일. 
  - fruit_putin.unity : 씬 파일

***

 - 참고사항

   - ※주의 : 과일의 종류를 늘린다면, InitializeStage가 컴포넌트로 들어간 오브젝트의 인스펙터 창에서 과일의 sprite 이미지 파일을 적용시켜 주어야 하고, 그에 따른 과일의 음성을 출력하기 위해서 씬에 존재하는 VoiceManager 오브젝트의 인스펙터 창에서 리스트를 하나 더 늘려 추가해주어야 한다.


1. 씬에 설정되는 과일의 수를 늘리거나 줄이려면,

    - 씬의 InitializeStage 클래스가 적용된 오브젝트의 인스펙터 창에서 mn_countFruits의 값을 변경해준다.

***
-------------
## README.md Change history
> ##### *2021.7.27 최대준 create*
