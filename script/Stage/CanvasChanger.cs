using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 最初に呼び出されるのは何もないキャンバス
/// </summary>
public class CanvasChanger : MonoBehaviour {



    public static Canvas[] canvasArray;
    /// <summary>
    /// 何もないキャンバス
    /// </summary>
    public static Canvas nothingCanvas; 
    /// <summary>
    /// ゲームプレイ中のキャンバス
    /// </summary>
    public static Canvas playingCanvas;
    /// <summary>
    /// クリアした後のキャンバス
    /// </summary>
    public static Canvas clearedCanvas;

    private void Start()
    {

        nothingCanvas = CanvasFind("NothingCanvas");
        playingCanvas = CanvasFind("PlayingCanvas");
        clearedCanvas = CanvasFind("ClearedCanvas");
        canvasArray = new Canvas[] {nothingCanvas, playingCanvas, clearedCanvas };

        CanvasSwitch(1);
        

    }


    /// <summary>
    /// 引数の数字によってオンにするキャンバスを変える。１ならば、何もないキャンバス。２ならばプレイ中のキャンバス。3ならばクリア後のキャンバス
    /// </summary>
    /// <param name="canvasNumber"></param>
    public static void CanvasSwitch(int canvasNumber)
    {
        switch (canvasNumber) {
            case 1:
                CanvasOn(nothingCanvas);
                break;
            case 2:
                CanvasOn(playingCanvas);
                break;
            case 3:
                CanvasOn(clearedCanvas);
                break;


        }
    }









    private static void CanvasOn(Canvas targetCanvas)
    {
        foreach (Canvas offCanvas in canvasArray)
        {
            offCanvas.enabled = false;
        }


        targetCanvas.enabled = true;
    }
    private static Canvas CanvasFind(string tag)
    {
        return GameObject.FindWithTag(tag).GetComponent<Canvas>();
    }
}
