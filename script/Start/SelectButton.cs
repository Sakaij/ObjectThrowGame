using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;

public class SelectButton : MonoBehaviour
{



    public static Color unClrearedButton = new Color(0, 0, 0, 0); //未クリアのボタンの色
    public static Color clrearedButton = new Color(1, 1,1,1); //クリア済みのボタンの色
    public static Color continueButton = new Color(1,0,1,1); //続き（未クリア）のボタンの色

    public void Start()
    {
        ButtonColorChange(gameObject.GetComponent<Button>());
    }

    /// <summary>
    /// ボタンの色を変える
    /// </summary>
    public static void ButtonColorChange(Button target)
    { 
        if (SaveManager.GetMostAdvanceStage() >= int.Parse(target.GetComponentInChildren<Text>().text))
        {
            target.image.color = clrearedButton;

            return;
        }
        else if (SaveManager.GetMostAdvanceStage() + 1 == int.Parse(target.GetComponentInChildren<Text>().text))
        {
            ButtonCoroutine.Instance.ButtonAppel(target);
            return;
        }
        else if (SaveManager.GetMostAdvanceStage() < int.Parse(target.GetComponentInChildren<Text>().text))
        {
            target.image.color = unClrearedButton;
            return;
        }


    }

    //番号のステージへ遷移(いけないステージは、クリック不可能にする)
    public void SelectStage()
    {
        if (!(SaveManager.GetMostAdvanceStage() + 1 < int.Parse(gameObject.GetComponentInChildren<Text>().text)))
        {
            MySceneManager.StageSelect(int.Parse(gameObject.GetComponentInChildren<Text>().text));
        }
    }
    

    IEnumerator ContinueStage()
    {
        GetComponent<Button>().image.color = continueButton;
        int wC = 0;
        while (wC == 0)
        {
            for (int count = 0; count < 50; count++)
            {
                yield return new WaitForSeconds(0.01f);
                GetComponent<Button>().image.color -= new Color(0, 0, 0, 0.02f);

            }


            for (int count = 0; count < 50; count++)
            {
                yield return new WaitForSeconds(0.01f);
                GetComponent<Button>().image.color += new Color(0, 0, 0, 0.02f);
            }

        }

    }
}