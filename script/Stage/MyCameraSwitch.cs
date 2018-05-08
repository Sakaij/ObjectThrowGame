using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace CameraUtil
{
    public class MyCameraSwitch : MonoBehaviour
    {
        public static Camera fixLeftCamera;
        public static Camera fixRightCamera;
        public static Camera mainCamera;
        public static Camera trackingCamera;
        public static Camera feedInCamera;
        public static Vector3 mainCameraDefoltPosition;
        public static Vector3 fixLeftCameraDefoltPosition;
        public static Vector3 fixRightCameraDefoltPosition;
        public static Vector3 trackingCameraDefoltPosition;
        public static Vector3 feedInCameraDefoltPosition;
        private static Vector3 mainCameraDefoltRotation;
        private static Vector3 trackingCameraDefoltRotaition;
        /// <summary>
        /// １なら、メインカメラ、2なら固定右カメラ、３なら固定左カメラ、４ら追跡カメラ、５ならフェードインのカメラ
        /// </summary>
        public static int nowCamera=5;
       



        public static bool trackingBool;


        //フェードイン関連
        public Vector3 feedInCameraOverLookingEndPosition; // new Vector3(-105, 35, 115)を指定秒後に、new Vector3(-105, 35, 215)にする
        public Transform feedInCameraLookAtPosition;
        public float feedInCameraFeedInTime=1.5f;





        //初期化　＋　フェードインを始める
        void Start()
        {
            
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            mainCameraDefoltPosition = mainCamera.transform.position;
            mainCameraDefoltRotation = mainCamera.transform.rotation.eulerAngles;
            fixLeftCamera = GameObject.FindWithTag("LeftCamera").GetComponent<Camera>();
            fixLeftCameraDefoltPosition = fixLeftCamera.transform.position;
            fixRightCamera = GameObject.FindWithTag("RightCamera").GetComponent<Camera>();
            fixRightCameraDefoltPosition = fixRightCamera.transform.position;
            trackingCamera = GameObject.FindWithTag("TrackingCamera").GetComponent<Camera>();
            trackingCameraDefoltPosition = trackingCamera.transform.position;
            trackingCameraDefoltRotaition = trackingCamera.transform.rotation.eulerAngles;
            feedInCamera = GameObject.FindWithTag("FeedInCamera").GetComponent<Camera>();
            feedInCameraDefoltPosition = feedInCamera.transform.position;



            CameraChanger(5);
            StartCoroutine("CameraFeedIn");

        }



        /// <summary>
        /// インスタンス化を許さない
        /// </summary>
        private MyCameraSwitch()
        {
            
        }




        /// <summary>
        /// カメラを切り替える関数。引数に１を入れると、メインカメラ、２を入れると固定右カメラ、３を入れると、固定左カメラ、４を入れると追跡カメラ、５を入れると、フェードインカメラをONにし、
        /// 他のカメラをOFFにしたうえで、デフォルトの位置に戻す
        /// </summary>
        /// <param name="i"></param>
        public static void CameraChanger(int i)
        {
            switch (i)
            {
                case 1:
                    TargetCameraOn(mainCamera, fixLeftCamera, fixRightCamera, trackingCamera, feedInCamera);
                    nowCamera = 1;
                    CameraPositionBack();
                    break;
                case 2:
                    TargetCameraOn(fixRightCamera, mainCamera, fixLeftCamera, trackingCamera, feedInCamera);
                    nowCamera = 2;
                    CameraPositionBack();

                    break;
                case 3:
                    TargetCameraOn(fixLeftCamera, mainCamera, fixRightCamera, trackingCamera, feedInCamera);
                    nowCamera = 3;
                    CameraPositionBack();

                    break;
                case 4:
                    TargetCameraOn(trackingCamera, mainCamera, fixLeftCamera, fixRightCamera, feedInCamera);
                    nowCamera = 4;
                    CameraPositionBack();
                    break;

                case 5:
                    TargetCameraOn(feedInCamera, mainCamera, fixLeftCamera, fixRightCamera, trackingCamera);
                    nowCamera = 5;
                    CameraPositionBack();
                    break;

                default:
                    TargetCameraOn(mainCamera, fixRightCamera, fixLeftCamera, trackingCamera, feedInCamera);
                    CameraPositionBack();
                    break;

            }
        }


        //カメラの座標と角度をもとに戻す
        private static void CameraPositionBack()
        {
            mainCamera.transform.position = mainCameraDefoltPosition;
            fixRightCamera.transform.position = fixRightCameraDefoltPosition;
            fixLeftCamera.transform.position = fixLeftCameraDefoltPosition;
            trackingCamera.transform.position = trackingCameraDefoltPosition;
            feedInCamera.transform.position = feedInCameraDefoltPosition;
            mainCamera.transform.rotation = Quaternion.Euler(mainCameraDefoltRotation);
            trackingCamera.transform.rotation = Quaternion.Euler(trackingCameraDefoltRotaition);
        }

        //第一引数に、ONにしたいカメラを指定し、第二引数の可変長引数にOFFにしたいカメラを指定
        private static void TargetCameraOn(Camera targetCamera, params Camera[] offCamera)
        {
            foreach (Camera camera in offCamera)
            {
                camera.enabled = false;
            }

            targetCamera.enabled = true;
        }

        //ボタンのテキストを変更するメソッド
        public static void ButtonTextChange(string name, Button button)
        {
            button.GetComponentInChildren<Text>().text = name;
        }





        //フェードイン後、メインカメラにする
        IEnumerator CameraFeedIn()
        {

            //時間の内訳は、2割が全体のステージを見渡すのに使い、残りは、フェードインの時間に使う。(残りが、0.3秒になったところで向きの調節に時間をつかう)]
            float overLookingTime = feedInCameraFeedInTime * 0.5f; //２秒なら、0.4秒
            float feedInTime = feedInCameraFeedInTime * 0.5f; // ２秒なら、1.6秒
            float rotationControlTime = 0.2f;
            int overLookingAmount = Mathf.FloorToInt(overLookingTime * 100);
            int feedInAmount = Mathf.FloorToInt(feedInTime * 100);
            int rotationControlAmount =CallCountCalculater(rotationControlTime, 0.01f); //角度を調節するためのフレーム数

            Vector3 overLookingPerFlameMove = (feedInCameraOverLookingEndPosition - feedInCameraDefoltPosition) / overLookingAmount;
            Vector3 firstFeedInCameraRotation = RotationRangeChanger180(feedInCamera.transform.eulerAngles);//フェードインカメラの最初の角度。 new Vector3(0 , -60 , 0)
            Vector3 endRotation =RotationRangeChanger180(mainCamera.transform.eulerAngles); //ここには、new Vector3( 0, -5 , 0)が入る
            Vector3 perFlameMove = (mainCameraDefoltPosition - feedInCameraOverLookingEndPosition) / feedInAmount;


            feedInCamera.transform.LookAt(feedInCameraLookAtPosition);
            /*
            for ( int i =0; i < overLookingAmount; i++)
            {
                feedInCamera.transform.position += overLookingPerFlameMove;
                feedInCamera.transform.LookAt(feedInCameraLookAtPosition);
                yield return new WaitForSeconds(0.01f);
            }
            */

            feedInCamera.transform.position = feedInCameraOverLookingEndPosition;

            for (int i = 0; i < feedInAmount; i++)
            {

                if (feedInAmount - i <= rotationControlAmount) //　残りが0.1秒ならば調節に入る
                {
                    Vector3 nowFeedInCameraRotation = RotationRangeChanger180(feedInCamera.transform.eulerAngles); // -180 ～　180　に変換したフェードインカメラの今の角度
                    Vector3 controlPerRotation =RotationAmount180(nowFeedInCameraRotation, endRotation) / rotationControlAmount; //１回につき、調節する角度の量
                    for (int controlCount = 0; controlCount < rotationControlAmount; controlCount++)
                    {
                        feedInCamera.transform.position += perFlameMove;
                        feedInCamera.transform.Rotate(controlPerRotation);
                        yield return new WaitForSeconds(0.01f);
                    }
                    break;
                }
                feedInCamera.transform.position += perFlameMove;
                feedInCamera.transform.LookAt(feedInCameraLookAtPosition);
                yield return new WaitForSeconds(0.01f);

            }
            CanvasChanger.CanvasSwitch(2); // プレイ中のキャンバスに変える
            CameraChanger(1); // メインカメラに変換

        }

        /// <summary>
        /// 今起動しているカメラを取得する。
        /// </summary>
        /// <returns></returns>
        public static Camera GetNowCamera()
        {
            switch (nowCamera) {
                case 1:
                    return mainCamera;
                case 2:
                    return fixRightCamera;
                case 3:
                    return fixLeftCamera;
                case 4:
                    return trackingCamera;
                case 5:
                    return feedInCamera;
                default:
                    return mainCamera;
            }

        }

        /// <summary>
        /// フレームを何回呼び出せば、第一引数分の時間になるのかを計算
        /// </summary>
        /// <param name="time"></param>
        /// <param name="flame"></param>
        /// <returns></returns>
        private int CallCountCalculater(float time, float flame)
        {
            return Mathf.FloorToInt(time / flame);
        }



        /// <summary>
        /// 引数の角度（０～３６０）の範囲の角度を　-180 ～　180　の範囲に変更する
        /// </summary>
        /// <param name="targetRotation"></param>
        /// <returns></returns>
        private  Vector3 RotationRangeChanger180(Vector3 targetRotation)
        {
            return new Vector3(
                targetRotation.x > 180 ? targetRotation.x - 360 : targetRotation.x,
                targetRotation.y > 180 ? targetRotation.y - 360 : targetRotation.y,
                targetRotation.z > 180 ? targetRotation.z - 360 : targetRotation.z
                
                );
        }


        /// <summary>
        /// 第一引数に、最初の角度、第二引数に最終的な角度を入れることで、最短の距離で、どれだけ回転させればいいかを決める。(指定範囲は-180 ～ 180)
        /// </summary>
        /// <param name="firstRotation"></param>
        /// <param name="endRotation"></param>
        /// <returns></returns>
        private Vector3 RotationAmount180(Vector3 firstRotation , Vector3 endRotation)
        {
            return new Vector3(
                endRotation.x - firstRotation.x,
                endRotation.y - firstRotation.y,
                endRotation.z - firstRotation.z
                );
        }








    }
}
