using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;

/// <summary>
/// つけたボタンに同じ動作をさせたいボタンのスクリプトに継承させる
/// </summary>
public class ButtonBehaviour : MonoBehaviour {

    /// <summary>
    /// ボタンを押すメソッドは、このメソッドをオーバーライドして使用(base.Button()も使用）
    /// </summary>
    public virtual void Button()
    {
        SoundPlayer.Instance.PlayOneShot(SoundPlayer.buttonSound);
    }

    public virtual void Button2()
    {
        SoundPlayer.Instance.PlayOneShot(SoundPlayer.buttonSound);
    }

    
}
