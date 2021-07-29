using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializePuzzle : MonoBehaviour {
    private float mf_originWidth, mf_originHeight;
    private RectTransform mrt_parentSize;
    public GameObject mgo_SetPuzzle;
    private GridLayoutGroup mglg_AnsGridSize;
    private GridLayoutGroup mglg_ProbGridSize;
    private Sprite[] mspl_slicedPuzzle;
    public Texture2D mtex2_slicePuzzle;

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
        mspl_slicedPuzzle = SpriteSlice.sliceSprite(col, row, mtex2_slicePuzzle);
        mglg_AnsGridSize.cellSize = new Vector2((mf_originWidth - 100) / col, (mf_originHeight - 100) / row);
        mglg_ProbGridSize.cellSize = new Vector2((mf_originWidth - 100) / col, (mf_originHeight - 100) / row);
    
        mglg_AnsGridSize.constraintCount = col;
        mglg_ProbGridSize.constraintCount = col;
        setChild(col * row, transform.GetChild(1), true);
        setChild(col * row, transform.GetChild(2), false);
    }

    void setChild(int nChild, Transform tParent, bool bClassifyType) {
        for (int i = 0; i < nChild; i++) {
            GameObject temp = GameObject.Instantiate(mgo_SetPuzzle, tParent);
            temp.GetComponent<Puzzle_Matching_Puzzle>().mn_PuzzleId = i;
            // true is AnswerPuzzle
            if (bClassifyType) {
                temp.GetComponent<Image>().sprite = mspl_slicedPuzzle[i];
                temp.GetComponent<Puzzle_Matching_Puzzle>().mb_classifyWhetherAns = bClassifyType;
                Color tempColor = temp.GetComponent<Image>().color;                          //흐렷던 퍼즐조각을 선명하게 변경
                tempColor.a = 0.1f;
                temp.GetComponent<Image>().color = tempColor;
            }
            else
                temp.GetComponent<Image>().sprite = mspl_slicedPuzzle[i];
        }
    }
}
