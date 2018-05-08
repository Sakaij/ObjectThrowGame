using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;


/// <summary>
/// 残り遊ぶことのできるライフをこのクラスを介して操作する。
/// </summary>
public class Life : SingletonMonoBehaviour<Life> {


    //残りライフ
    private static int restOfLife;
    /// <summary>
    /// 取得専用の残りライフ
    /// </summary>
    public static int REST_LIFE { get { return restOfLife; } }

    //残りライフ表示用のテキスト
    public Text lifeOfText;


    private void Start()
    {
        restOfLife = SaveManager.RestOfLife();
        lifeOfText.text = "=" + restOfLife;
    }

    

    /// <summary>
    /// ライフを一つ減らす。減らすことができなければfalseを返す。
    /// </summary>
    public bool ReduceLifeOne()
    {
        restOfLife--;
        lifeOfText.text ="="+ restOfLife.ToString();
        SaveManager.SaveLife(restOfLife);
        return false;
    }


    /// <summary>
    /// 指定したかずだけ、ライフを増やす。
    /// </summary>
    /// <returns></returns>
    public void IncrementLife(int upLife)
    {
        int now=SaveManager.RestOfLife();
        int after = now + upLife;
        SaveManager.SaveLife(after);
        restOfLife = SaveManager.RestOfLife();
        StartCoroutine(IncrementAnimation(upLife , now));
    }

    //Lifeを増やしたいときのアニメーション
    IEnumerator IncrementAnimation(int upLife, int nowLife)
    {
        int rest = nowLife;
        for (int count=1;count <= upLife; count++)
        {
            yield return new WaitForSeconds(0.02f);
            SoundPlayer.Instance.PlayOneShot(SoundPlayer.lifeUpSound);
            lifeOfText.text = "=" + ++rest+" +"+count;
            Debug.Log("今のライフは" + restOfLife);
        }
    }

    /// <summary>
    /// 開発者用なので絶対に呼び出してはならない
    /// </summary>
    public void returnTo0()
    {
        restOfLife = 0;
        lifeOfText.text = "=" + restOfLife.ToString();
        SaveManager.SaveLife(0);
    }



}
