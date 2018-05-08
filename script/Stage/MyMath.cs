using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// よく使う計算用のクラス
/// </summary>
public class MyMath : MonoBehaviour {


    /// <summary>
    /// 角度を-180から180の範囲に変換する
    /// </summary>
    /// <returns></returns>
    public static Vector3 RotationRangeChange180(Vector3 target)
    {

        return new Vector3(
            target.x > 180 ? target.x - 360 : target.x,
            target.y > 180 ? target.y - 360 : target.y,
            target.z > 180 ? target.z - 360 : target.z
            );
    }

    /// <summary>
    /// 今の角度に何度足したら、目的の角度になるのかを計算する関数
    /// </summary>
    /// <param name="targetRotation">目的の角度</param>
    /// <param name="nowRotation">今の角度</param>
    /// <returns></returns>
    public static Vector3 RotationCalcualateRange180(Vector3 targetRotation, Vector3 nowRotation)
    {
        Vector3 rotationAmout = targetRotation - nowRotation;
        return RotationRangeChange180(rotationAmout);
    }










}
