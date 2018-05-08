using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マウスのボタンの処理をするクラス
/// </summary>
class ClickManager : MonoBehaviour
    
{
    /*
    ObjectForce ins;
    public Transform throwPoint; 
    public Transform spawnPoint;
    public GameObject spawnObject;
    public Camera mainCamera;

    private Vector3 firstPosition; //最初に画面判定があった座標
    

    


    bool objectClickingBool; //対象のオブジェクトをクリック中かどうか

    private void Start()
    {
        ins = new ObjectForce(spawnObject, spawnPoint, throwPoint, mainCamera);
        ins.ObjectSpawn();

    }


    /// <summary>
    ///　インスタンスのプロパティをここで初期化
    /// </summary>


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ins.Throwable(Input.mousePosition))
            {
                firstPosition = Input.mousePosition;
                objectClickingBool = true;
            }
        }

        if (Input.GetMouseButtonUp(0) & objectClickingBool)
        {
            ins.Force(ins.objArray[ins.NowObjectNumber()], firstPosition, Input.mousePosition);
            objectClickingBool = false;
        }
    }
    */

    /// <summary>
    /// オブジェクトをクリックしているかどうか
    /// </summary>
    public bool clickingBool;
    /// <summary>
    /// オブジェクトが投げてもいい状態かどうか
    /// </summary>
    public bool throwBool;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {


            
        }
        
    }


}
