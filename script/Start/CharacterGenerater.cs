using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;

/// <summary>
/// スタート画面に表示するキャラクターを生成する
/// </summary>
public class CharacterGenerater : MonoBehaviour {


    /*
     * ステージ１０を超えると、生成数を２に増やす。以後１０を超えるごとに１つづつ増やす。
     */

    /// <summary>
    /// リソースの目的のオブジェクトにいきつくまでのPASS
    /// </summary>
    private static string[] characterPath= {"character1" };
    //顔のスプライトのファイル名はface1_1,face1_2,face1_3 face2_1...となるようにする
    private static string[] facePath= {"face1_" };
    private static GameObject[] characterArray;

    public Transform[] spawnPosition;
    public float[] untilBlinkTime;
    public float[] untilShakeHeadTime;

    //生成する キャラクターの種類の数
    public int characterCount = 1;
    private int spawnCount;
    


	// Use this for initialization
	void Start () {
        Initialize();
	}
	


    private void Initialize()
    {      
        spawnCount = SaveManager.GetMostAdvanceStage() == 0 ?1:Mathf.FloorToInt(SaveManager.GetMostAdvanceStage() / 10) + 1;
        characterArray = new GameObject[characterPath.Length];
        for(int i=0;i < characterPath.Length; i++)
        {
            characterArray[i] = Resources.Load(characterPath[i], typeof(GameObject)) as GameObject;
        }
    }

    /// <summary>
    /// キャラクターを生成する(引数は、キャラクターを生成する数)
    /// </summary>
    /// <param name="spawnCount"></param>
    private void CharacterGenerate(int spawnCount)
    {
        
    }

}
