/*
 * - Name : InitializePuzzle.cs
 * - Writer : 최대준
 * - Content : Text to Speech Class로, 디자인 패턴은 싱글톤 패턴을 이용하였다. 조금 무거운 클래스 이므로 하나의 인스턴스를 다른 오브젝트 클래스에서 재사용할 수 있도록 하기 위해서 싱글톤 패턴을 이용하였다.
 * - History -
 * 2021-07-28 : 첫 구현.
 * 2021-07-29 : 주석 처리.
 *
 * - InitializePuzzle Member Variable 
 *
 * mf_originWidth, mf_originHeight : 퍼즐 윈도우 즉 퍼즐이 존재하는 창의 너비, 높이를 나타낸다.
 * mrt_parentSize : 퍼즐 윈도우 크기를 RectTransform클래스 형태로 표기한다.
 * mgo_SetPuzzle : 퍼즐의 프리팹을 담는 변수이다.
 * mglg_AnsGridSize : 퍼즐은 그리드 형태로 윈도우 창에 존재하게 되므로 그때 퍼즐의 크기를 조절한다.
 * mglg_ProbGridSize : 퍼즐을 그리드 형태로 있게 해주는 컴포넌트이다.
 * mspl_slicedPuzzle : SpriteSlice.sliceSprite() 함수는 반환값이 Sprite[] 이므로 반환값을 저장하는 변수이다.
 * mtex2_slicePuzzle : 짜를 이미지를 인스펙터 창에서 담아온다.
 
 * - InitializePuzzle Member Function
 *
 * Awake() : 씬이 시작될 때, 퍼즐 윈도우 사이즈에 따라 그리드 세팅 값과 그 안에 존재하는 퍼즐들을 생성하는 함수이다.  
 * setChild() : 퍼즐 윈도우 안에 퍼즐 게임오브젝트를 생성한다. 이때 퍼즐 윈도우의 자식 오브젝트로 들어가게 만든다.
 *
 */

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
/// <summary>
/// 본 클래스는 퍼즐 스테이지의 초기화를 담당한다. 퍼즐 조각을 씬에 생성하게 된다.
/// </summary>
public class InitializePuzzle : MonoBehaviour {
    private float mf_originWidth, mf_originHeight;
    private RectTransform mrt_parentSize;
    public GameObject mgo_SetPuzzle;
    private GridLayoutGroup mglg_AnsGridSize;
    private GridLayoutGroup mglg_ProbGridSize;
    private Sprite[] mspl_slicedPuzzle;
    public Texture2D mtex2_slicePuzzle;
    private int[] myArray;
    /// <summary>
    /// 윈도우 사이즈에 맞춰 그리드 셀 사이즈를 조절하고, 그 그리드에 맞춰 퍼즐 조각을 생성해준다.
    /// </summary>
    void Awake() {
        int col = 0;
        int row = 0;
        mrt_parentSize = transform.GetChild(1).gameObject.GetComponent<RectTransform>();
        mglg_AnsGridSize = transform.GetChild(1).gameObject.GetComponent<GridLayoutGroup>();
        mglg_ProbGridSize = transform.GetChild(2).gameObject.GetComponent<GridLayoutGroup>();

        mf_originWidth = mrt_parentSize.rect.width;
        mf_originHeight = mrt_parentSize.rect.height;

        int nSelectPuzzleRanNum = Random.Range(0,3);
        switch(nSelectPuzzleRanNum) {
            case 0:
                col = 2;
                row = 2;
                break;
            case 1:
                col = 2;
                row = 3;
                break;
            case 2:
                col = 3;
                row = 3;
                break;
        }

        GameObject.Find("CheckPuzzle").GetComponent<Puzzle_CheckPuzzle>().mn_AnswerPuzzle = col * row;

        mspl_slicedPuzzle = SpriteSlice.sliceSprite(col, row, mtex2_slicePuzzle);
        myArray = Enumerable.Range(0, col * row).ToArray();
        Shuffle.ShuffleArray<int>(myArray);

        mglg_AnsGridSize.cellSize = new Vector2((mf_originWidth - 100) / col, (mf_originHeight - 100) / row);
        mglg_ProbGridSize.cellSize = new Vector2((mf_originWidth - 100) / col, (mf_originHeight - 100) / row);
    
        mglg_AnsGridSize.constraintCount = col;
        mglg_ProbGridSize.constraintCount = col;
        setChild(col * row, transform.GetChild(1), true);
        setChild(col * row, transform.GetChild(2), false);
    }

    /// <summary>
    /// 퍼즐 윈도우 오브젝트의 자식 오브젝트로써 퍼즐 조각을 생성하는 함수이다.
    /// </summary>
    /// <param name="nChild">해당 tParent 오브젝트 아래로 몇개의 자식 오브젝트를 생성할 지를 지정해주면 된다.</param>
    /// <param name="tParent">자식 오브젝트를 생성할 부모 오브젝트를 지정해주면 된다.</param>
    /// <param name="bClassifyType">정답 퍼즐인지, 플레이어가 조종하는 퍼즐인지에 맞춰 오브젝트 세팅을 변경한다.</param>
    void setChild(int nChild, Transform tParent, bool bClassifyType) {
        Sprite[] tempSprite = new Sprite[mspl_slicedPuzzle.Length];
        Debug.Log(tempSprite[0]);
        for (int i = 0; i < nChild; i++) {
            GameObject temp = GameObject.Instantiate(mgo_SetPuzzle, tParent);
            temp.GetComponent<Puzzle_Matching_Puzzle>().mn_PuzzleId = i;
            // true is AnswerPuzzle
            if (bClassifyType) {
                temp.GetComponent<Image>().sprite = mspl_slicedPuzzle[i];
                temp.GetComponent<Puzzle_Matching_Puzzle>().mb_classifyWhetherAns = bClassifyType;
                Color tempColor = temp.GetComponent<Image>().color;                          
                tempColor.a = 0.1f;
                temp.GetComponent<Image>().color = tempColor;
            }
            else {
                temp.GetComponent<Puzzle_Matching_Puzzle>().mn_PuzzleId = myArray[i];
                temp.GetComponent<Image>().sprite = mspl_slicedPuzzle[myArray[i]];
            }
        }
    }
}
