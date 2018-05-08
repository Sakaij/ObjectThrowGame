using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;

/// <summary>
///クリアステージ数に応じてスタート画面にキャラクターを表示するクラス
/// </summary>
public class ObjectInstance : MonoBehaviour {

    public int spawnCount;
    //キャラクターの種類の数(キャラクター番号１の基本顔の場合は character1_1という感じになる）
    public static string characterFleName="Character/"+"character";
    //キャラクターの顔のファイル名（キャラ１の基本の顔ならばface1_1となるようにする
    public static string faceFileName = "Image/face";
    //実際に生成したオブジェクト
    private CharacterObject[] characterArray;
    private Character[] characterInfArray;
    public int characterCount;
    public GameObject[] startObjectArray; //オブジェクト用の配列
    public Transform[] startObjectPosition; //オブジェクトを生成する位置
    public float[] untilFunnyFaceTime; //キャラクターの番号と連動した数
    public float[] untilBlinkFaceTime; //瞬きまでにかける時
    public static float funnyFaceTime=0.1f; //キャラクターの番号と連動した数

    public static Vector3 perFlameRotation = new Vector3( 0, 1f, 0);
    public Image testimage;
    private static int untilSpawnAmount;
    private Sprite[,] faceArray; //一つ目の配列は、キャラクター番号、２つめの配列は、キャラクター特有の顔４枚
    private int spawnAmount; //スポーンさせることができるキャラクター数

    //@Cx1a1fkEQ2TCzCj


	void Start () {
        Initialize();
        ObjectGenerate(spawnCount);
        
	}




    /// <summary>
    /// ここで実際に生成する(引数は何匹生成するか）
    /// </summary>
    /// <param name="count"></param>
    public void ObjectGenerate(int count)
    {
        if (count > 10) { count = 10; };
        HashSet<int> has = new HashSet<int>();
        for(int i = 1; i <= count; i++)
        {
            bool a = true;
            int position=0;
            while (a)
            {
                int random = Random.Range(0, 10);
                
                if (!has.Contains(random))
                {
                    has.Add(random);
                    position = random;
                    a = false;
                    //Debug.Log("PositionNumber" + random);
                }
            }

            if(i > characterCount)
            {
                CharacterObject obj = new CharacterObject(characterInfArray[0], startObjectPosition[position].position, untilBlinkFaceTime[i - 1], untilFunnyFaceTime[i - 1]);
                StartCoroutine(FaceChange(obj));
                StartCoroutine(FunnyFace(obj));
            }
            else
            {
                CharacterObject obj = new CharacterObject(characterInfArray[i - 1], startObjectPosition[position].position, untilBlinkFaceTime[i - 1], untilFunnyFaceTime[i - 1]);
                StartCoroutine(FaceChange(obj));
                StartCoroutine(FunnyFace(obj));
            }

            

            
        }
        
    }




    /// <summary>
    /// 主に瞬きの時に使う
    /// </summary>
    /// <param name="targetCharacter"></param>
    /// <returns></returns>
    IEnumerator FaceChange(CharacterObject targetCharacter)
    {
        int count = 0;
        while (count == 0)
        {
            yield return new WaitForSeconds(targetCharacter.untilBlinkTime);
            if (targetCharacter.blinkBool)
            {
                targetCharacter.FaceChange(2);

            }
            if (targetCharacter.blinkBool)
            {
                yield return new WaitForSeconds(0.5f);
                targetCharacter.FaceChange(1);
            }
        }
    }


    IEnumerator FunnyFace(CharacterObject targetCharacter)
    {
        Debug.Log("FunnyFaceコルーチンが呼び出されています");
        yield return new WaitForSeconds(targetCharacter.untilShakeHeadTime);
        Debug.Log("FunnyFaceの起動時間になりまし");
        targetCharacter.blinkBool = false;
        targetCharacter.shakeHeadBool = true;
        StartCoroutine("ShakeHead", targetCharacter);
        for (int count = 0; count < 10; count++)
        {
            yield return new WaitForSeconds(funnyFaceTime);
            targetCharacter.FaceChange(2);
            yield return new WaitForSeconds(funnyFaceTime);
            targetCharacter.FaceChange(3);
        }
        targetCharacter.shakeHeadBool = false;
        targetCharacter.FaceChange(1);
        targetCharacter.blinkBool = true;
        targetCharacter.character.transform.rotation = Quaternion.Euler(0, 0, 0);

        StartCoroutine(FunnyFace(targetCharacter));

    }

    /// <summary>
    /// このコルーチンは、首を横に振る動作をする
    /// </summary>
    /// <returns></returns>
    IEnumerator ShakeHead(CharacterObject target)
    {
        int count = 0;
        while (count == 0)
        {
            if (target.shakeHeadBool)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (target.shakeHeadBool)
                    {
                        target.character.transform.Rotate(perFlameRotation);
                    }
                    else
                    {
                        break;
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                for (int i = 0; i < 10; i++)
                {
                    if (target.shakeHeadBool)
                    {
                        target.character.transform.Rotate(-perFlameRotation);
                    }
                    else
                    {
                        break;
                    }
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else
            {
                break;
            }




        }

        target.character.transform.rotation = Quaternion.Euler(0, 0, 0);

    }


    private void Initialize()
    {

        //顔の数は、キャラクターが何種類いるかで決める(初期バージョンは、１匹のキャラクターを何匹も生成する仕様なので生成する顔は全部で２枚
        faceArray = new Sprite[characterCount,3];

        //顔の配列をつくる
        for (int i = 0; i < characterCount; i++)
        {
            //１つのキャラクターにつき２つの顔のSpriteを用意する
            for (int j = 0; j < 3; j++)
            {
                faceArray[i,j] = Resources.Load(faceFileName + (i + 1) + "_" + (j + 1), typeof(Sprite)) as Sprite;
            }
        }

        //キャラクターの種類の数によりキャラクターを生成
        characterInfArray = new Character[characterCount];

        //すでに基本の顔のついたゲームオブジェクトを代入
        for(int i=0;i< characterCount; i++)
        {
            characterInfArray[i] = new Character(Resources.Load(characterFleName + (i + 1), typeof(GameObject)) as GameObject,i+1, faceArray[i,0], faceArray[i,1],faceArray[i,2]);
        }


        //ここで、オブジェクトを何個生成するのかを決める
        int most = SaveManager.GetMostAdvanceStage(); //何も保存されていなければ１がかえってくる。
        if ((most > 0) && (most < 101))
        {
            spawnCount = Mathf.FloorToInt((most / 10) + 1);
        }
        else if (most <= 0)
        {
            spawnCount = 1;
        }
        else if (most > 101)
        {
            spawnCount = 10;

        }


    }




}


/// <summary>
/// このクラスはキャラクターの種類を扱うクラス
/// </summary>
public class Character : MonoBehaviour{

    /// <summary>
    /// ０から９までの、キャラクターごとに割り当てられている数字を表す。
    /// </summary>
    public int characterNum;
    /// <summary>
    /// ここは、顔とオブジェクトの親クラスの方のゲームオブジェクトを入れる。
    /// </summary>
    public GameObject character;
    /// <summary>
    /// 基本の顔
    /// </summary>
    public Sprite baseFace;
    /// <summary>
    /// 瞬き用の顔
    /// </summary>
    public Sprite blinkFace;
    /// <summary>
    /// 口を開けてるパターン
    /// </summary>
    public Sprite blinkFace2;
    public bool blinkBool=true;
    /// <summary>
    /// 首を振ってもよいか
    /// </summary>
    public bool shakeHeadBool ;


    /// <summary>
    /// キャラクターの顔の画像を引数にあった顔に変更する 1,基本形の顔 2,瞬き用の顔3,変顔１ 4,変顔２
    /// </summary>
    /// <param name="faceNumber"></param>
    public void FaceChange(int faceNumber)
    {
        switch (faceNumber) {
            case 1:
                character.GetComponentInChildren<SpriteRenderer>().sprite = baseFace;
                break;
            case 2:
                character.GetComponentInChildren<SpriteRenderer>().sprite = blinkFace;
                break;
            case 3:
                character.GetComponentInChildren<SpriteRenderer>().sprite = blinkFace2;
                break;

        }


    }

    public Character(GameObject character ,int charNum,Sprite baseFace, Sprite blinkFace,Sprite blinkFace2)
    {

        this.character = character;
        this.characterNum = charNum;
        this.baseFace = baseFace;
        this.blinkFace = blinkFace;
        this.blinkFace2 = blinkFace2;
        FaceChange(1);

    }




}

//実際に画面に表示する一匹一匹づつの情報を扱うクラス
class CharacterObject : MonoBehaviour
{
    //何番目に生成されたオブジェクトか
    public int objectNumber;
    //瞬きをするまでに要する時間
    public float untilBlinkTime;
    //首を振るコルーチンにを開始するまでに要する時間
    public float untilShakeHeadTime;
    //生成された対象のオブジェクト、実際にオブジェクトに操作をする場合はこのオブジェクトからいじる
    public GameObject character;

    public Sprite baseFace;
    public Sprite blinkFace;
    public Sprite blinkFace2;

    public bool blinkBool = true;
    public bool shakeHeadBool;


    //インスタンス化した時点でオブジェクトを生成する
    public CharacterObject(Character character, Vector3 position, float untilBlinkTime, float untilShakeHeadTime)
    {
        this.untilBlinkTime = untilBlinkTime;
        this.untilShakeHeadTime = untilShakeHeadTime;
        this.baseFace = character.baseFace;
        this.blinkFace = character.blinkFace;
        this.blinkFace2 = character.blinkFace2;
        this.character = Instantiate(character.character, position, Quaternion.identity) as GameObject;
    }

    /// <summary>
    /// 指定の顔に変更する（１は基本顔、２は瞬きの時の顔、３は瞬き用の顔のVer2
    /// </summary>
    /// <param name="face"></param>
    public void FaceChange(int faceNumber)
    {
        switch (faceNumber)
        {
            case 1:
                character.GetComponentInChildren<SpriteRenderer>().sprite = baseFace;
                break;
            case 2:
                character.GetComponentInChildren<SpriteRenderer>().sprite = blinkFace;
                break;
            case 3:
                character.GetComponentInChildren<SpriteRenderer>().sprite = blinkFace2;
                break;
            default:
                character.GetComponentInChildren<SpriteRenderer>().sprite = baseFace;
                break;



        }

    }


}

