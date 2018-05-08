using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;

/// <summary>
/// ボタンを一定時間の間に回転させる
/// </summary>
public class ButtonRotation : SingletonMonoBehaviour<ButtonRotation>{

    private int wait = 1;
    

	// Use this for initialization
	void Start () {
        StartCoroutine(Rotation());
	}


    IEnumerator Rotation()
    {
        int count = 0;

        while(count == 0)
        {
            yield return new WaitForSeconds(wait);
            for(int i = 0; i < 120; i++)
            {
                yield return new WaitForSeconds(0.01f);
                gameObject.GetComponent<Image>().transform.Rotate(new Vector3(0, 3, 0));
            }
            gameObject.GetComponent<Image>().transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        
    }
    public void RotStart()
    {
       
        
            gameObject.GetComponent<Image>().transform.rotation = Quaternion.Euler(Vector3.zero);
            StartCoroutine(Rotation());
        
    }

}
