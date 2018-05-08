using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;
public class PageChanger : ButtonBehaviour {

    public static int pageNumber = 1;

    private int pageMax = 2;
    public Button[] buttonArray;

    private void Start()
    {
        pageNumber = 1;

    }

    /// <summary>
    /// ページを左にめくる
    /// </summary>
    public override void Button()
    {
        if (pageNumber > 1)
        {
            foreach (Button but in buttonArray)
            {
                int num = int.Parse(but.GetComponentInChildren<Text>().text);
                //テキストの数字を２０引く
                if (num == SaveManager.GetMostAdvanceStage())
                {
                    ButtonCoroutine.Instance.StopAllCoroutines();
                }
                but.GetComponentInChildren<Text>().text = (num - 20).ToString();
                SelectButton.ButtonColorChange(but);

            }
            pageNumber--;
            base.Button();
        }

    }
    
    /// <summary>
    /// ページを右にめくる
    /// </summary>
    public override void Button2()
    {
        if(pageNumber < pageMax)
        {
            foreach(Button but in buttonArray)
            {
                int num = int.Parse(but.GetComponentInChildren<Text>().text);
                if (num == SaveManager.GetMostAdvanceStage())
                {
                    ButtonCoroutine.Instance.StopAllCoroutines();
                }
                //テキストの数字を２０足す
                but.GetComponentInChildren<Text>().text = (int.Parse(but.GetComponentInChildren<Text>().text) + 20).ToString();
                SelectButton.ButtonColorChange(but);
            }
            pageNumber++;
            base.Button2();
        }
        
    }
}
