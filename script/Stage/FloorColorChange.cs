using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColorChange : MonoBehaviour {
    public GameObject floor;
    private Color changeR = new Color(0.1f, 0, 0, 0);
    private Color changeG = new Color(0, 0.01f, 0, 0);
     Color color;


    private void Start()
    {
        color = floor.GetComponent<Renderer>().material.color;
        StartCoroutine("ColorChange");
    }

    /// <summary>
    /// 色を変えるためのコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator ColorChange()
    {

        int singR = -1;
        int singG = -1;
        int count = 0;
        while(count == 0){
            for (int gCount = 0; gCount < 5; gCount++) {

                Material  mat= GetComponent<Renderer>().material;
                for (int g = 0; g < 10; g++) //ここで、すべて呼び出して、0.2秒で、0.01程度変え、1秒で0.05
                {
                    yield return new WaitForSeconds(0.2f);
                    //floor.GetComponent<Renderer>().material.
                        mat.color +=(changeR*singR / 10);
                }

                for (int r = 0; r < 50; r++)
                {
                    yield return new WaitForSeconds(0.2f);
                    //floor.GetComponent<Renderer>().material.
                        mat.color +=changeG * singG;
                }
                singG *= -1;
            }
            singR *= -1;
        }
    }
}
