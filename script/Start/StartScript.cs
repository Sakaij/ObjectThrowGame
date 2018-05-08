using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;


/// <summary>
/// アプリを起動してから一番最初の画面用のスクリプト
/// </summary>
public class StartScript : MonoBehaviour {

    public Button toLastStage; //最後にクリアした次のステージへ遷移
    public Button stageSelect; //ステージ数を選択できる画面へ移動
    public Camera startCamera; //オブジェクトを追跡するようのカメラ
    public Transform startPosition1;
    public Transform startPosition2;
    public Transform startPosition3;
    public Transform endPosition1;
    public Transform endPosition2;
    public Transform endPosition3;
    public GameObject sampleObject;
    public Transform spawnPoint;
    public Vector3 perFlameRotation = new Vector3(0, 0.1f, 0); //縦方向に強めに動かす
    public float cameraMoveTime = 10;
    public Canvas stageSelectCanvas;
    public Canvas startCanvas;


    private GameObject targetObject;
    private float flameTime = 0.01f;
    private int flameAmount ; 
    private Vector3 perFlameMove1;
    private Vector3 perFlameMove2;
    private Vector3 perFlameMove3;
    
    

    private void Start()
    {
        PerFlameMove();

        StartCoroutine("CameraMove");
        
    }




    IEnumerator CameraMove()
    {

        int counta = 0;
        while(counta ==0)
            for (int positionNumber = 1; positionNumber < 4; positionNumber++)
            {
                CameraPositionChange(positionNumber);
                Vector3 perMove = PerFlame(positionNumber);
                for (int count =0; count < flameAmount; count++)
                {
                    yield return new WaitForSeconds(flameTime);
                    startCamera.transform.position += perMove;
                startCamera.transform.LookAt(Vector3.zero);
                
                
                }
                

            }
        
    }


    private Vector3 PerFlame(int i)
    {
        switch (i)
        {
            case 1:
                return perFlameMove1;
            case 2:
                return perFlameMove2;

            case 3:
                return perFlameMove3;
            default:
                return perFlameMove1;
        }

    }
    private void CameraPositionChange(int i)
    {
        switch (i)
        {
            case 1:
                startCamera.transform.position = startPosition1.position;
                break;
            case 2:
                startCamera.transform.position = startPosition2.position;
                break;
            case 3:
                startCamera.transform.position = startPosition3.position;
                break;
        }

    }

    private Vector3 CameraDefaultPosition(int i)
    {
        switch (i)
        {
            case 1:
                return startPosition1.position;
            case 2:
                return startPosition2.position;
            case 3:
                return startPosition3.position;
            default:
                return startPosition1.position;
               
        }
    }


    private void PerFlameMove()
    {
        flameAmount = Mathf.FloorToInt(cameraMoveTime * 100);
        perFlameMove1 =(endPosition1.position - startPosition1.position) / flameAmount;
        perFlameMove2=(endPosition2.position - startPosition2.position) / flameAmount;
        perFlameMove3=(endPosition3.position - startPosition3.position) / flameAmount;

          

    }




    
}
