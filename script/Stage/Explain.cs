using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CameraUtil;
using ManagerRelation;
/// <summary>
/// 操作方法を説明するクラス
/// </summary>
public class Explain : MonoBehaviour {

    public Text playText;
    public Text lifeExplanationText;
    public Text untilClearExplanationText;
    public GameObject goal;
    public Button chaceModeButton;
    public Button fixCameraChangeButton;
    public Image finger; //指示用の指のSprite
    private float moveDistance = -50; //上下運動の一定距離
    private bool calledBool; //一回でも呼ばれいてるかどうか
    private bool fingerWasMoved=true; //　指が指定のポジションにいきついたかどうかのブール
    private Vector3 endRotation = new Vector3(200, 0, 0);
    private Vector3 firstPosition;
    /// <summary>
    /// ０ならば、未フォース段階　１ならば、初フォース後、追跡ボタン押す前　２、ならば、追跡ボタン押した後、フォース前、３、追跡のフォース後
    /// </summary>
    public static int  explainedStage=0;
    private Coroutine fingerMove;
    private Vector3 scaleChangeAmount = new Vector3(0.2f, 0.2f, 0);
    private bool moveFingerBool = false;
    private bool firstBackBool = true; //一回だけ、最初の位置に戻す処理を行いたいので、
    private bool lifeDicrementBool; //ライフを減らすかどうかを決める


    /// <summary>
    /// 指を上下に動かす(投げるように勧める）
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveFinger()
    {
        
        int flameAmount = 100;
        float perMove = moveDistance / flameAmount;
        Vector3 defPosition = finger.transform.position;
        UIActiveManager.UIApper(finger.gameObject);
        while(moveFingerBool)
        {
            
            for(int count = 0; count < flameAmount; count++)
            {
                if(moveFingerBool)
                {
                    yield return new WaitForSeconds(0.01f);
                    finger.transform.position += new Vector3(0, perMove, 0);
                }
            }
            
            yield return new WaitForSeconds(1);
            if (moveFingerBool)
            {
                finger.transform.position = defPosition;

            }
        }
    }

    /// <summary>
    /// 指を大きさを変えて目立たせる
    /// </summary>
    /// <returns></returns>
    IEnumerator FingerScaleChange()
    {
        Vector3 firstScale = finger.transform.localScale;
        int flame = 50;
        //追跡ボタンを押させるのか、固定カメラボタンを押させるのかどちらかなら
        while (explainedStage == 1 || explainedStage == 3)
        {

            //スケールを上げる
            for(int upCount =0; upCount < flame; upCount++)
            {
                finger.transform.localScale += scaleChangeAmount / flame;
                yield return new WaitForSeconds(0.01f);
            }
            //スケールを下げる
            for(int downCount =0; downCount < flame; downCount++)
            {
                finger.transform.localScale -= scaleChangeAmount / flame;
                yield return new WaitForSeconds(0.01f);
            }
            finger.transform.localScale = firstScale;
        }

        finger.transform.localScale = firstScale;
        //これが終わるということは、プレイヤーが追跡ボタンを押したことになるのでポジションをもとに戻す。
    }


    /// <summary>
    /// 指定の座標に時間をかけて移動する
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    IEnumerator FingerPositionChange(Vector3 targetPosition)
    {
        int flame = 100;
        fingerWasMoved = false;
        Vector3 fingerEndPosition = targetPosition;
        Vector3 nowFingerPosition = finger.transform.position;
        Vector3 distance = fingerEndPosition - nowFingerPosition;
        for(int count = 0; count < flame; count++)
        {
            yield return new WaitForSeconds(0.01f);
            finger.transform.position += distance / flame;
        }
        finger.transform.position = targetPosition;
        fingerWasMoved = true;
        //追跡ボタンを押させる処理
        if (explainedStage == 1)
        {

            StartCoroutine(FingerScaleChange());
            chaceModeButton.enabled = true;

        }
        //追跡ボタンをおした上での、フォースを勧める処理
        if(explainedStage == 2)
        {
            moveFingerBool = true;
            yield return new WaitForSeconds(1);

            StartCoroutine(MoveFinger());
 
        }
        //メインバックを押させる処理
        if(explainedStage == 3)
        {
            StartCoroutine(FingerScaleChange());
        }

    }



    /// <summary>
    /// ゴールできる角度にする
    /// </summary>
    /// <returns></returns>
    IEnumerator GoalRotationChange()
    {
        Vector3 rotationAmount =MyMath.RotationCalcualateRange180(endRotation, goal.transform.rotation.eulerAngles);
        int flameAmount = 200;
        for (int count = 0; count < flameAmount; count++)
        {
            goal.transform.Rotate(rotationAmount / flameAmount);
            yield return new WaitForSeconds(0.01f);
        }

        goal.transform.rotation = Quaternion.Euler(endRotation);
        lifeDicrementBool = true;
        UIActiveManager.UIApper(untilClearExplanationText.gameObject, lifeExplanationText.gameObject);
        yield return new WaitForSeconds(10);

        UIActiveManager.UIDisapper(untilClearExplanationText.gameObject, lifeExplanationText.gameObject);
        yield return new WaitForSeconds(2);
        UIActiveManager.UIApper(playText.gameObject);
        yield return new WaitForSeconds(4);
        UIActiveManager.UIDisapper(playText.gameObject);

    }


    

    public int stageNumber; //このステージの番号
    public GameObject spawnObject;
    public Transform spawnPoint;
    public Transform throwPoint;
    public Camera mainCamera; //オブジェクトの中心点を取得するためのカメラ
    public float untilSpawnTime = 2; //オブジェクトがフォースされてから、次のオブジェクトを生成するまでにかける時間
    private Dictionary<int, GameObject> objArray = new Dictionary<int, GameObject>();
    /// <summary>
    /// ここの値をいじっていいのは、コルーチン"Spawn"のみ
    /// </summary>
    private int spawnCount = 0;

    //クリック系
    /// <summary>
    /// 特定のオブジェクトをクリック中かどうか
    /// </summary>
    private bool clickingBool;
    /// <summary>
    /// 今オブジェクトが投げられる状態にあるかどうか
    /// </summary>
    private bool throwBool;
    /// <summary>
    /// タッチまたは、クリック判定をするかどうか
    /// </summary>
    private bool judgeBool;

    //フェードイン系
    public float feedInTime = 1;
    public int feedInRotation90Count = 2;//一番ちょうどいい値になるのがこの値

    private Vector3 feedInEndRotation; //フェードイン時オブジェクトがたどり着く最終角度。
    private int feedInCount;
    private Vector3 feedInPerFlameMove;
    private Vector3 feedInPerFlameRotation;

    //フォース関連
    /// <summary>
    ///対象のオブジェクトをタッチしたうえで、最初にタッチし座標
    /// </summary>
    private Vector3 firstClickPoint;
    public int xPowerControler = 10000;
    public int yPowerControler = 50000;
    public int zPowerControler = 2000;
    public AudioClip cantForceSE; //オブジェクトが投げられない（ライフがない）時にならす音源
                                  /// <summary>
                                  /// だいたいの値は１０００ほどで
                                  /// </summary>
    public int rotationControler = 10000;
    private Vector3 objectCenterPoint;
    private Vector3 forcePower;
    private Vector3 rotationPower;
    private float zPower8_10Magnificant = 1.5f; //ｚ軸のフォース値が、８～１０の時に、掛ける倍率の変数
    private int zPower; //　1 ～　10までの値

    //スケールアップ関連
    public float zPower1_7PerTime = 0.05f;  //１～８までで、一つ上がるまでに掛ける時間
    public float zPower8_10PerTime = 0.03f;// 8 ～　１０の終わりまで、１つ上がるまでにかける時間
    private Vector3 zPowerPerScaleUpAmount = new Vector3(0.03f, 0.03f, 0.03f); //zフォース力が、１つあがるたびに上げるスケールの量（固定）
                                                                               /// <summary>
                                                                               /// 今投げる位置にあるオブジェクトにスケールチェンジ処理を行うかどうかのbool
                                                                               /// </summary>
    private bool scaleChangeBool = false;

    //追跡カメラ関連
    private bool trackMode = false;
    private float cameraBackTime = 1;　//追跡カメラが、デフォルトの座標に戻るまでの時間 



    //Z軸へのフォースのテキスト関連
    public static string zPowerTextFirst = "Power : "; //ローカライズのため分けている。初期値、英語

    //UI系(表示非表示をこのクラスで切り替えするため）
    public Button trackButton;
    public Button mainBackButton;
    public Button anotherCameraButton;
    public Button rightCameraButton;
    public Button leftCameraButton;
    public Text zPowerText;

    //顔用の変数
    private static int untilBlinkTime = 10;
    private static float blinkTime = 0.5f;
    private static string baseFaceName = "baseFace";
    private static string blinkFaceName = "blinkFace";



    /// <summary>
    /// この関数は、ハードによって、クリックか、タッチ判定なのかを変える。
    /// </summary>
    public void Update()
    {
        //メインカメラに切り替わった時点で、コルーチンを開始したいので監視しておく。
        if ((MyCameraSwitch.nowCamera == 1) && (!calledBool))
        {
            UIActiveManager.UIApper(finger.gameObject);
            moveFingerBool = true;
            StartCoroutine(MoveFinger());
            calledBool = true;
        }
        if (Input.GetMouseButtonDown(0) && !clickingBool && throwBool && MyCameraSwitch.nowCamera == 1 && fingerWasMoved)
        {
            if (ObjectRayBool(Input.mousePosition))
            {
                firstClickPoint = MyCameraSwitch.mainCamera.ScreenToViewportPoint(Input.mousePosition); //ビューポート座標に変換(
                clickingBool = true;
                StartCoroutine("ObjectScaleChange", objArray[spawnCount]);

            }
        }




        //オブジェクトのクリックが終わった判定
        if (Input.GetMouseButtonUp(0) & clickingBool)
        {

            Force(objArray[spawnCount], firstClickPoint, mainCamera.ScreenToViewportPoint(Input.mousePosition));
            clickingBool = false;
        }

        //追跡ボタンが押された時の処理
        if(explainedStage == 2)
        {
            //指の最初の座標に移動する
            if (firstBackBool)
            {
                StartCoroutine(FingerPositionChange(firstPosition));
                firstBackBool = false;
            }
        }
        if(explainedStage == 4)
        {
            UIActiveManager.UIDisapper(finger.gameObject);
            StartCoroutine(GoalRotationChange());
            anotherCameraButton.enabled = true;
            chaceModeButton.enabled = true;
            explainedStage++;
        }

    }


    public void Start()
    {
        Initialize();
        SaveManager.SaveExplanationed();
        firstPosition = finger.transform.position;
        chaceModeButton.enabled = false;
        fixCameraChangeButton.enabled = false;
        FeedInInitializer();
        ObjectSpawn();
    }

    private void Initialize()
    {
        explainedStage = 0;
    }


    /// <summary>
    /// 引数のオブジェクトに力を加える。第二引数は、最初に検知した場所、第三引数は、最後に検知した場所
    /// </summary>
    public void Force(GameObject targetObject, Vector3 startPosition, Vector3 endPosition)
    {
        if (Life.REST_LIFE > 0 &&lifeDicrementBool)
        {
            Life.Instance.ReduceLifeOne();
        }
        if (Life.REST_LIFE <= 0 && lifeDicrementBool)
        {
            SoundPlayer.Instance.PlayOneShot(SoundPlayer.cantForceSound);
            return;
        }

        //初フォース
        if(explainedStage == 0)
        {
            moveFingerBool = false;
            StartCoroutine(FingerPositionChange(chaceModeButton.gameObject.transform.position - new Vector3(0,30,0)));
            explainedStage++;
        }
        //追跡ボタンの押した後の初フォース
        if(explainedStage == 2)
        {
            moveFingerBool = false;
            StartCoroutine(FingerPositionChange(mainBackButton.transform.position - new Vector3(0, 30, 0)));
            explainedStage++;
        }
        targetObject.GetComponentInChildren<Collider>().isTrigger = false;
        ForcePowerDecide(startPosition, endPosition);
        GravitySwitch(targetObject);
        if (ExplainChaceModeButton.ChaceModeBool())
        {
            StartCoroutine("ObjectTrack", objArray[spawnCount]);
        }
        targetObject.GetComponent<Rigidbody>().AddForce(forcePower);
        targetObject.GetComponent<Rigidbody>().AddTorque(rotationPower);
        SoundPlayer.Instance.PlayOneShot(SoundPlayer.forceTimeSound);
        throwBool = false;
        StartCoroutine("Spawn");
    }





    /// <summary>
    /// オブジェクトのスポーン、この関数を呼び出すと、オブジェクトのフェードインも行う。
    /// </summary>
    public void ObjectSpawn()
    {
        objArray.Add(spawnCount, Instantiate(spawnObject, spawnPoint.position, Quaternion.Euler(180, 0, 0)) as GameObject);
        StartCoroutine("FeedIn", objArray[spawnCount]);


    }


    /// <summary>
    /// クリック時にフォース力に応じて、オブジェクトのスケール、またはz軸へのフォース力を変えるコルーチン
    /// </summary>
    /// <param name="targetObj"></param>
    /// <returns></returns>
    IEnumerator ObjectScaleChange(GameObject targetObj)
    {
        Vector3 defaultScale = targetObj.transform.localScale;

        //クリック中ならまた一から戻す
        while (clickingBool)
        {
            zPower = 1;
            ZPowerText text = new ZPowerText();
            zPowerText.text = zPowerTextFirst + zPower;
            //１から２　２から３　３から４　４から５　５から６　６から７　７から８
            for (int pow = 0; pow < 7; pow++)
            { //1 ～ 8になるまでが 0.35秒
                //このfor文が終わるまで、0.5秒
                for (int scale = 0; scale < 5; scale++) //
                {
                    targetObj.transform.localScale += zPowerPerScaleUpAmount / 5;
                    yield return new WaitForSeconds(0.01f);
                }
                if (clickingBool)
                {

                    zPower++;
                    zPowerText.text = zPowerTextFirst + zPower;
                }
                else
                {
                    break;
                }


            }
            if (clickingBool)
            {
                // ３回呼び出したい
                for (int pow = 0; pow < 3; pow++)
                {
                    for (int scale = 0; scale < 3; scale++)
                    {
                        targetObj.transform.localScale += zPowerPerScaleUpAmount / 3;
                        yield return new WaitForSeconds(0.01f);
                    }
                    if (clickingBool)
                    {
                        zPower++;
                        zPowerText.text = zPowerTextFirst + zPower;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            targetObj.transform.localScale = defaultScale;
        }
    }


    /// <summary>
    ///オブジェクトをフェードインさせるためのコルーチン(オブジェクトの中心点も求める）
    /// </summary>
    IEnumerator FeedIn(GameObject targetObj)
    {
        for (int i = 0; i < feedInCount; i++)
        {
            targetObj.transform.position += feedInPerFlameMove;
            targetObj.transform.Rotate(feedInPerFlameRotation);
            yield return new WaitForSeconds(0.01f);
        }
        targetObj.transform.position = throwPoint.position;
        //rotationは、顔Spriteの問題があるので、回転数に応じて最終的な角度を決める
        targetObj.transform.rotation = Quaternion.Euler(feedInEndRotation);

        CenterDecide(targetObj);
        throwBool = true;
    }




    /// <summary>
    /// オブジェクトを生成するまで時間を計測する。時間が来たらスポーン
    /// </summary>
    /// <returns></returns>
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(untilSpawnTime);
        spawnCount++;
        ObjectSpawn();

    }


    /// <summary>
    /// オブジェクトの追跡をする。(追跡カメラに変え、終わるとメインカメラに戻す)
    /// </summary>
    /// <returns></returns>
    IEnumerator ObjectTrack(GameObject targetObject)
    {
        MyCameraSwitch.CameraChanger(4);//追跡カメラをON
        UIActiveManager.UIApper(mainBackButton.gameObject);//メインカメラに戻るためのボタンを表示
        UIActiveManager.UIDisapper(anotherCameraButton.gameObject); //左右の固定カメラに移動するためのボタンを非表示
        Vector3 diffrent = MyCameraSwitch.trackingCameraDefoltPosition - throwPoint.position;

        while (ExplainChaceModeButton.ChaceModeBool() && targetObject != null)
        {

            MyCameraSwitch.trackingCamera.transform.position = targetObject.transform.position + diffrent;
            yield return null;
        }


        Vector3 cameraPerFlameMove = (MyCameraSwitch.trackingCameraDefoltPosition - MyCameraSwitch.trackingCamera.transform.position) / 100;
        for (int count = 0; count < 100; count++) //ここからは、戻るための処理
        {
            yield return new WaitForSeconds(0.01f);
            MyCameraSwitch.trackingCamera.transform.position += cameraPerFlameMove;
        }
        MyCameraSwitch.CameraChanger(1);
        UIActiveManager.UIDisapper(mainBackButton.gameObject); //メインカメラに戻るためのボタンを非表示
        UIActiveManager.UIApper(anotherCameraButton.gameObject); // 固定カメラに遷移するためのボタンを表示
        //追跡モードのボタンのテキストが、ONの状態なのであれば、追跡モードかどうかをONにしておく
        if (ExplainChaceModeButton.trackButtonBool)
        {
            ExplainChaceModeButton.ChaceModeON();
        }


    }



    /// <summary>
    /// 第一引数に、最初に検知した場所、第二引数に、最後に検知した場所を入れると、投げる力と、回転力を設定する。ビューポート座標
    /// </summary>
    /// <param name="firstPosition"></param>
    /// <param name="endPosition"></param>
    private void ForcePowerDecide(Vector3 firstPosition, Vector3 endPosition)
    {
        //ｘとｙのビューポート座標の差は、ともに平均、0.1ほど、最大0.2ほどになる
        //ビューポート座標は、左下が、基準となる。
        float xRotation = (firstPosition.y - endPosition.y) * rotationControler; //最初にタッチしたｘ座標が、離した場所より左だった場合正の数になる
        float yRotation = (firstPosition.x - endPosition.x) * rotationControler; //最初にタッチしたｙ座標が、離した場所より上だった場合正の数になる
        float xPower = (firstPosition.x - endPosition.x) * xPowerControler;
        float yPower = (firstPosition.y - endPosition.y) * yPowerControler;
        float zPower = this.zPower > 8 ? this.zPower * zPowerControler * zPower8_10Magnificant : this.zPower * zPowerControler;
        rotationPower = new Vector3(xRotation, yRotation, 0);
        forcePower = new Vector3(xPower, yPower, zPower);

    }


    /// <summary>
    /// フェードインの情報の初期化
    /// </summary>
    private void FeedInInitializer()
    {
        feedInCount = Mathf.FloorToInt(feedInTime * 100); //ここが２秒ならば２００になるはず ,ここはフレーム数の総量
        feedInPerFlameMove = (throwPoint.position - spawnPoint.position) / feedInCount; // z軸に２移動ならば、new Vector3(0 , 0 , 0.01)になるはず
        feedInPerFlameRotation = new Vector3((float)(90 * feedInRotation90Count) / (float)feedInCount, 0, 0); //２回転させたいならば、総回転数は720度なので、１フレームnew Vector3(2.6 , 0 , 0)になるはず
        feedInEndRotation = new Vector3(360 % (90 * feedInRotation90Count), 0, 0); //これが、回転総量
    }




    /// <summary>
    /// 引数のオブジェクトの中心点をビューポート座標で返す。
    /// </summary>
    private Vector3 CenterDecide(GameObject targetObject)
    {
        return mainCamera.WorldToViewportPoint(targetObject.transform.position);
    }


    /// <summary>
    /// 引数のオブジェクトの重力をONならばOFF, OFFならばONにする
    /// </summary>
    /// <param name="targetObj"></param>
    private void GravitySwitch(GameObject targetObj)
    {
        if (targetObj.GetComponent<Rigidbody>().useGravity)
        {
            targetObj.GetComponent<Rigidbody>().useGravity = false;
            return;
        }
        targetObj.GetComponent<Rigidbody>().useGravity = true;
    }



    /// <summary>
    ///　引数の座標に、今投げる状態のオブジェクトがあるかどうかを検知
    /// </summary>
    /// <returns></returns>
    private bool ObjectRayBool(Vector3 position)
    {
        Ray ray = MyCameraSwitch.mainCamera.ScreenPointToRay(position);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Player" && hit.collider.gameObject.transform.parent.gameObject == objArray[spawnCount])
            {
                return true;
            }

        }
        return false;
    }






}

