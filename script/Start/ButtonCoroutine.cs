using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;

/// <summary>
/// スタート画面の、ステージセレクトの画面で解放されているステージの中で数が一番でかいステージのボタンを光らせる
/// </summary>
public class ButtonCoroutine : SingletonMonoBehaviour<ButtonCoroutine> {

    public static Color continueButton = new Color(1, 0, 1, 1); //続き（未クリア）のボタンの色

    public void ButtonAppel(Button target)
    {
        StartCoroutine(ContinueStage(target));
    }

    IEnumerator ContinueStage(Button target)
    {
        target.image.color = continueButton;
        int wC = 0;
        while (wC == 0)
        {
            for (int count = 0; count < 50; count++)
            {
                yield return new WaitForSeconds(0.01f);
                
                target.image.color -= new Color(0, 0, 0, 0.02f);

            }


            for (int count = 0; count < 50; count++)
            {
                yield return new WaitForSeconds(0.01f);
                target.image.color += new Color(0, 0, 0, 0.02f);
            }

        }

    }
}
