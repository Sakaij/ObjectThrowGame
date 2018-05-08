using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 対象の床に、触れた瞬間から削除のアニメーションを始めるクラス。削除アニメーションの仕様は、７秒後に、３秒間削除アニメーションを開始し、２回転させてオブジェクトを削除
/// </summary>
public class DestroyFloor : MonoBehaviour {
    public GameObject destroyFloor;
    public float destroyTime = 10;
    public float destroyAnimationStartTime = 7;
    public Vector3 desteoyRotation = new Vector3(30, 720, 0);
    private Vector3 perFlameRotation; //１フレームでオブジェクトを何度回転させるかの量
    private int destroyAnimationCount; //削除アニメーションを何回に分けて行うか、
    private float destroyAnimationTime;//削除アニメーションにかける時間


    private void Start()
    {
        destroyAnimationTime = destroyTime - destroyAnimationStartTime;
        destroyAnimationCount = Mathf.FloorToInt(destroyAnimationTime * 100);
        perFlameRotation = desteoyRotation / destroyAnimationCount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.tag = "Touched";
            StartCoroutine("DestroyAnimationCoroutine", collision.gameObject);
        }

    }


    /// <summary>
    /// 一回でも床に触れたオブジェクトは"Touched"のタグをつけ、一回のみ削除コルーチンを行うようにする
    /// </summary>
    /// <param name="targetObj"></param>
    /// <returns></returns>
    IEnumerator DestroyAnimationCoroutine(GameObject targetObj)
    {
        Vector3 perFlameScaleChange = targetObj.transform.localScale / destroyAnimationCount;
        yield return new WaitForSeconds(destroyAnimationStartTime);
        targetObj.transform.rotation = Quaternion.Euler(0, 0, 0); //３秒かけて　、new Vector3( 30 , 720, 0)回転させる
        Destroy(targetObj.GetComponent<Collider>());
        targetObj.GetComponent<Rigidbody>().useGravity = false;
        for(int i=0; i < destroyAnimationCount; i++)
        {
            targetObj.transform.Rotate(perFlameRotation);
            targetObj.transform.localScale -= perFlameScaleChange;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(targetObj);



    }







}
