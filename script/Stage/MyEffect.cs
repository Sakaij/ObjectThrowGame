using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEffect : MonoBehaviour {
    private GameObject breakObject;
    private static float breakEffectYForce = 2; //破壊用のエフェクトのｙ軸にフォースする量


    /// <summary>
    /// 破壊エフェクトに使うオブジェクトをセットする。
    /// </summary>
    /// <param name="breakObject"></param>
    public void BreakObjectSet(GameObject breakObject)
    {
        this.breakObject = breakObject;

    }


    /// <summary>
    /// 指定座標に破壊エフェクトを開始する。
    /// </summary>
    public void BreakEffect(Vector3 effectPosition)
    {
        int zMark = 1;
        for (int count = 0; count < 100; count++)
        {
            //x,z 合わせて、１０になるようにランダムにフォース
            float randomX = Random.Range(-10.0f,10.0f); //11は含まれない。
            float randomZ = Mathf.Abs(10 - randomX) * zMark;
            zMark *= -1;
            GameObject obj = Instantiate(breakObject, effectPosition, Quaternion.identity) as GameObject;
            obj.GetComponent<Rigidbody>().AddForce(randomX, breakEffectYForce, randomZ);
            
        }
    }
}
