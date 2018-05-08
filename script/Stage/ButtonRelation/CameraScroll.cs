using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;
using CameraUtil;

/// <summary>
/// スクロールバーの値は、右なら0　左ならば１の値になる。
/// </summary>
public class CameraScroll : MonoBehaviour {

    /// <summary>
    /// z軸に、最大500動かせるようにするので、左の場合は、１のときにnew Vector3(0 , 0 , 500)   右の場合は、０のときにnew Vector3( 0, 0 ,500)になるようにする
    /// </summary>
    public float moveController;


    /// <summary>
    /// ここのスクロールバーの値は、右固定カメラ用ならば1。左固定カメラ用ならば0の値に設定
    /// </summary>
    public static Scrollbar scrollbar; 
    

    //スクロールは固定カメラが右のときに、１ならば

    public void Start()
    {
        scrollbar = GameObject.FindWithTag("FixCameraScrollbar").GetComponent<Scrollbar>();
        
    }



    public void Update()
    {

    }
    /// <summary>
    /// スクロールバーにアタッチする用
    /// </summary>
    public void Scroll()
    {
        //デフォルト値は、右固定カメラの場合は、１なので０に近づくにつれてｚ座標を大きくする
        if(LeftAndRightButton.cameraNumber == 1)
        {
            MyCameraSwitch.fixRightCamera.transform.position = new Vector3(MyCameraSwitch.fixRightCameraDefoltPosition.x, MyCameraSwitch.fixRightCameraDefoltPosition.y,MyCameraSwitch.fixRightCameraDefoltPosition.z +
                (1- scrollbar.value) * moveController);

        }
        //デフォルト値は、左固定カメラの場合は、０なので１に近づくにつれてｚ座標を大きくする
        else if(LeftAndRightButton.cameraNumber == 2)
        {
            MyCameraSwitch.fixLeftCamera.transform.position = new Vector3(MyCameraSwitch.fixLeftCameraDefoltPosition.x, MyCameraSwitch.fixLeftCameraDefoltPosition.y ,MyCameraSwitch.fixLeftCameraDefoltPosition.z +scrollbar.value* moveController);
        }
    }



}
