# 도형 맞추기
***
 - 작성 및 제작 : 이윤교
 - 언어 : C#
***
 - Update Log
     - 21.07.06 : 코드 초기 개발
     - 21.07.08 : 엔딩씬 연결 및 주석 처리 
     - 21.07.16 : 인코딩형식 UTF8로 수정
     - 21.07.19 : 해상도 변경에 따른 텍스트 크기 수정
     - 21.07.23 : 효과음 추가
     - 21.07.26 : bgm 추가
     - 21.07.27 : 주석 처리 수정 
***
 - 구동화면 및 내용

<img src = "https://user-images.githubusercontent.com/73592778/127114807-aec649e1-2afe-4eea-9d35-43291f5f937d.png" width="500" height="220">





    - 씬에 설정된 도형을 알맞은 위치에 드래그하여 빈 부분을 완성시킨다.
    - 남은 도형이 없다면 '참 잘했어요' 음성이 나오고 성공페이지가 나오면서 게임이 종료된다.
    

***


- Shape 구성 정보
  - Sprites
    - 씬에 사용되는 이미지들 및 효과음 저장
  - Scripts
    - Drag.cs : 물체 드래그 스크립트
    - Shape_CheckShape.cs : 도형을 다 맞췄는지 확인하고 end 씬 불러오는 스크립트
    - Shape_Matching_Shape.cs : 도형을 맞추는 스크립트
  - Scenes
    - match_shape_scene.unity : 씬 파일

***

-------------
## README.md Change history
> ##### *2021.7.27 이윤교 create*
