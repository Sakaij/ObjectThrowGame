using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;
public class BGMButtonSwitch : ButtonBehaviour {



    public override void Button()
    { 
        if (SaveManager.GetBGMBool())
        {
            SaveManager.SetBGMBool(false);
            gameObject.GetComponentInChildren<Text>().text = BGMTextInitialize.offText;
            BGMPlayer.Instance.StopBGM();
            base.Button();
            return;
        }
        else
        {
            SaveManager.SetBGMBool(true);
            gameObject.GetComponentInChildren<Text>().text = BGMTextInitialize.onText;
            BGMPlayer.Instance.StartBGM(BGMPlayer.stageBGM);
            base.Button();
        }

    }
}
