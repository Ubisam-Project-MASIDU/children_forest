using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSlice {

    /// <summary>
    /// 단순히 Texture 데이터와, 몇개로 나눌지 숫자를 매개변수로 주면 잘린 Sprite 데이터 리스트를 반환하는 함수.
    /// </summary>
    /// <param name="nCol">Texture2D 데이터의 width를 몇개로 자를지 지정해줌.</param>
    /// <param name="nRow">Texture2D 데이터의 height를 몇개로 자를지 지정해줌.</param>
    /// <returns>잘린 스프라이트 이미지를 반환.</returns>
    public static Sprite[] sliceSprite(int nCol, int nRow, Texture2D tex2dSrcTexture)
    {
        Sprite[] spriteTemp = new Sprite[nCol * nRow];
        
        for (int i = 0; i < nRow; i++) {
            for (int j = 0; j < nCol; j++) {
                spriteTemp[i * nCol + j] = Sprite.Create(tex2dSrcTexture, new Rect(j * (tex2dSrcTexture.width / nCol), i * (tex2dSrcTexture.height / nRow), tex2dSrcTexture.width / nCol, tex2dSrcTexture.height / nRow), Vector2.one * 0.5f);
            }
        }
        return spriteTemp;
    }
}
