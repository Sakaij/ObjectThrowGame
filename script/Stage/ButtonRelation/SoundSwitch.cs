using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;
public class SoundSwitch : ButtonBehaviour {


        



    public void Switch()
    {
        if (SaveManager.GetSoundsBool())
        {
            SaveManager.SetSoundsBool(false);
            gameObject.GetComponentInChildren<Text>().text = SoundInitializeText.offText;
            base.Button();
            return;
        }
        else
        {
            SaveManager.SetSoundsBool(true);
            gameObject.GetComponentInChildren<Text>().text =SoundInitializeText.onText;
            base.Button();
        }

    }

}
