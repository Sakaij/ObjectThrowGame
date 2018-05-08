using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;
using CameraUtil;


/// <summary>
/// ゴールの中の判定用コライダーにつけて使用。
/// </summary>
public class Goal : MonoBehaviour
{
    private int thisStageNumber;//このステージの番号
    public GameObject goalObj; //ゴール
    public string onGoalTag = "ObjectOnGoal"; //ゴールの判定をするかどうかを判定するためのタグ
    public string originTag = "Player";
    public string goaledTag = "goaledTag"; //判定を終了したオブジェクト用のタグ
    public Text clearCountText;
    public LineRenderer clrearedEffect; //クリアしたオブジェクトに、消滅エフェクトをつける
    public GameObject[] changeColorFloor; //色を変えたい対象の床の配列
    public Canvas clearedCanvas;
    public Image effectImage;

    private float effectImageChangeAmountA = 0.3f; //ゴールジに画面をちょっと光らせるみたいなエフェクトをいれたいので、その総量
    private string clearedText = "CLEAR";
    private int untilClearCount;
    private List<float> timerArray = new List<float>(); //時間用の配列
    private bool onGoalBool; //1つでもゴール判定中の、オブジェクトがあればTrue
    private bool elseObjectBool; //２つ以上ゴール判定が行われているかどうか
    private bool judgementBool; //テキスト反映対象のオブジェクトが判定対象かどうか
    private float smallestTime=5;//テキストに反映している時間
    private float changeColorAmount = 1;
    private int effectTime = 1;
    private int dumpTime = 5; //オブジェクトを床に捨てる動作
    



    private void Start()
    {
        thisStageNumber = MySceneManager.nowStageNumber;
        //セーブマネージャより、ゴールカウントが保存されていたらゴールカウントを設定する
        if(SaveManager.BoolUntilClrearHasKey(thisStageNumber)){
            untilClearCount = SaveManager.GetUntilClearCount(thisStageNumber);
            clearCountText.text = untilClearCount.ToString();
            return;
        }

        untilClearCount = ValueManager.UntilStageClear(thisStageNumber) ;
        clearCountText.text = untilClearCount.ToString();
        SaveManager.SaveUntilClearCount(thisStageNumber, untilClearCount);
        Debug.Log(PlayerPrefs.GetString(KeyManager.KEY_LANGUAGE));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("オブジェクトが入りました");
        if (other.gameObject.tag == onGoalTag)
        {
            return;
        }
        if (other.gameObject.tag == goaledTag)
        {
            return;
        }
        if (other.gameObject.tag == originTag)
        {
            other.gameObject.tag = onGoalTag;
            StartCoroutine("GoalCount", other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == onGoalTag)
        {
            other.gameObject.tag = originTag;
        }
    }

    

    
    /// <summary>
    /// 通り抜けによるゴール判定を防ぎたいので、１秒間タグがGoalであれば、ゴール判定対象になる。
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    IEnumerator GoalCount(GameObject target)
    {
        if (target.tag == onGoalTag) {
            yield return new WaitForSeconds(1);
            if (target.tag == onGoalTag)
            {
                if (untilClearCount > 1)
                {
                    untilClearCount--;
                    clearCountText.text = untilClearCount.ToString();
                    SaveManager.SaveUntilClearCount(thisStageNumber, untilClearCount);

                }
                else if (untilClearCount ==1) //ここで１ならば、もうクリアなので、クリア処理
                {
                    untilClearCount = ValueManager.UntilStageClear(thisStageNumber); //ステージごとのクリアまでに要するゴール数
                    clearCountText.text = untilClearCount.ToString();
                    SaveManager.SaveUntilClearCount(thisStageNumber,  untilClearCount);
                    //すでにこのステージがクリア済みならば、このキャンバス内で、クリアテキストを表示する
                    //初クリアの処理（クリアの用のキャンバスを表示）
                    if(SaveManager.GetMostAdvanceStage() < thisStageNumber)//最も進んでいるステージがこのステージならば、最新のステージとして保存する。
                    {
                        SaveManager.SetMostAdvanceStage(thisStageNumber);
                        CanvasChanger.CanvasSwitch(3);
                    }

                    //ステージ番号に見合ったライフ数をUPする。
                    int upLife = ValueManager.GetStageOfUpLife(thisStageNumber);
                    Life.Instance.IncrementLife(upLife);
                    StartCoroutine("DumpObject");

                }

                PlaySE();
                StartCoroutine("GoalEffect");
                StartCoroutine("ScreenEffect");
                StartCoroutine("CameraShake");
            }
        }
        
    }


    /// <summary>
    ///ゴール時のエフェクト（クリア時ではない）。指定秒でオブジェクトの色を変える。
    /// </summary>
    /// <param name="targetObject"></param>
    /// <returns></returns>
    IEnumerator GoalEffect()
    {
        
        int flameAmount = effectTime * 10;
        float perChange = changeColorAmount / flameAmount;
        for (int i = 0; i < flameAmount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            foreach (GameObject floor in changeColorFloor)
            {
                floor.GetComponent<Renderer>().material.color -= new Color(perChange, perChange, 0, 0);
            }
        }

        for(int i=0; i<flameAmount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            foreach(GameObject floor in changeColorFloor)
            {
                floor.GetComponent<Renderer>().material.color += new Color(perChange, perChange,  0, 0);
            }
        }
    }

    /// <summary>
    /// 画面を光らせるのと、画面を揺らすエフェクトを操作するエフェクト
    /// </summary>
    /// <returns></returns>
    IEnumerator ScreenEffect()
    {
        int flameAmount = 10;
        float perChange = effectImageChangeAmountA / flameAmount;
        for (int i = 0; i < flameAmount; i++)
        {
            yield return new WaitForSeconds(0.01f);
            effectImage.color += new Color(0, 0, 0, perChange);
        }
        int outAmount = 5;
        float perOutChange = effectImageChangeAmountA / outAmount;
        for(int  i=0; i < outAmount; i ++)
        {
            yield return new WaitForSeconds(0.01f);
            effectImage.color -= new Color(0, 0, 0, perOutChange);
        }

        effectImage.color = new Color(effectImage.color.r, effectImage.color.g, effectImage.color.b, 0);

    }
    

    /// <summary>
    /// 中に入っているオブジェクトを振り落とすためのコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator DumpObject()
    {
        int flame = 100;
        Vector3 goalRotation = goalObj.transform.rotation.eulerAngles;
        Vector3 dumpEndRotation = Change180Range(new Vector3(90, goalRotation.y ,goalRotation.z));
        goalRotation = Change180Range(goalRotation);
        Vector3 perRotation = Change180Range(dumpEndRotation - goalRotation) /flame; //dump.xが９０　goalRotationが２００　つまり、移動総量-110になるはず
        Vector3 backPerRotation =Change180Range(goalRotation-dumpEndRotation) / flame; //
        yield return new WaitForSeconds(2);
        //オブジェクトを下に落とす動作
        for (int count = 0; count < flame; count++)
        {
            yield return new WaitForSeconds(0.01f);
            goalObj.transform.Rotate(backPerRotation.x,0,0);
        }
        goalObj.transform.rotation = Quaternion.Euler(dumpEndRotation);
        yield return new WaitForSeconds(3);
        for(int count = 0; count < flame; count++)
        {
            yield return new WaitForSeconds(0.01f);
            goalObj.transform.Rotate(perRotation.x, 0,0);
        }
        goalObj.transform.rotation = Quaternion.Euler(goalRotation);
    }

    /// <summary>
    /// ゴール時のエフェクトとして、カメラを揺らすコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator CameraShake()
    {

        if(MyCameraSwitch.nowCamera == 1 | MyCameraSwitch.nowCamera == 4)
        {
            Vector3 nowRot = MyCameraSwitch.GetNowCamera().transform.rotation.eulerAngles;
            int nowCamera = MyCameraSwitch.nowCamera;
            for (int count = 0; count < 10; count++)
            {

                yield return new WaitForSeconds(0.01f);
                MyCameraSwitch.GetNowCamera().transform.rotation = Quaternion.Euler(nowRot);
                float  Xrandom =Random.Range(-0.5f, 0.5f);//カメラをこの範囲の角度のうちで揺らす
                float Yrandom = Random.Range(-0.5f,0.5f);
                MyCameraSwitch.GetNowCamera().transform.Rotate(Xrandom,Yrandom,0);
            }
            if(nowCamera == 1)
            {
                MyCameraSwitch.mainCamera.transform.rotation = Quaternion.Euler(nowRot);
            }
            else if(nowCamera == 4)
            {
                MyCameraSwitch.trackingCamera.transform.rotation = Quaternion.Euler(nowRot);
            }
        }

    }

    /// <summary>
    /// 0～360の角度を-180から180までの範囲の角度に変更する
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    private Vector3 Change180Range(Vector3 rotation)
    {
        return new Vector3(
            rotation.x < 180 ? rotation.x : rotation.x - 360,
            rotation.y < 180 ? rotation.y : rotation.y - 360,
            rotation.z < 180 ? rotation.z : rotation.z - 360
            );
    }

    /// <summary>
    /// 引数に指定された効果音を一度ならす
    /// </summary>
    /// <param name="se"></param>
    private void PlaySE()
    {
        SoundPlayer.Instance.PlayOneShot(SoundPlayer.goalTimeSound);
    }





    

}