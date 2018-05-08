using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using ManagerRelation;
using UnityEngine.UI;

/// <summary>
/// 広告再生ボタン用のクラス
/// </summary>
public class AdsStart : MonoBehaviour {

    public void AdsStartButton()
    {
        StartCoroutine(AdsCoroutine());
    }


    IEnumerator AdsCoroutine()
    {
        for(int count = 0;count < 20; count++)
        {
            yield return new WaitForEndOfFrame();
            if (AdsManage.Instance.CanPlay())
            {
                AdsManage.Instance.MovieStart();
                break;
            }
        }

        Debug.Log("インターネット環境に接続されていません");
    }
}
