using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Advertisements;

/// <summary>
/// マネージャー系のパッケージ
/// </summary>
namespace ManagerRelation
{

    /// <summary>
    /// UIのアクティブの操作をする場合は、このクラスを介して行う。(各クラスでやるとエラーが発生するので）
    /// </summary>
    public class UIActiveManager : SingletonMonoBehaviour<UIActiveManager>
    {


        /// <summary>
        /// 引数に、表示させたいGUIを入れる。（すでに表示されていたら、何もしない）
        /// </summary>
        /// <param name="GUIArray"></param>
        public static void UIApper(params GameObject[] GUIArray)
        {
            foreach (GameObject targetGUI in GUIArray)
            {
                if (!targetGUI.activeSelf)
                {
                    targetGUI.SetActive(true);
                }
            }
        }


        /// <summary>
        /// 引数に、非表示にしたいGUIを入れる。（すでに非表示の場合は、何もしない）
        /// </summary>
        /// <param name="GUIArray"></param>
        public static void UIDisapper(params GameObject[] GUIArray)
        {
            foreach (GameObject targetGUI in GUIArray)
            {
                if (targetGUI.activeSelf)
                {
                    targetGUI.SetActive(false);
                }
            }
        }

    }





    /// <summary>
    /// シーンの遷移は必ずこのクラスを介して行う。それぞれのクラスで勝手にやってしまうと、変数などがおかしくなるので
    /// </summary>
    public class MySceneManager : SingletonMonoBehaviour<MySceneManager>
    {

        /// <summary>
        /// スタート画面用のシーン名
        /// </summary>
        public static readonly string SCENE_START = "StartScene";
        /// <summary>
        /// ステージ用のプレフィレンスの名前
        /// </summary>
        public static readonly string SCENE_STAGEPREFERENCE = "Stage";


        private static string nowSceneName;
        /// <summary>
        /// 今起動中のステージ数の番号
        /// </summary>
        public static int nowStageNumber = 0;



        /// <summary>
        /// 今起動中のシーン名
        /// </summary>
        public static string NowSceneName { get { return nowSceneName; } }



        /// <summary>
        /// 今のシーンを再生成する（再生成は、ステージ以外では、しない想定だがそれ以外でもOK）
        /// </summary>
        public static void SceneReLoad()
        {
            if (nowSceneName != null)
            {
                SceneManager.LoadScene(nowSceneName);
            }

        }


        /// <summary>
        /// スタート画面用のシーンに遷移する
        /// </summary>
        public static void StartScene()
        {
            MyLoadScene(SCENE_START);
            nowStageNumber = 0;
        }

        /// <summary>
        /// 引数に指定されたステージ数の番号のステージのシーンに遷移する
        /// </summary>
        /// <param name="stageNumber"></param>
        public static void StageSelect(int stageNumber)
        {
            if (SaveManager.GetMostAdvanceStage()+1 >= stageNumber)
            {
                if (stageNumber > 40 && stageNumber <= 50)
                {
                    nowStageNumber = stageNumber;
                    SaveManager.SetLastStage(stageNumber);
                    MyLoadScene(SCENE_STAGEPREFERENCE + "41_50");

                    return;
                    
                } else if (stageNumber > 30 && stageNumber <= 40)
                {
                    nowStageNumber = stageNumber;
                    SaveManager.SetLastStage(stageNumber);
                    MyLoadScene(SCENE_STAGEPREFERENCE + "31_40");
                    return;
                } else if (stageNumber > 20 && stageNumber <= 30)
                {
                    nowStageNumber = stageNumber;
                    SaveManager.SetLastStage(stageNumber);
                    MyLoadScene(SCENE_STAGEPREFERENCE + "21_30");
                    return;
                } else if (stageNumber > 10 && stageNumber <= 20)
                {
                    nowStageNumber = stageNumber;
                    SaveManager.SetLastStage(stageNumber);
                    MyLoadScene(SCENE_STAGEPREFERENCE + "11_20");
                    return;
                } else if (stageNumber > 1 && stageNumber <= 10)
                {
                    nowStageNumber = stageNumber;
                    SaveManager.SetLastStage(stageNumber);
                    MyLoadScene(SCENE_STAGEPREFERENCE + "2_10");
                    return;
                }
                else if (stageNumber == 1)
                {
                    nowStageNumber = stageNumber;
                    SaveManager.SetLastStage(stageNumber);
                    MyLoadScene(SCENE_STAGEPREFERENCE + 1);
                    return;
                }
            }
        }




        /// <summary>
        /// 今起動中のステージの次のステージのシーンに遷移する
        /// </summary>
        public static void NextStage()
        {
            if (nowStageNumber != 0)
            {
                nowStageNumber++;
                StageSelect(nowStageNumber);
            }
        }


        /// <summary>
        /// 変数などの関係で、シーン遷移自体はこの関数を必ず使う。
        /// </summary>
        private static void MyLoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            nowSceneName = sceneName;
        }

        /// <summary>
        /// プレイヤーが最後に遊んだステージへ遷移
        /// </summary>
        public static void LastStageLoad()
        {
            StageSelect(SaveManager.GetLastStage());
        }




    }










    /// <summary>
    /// 端末へのデータの保存のクラス。データの保存はこのクラスを介して行う。
    /// </summary>
    public class SaveManager : SingletonMonoBehaviour<SaveManager>
    {


        private static int stageAmount = 100; //ステージの総量最初は１００にしておく

        private static string nowSceneName;
        /// <summary>
        /// 今起動中のシーン名
        /// </summary>
        public static string NowSceneName { get { return nowSceneName; } }



        /// <summary>
        /// クリアした最大のステージを保存するときに使う。
        /// </summary>
        /// <param name="stageNumber"></param>
        public static void SetMostAdvanceStage(int stageNumber)
        {
            PlayerPrefs.SetInt(KeyManager.KEY_MOSTADVANCE, stageNumber);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// プレイヤーがクリアしたステージの中で一番進んでいるのステージを返す。(デフォルトでは0を返す）
        /// </summary>
        /// <returns></returns>
        public static int GetMostAdvanceStage()
        {
            if (PlayerPrefs.HasKey(KeyManager.KEY_MOSTADVANCE))
            {
                return PlayerPrefs.GetInt(KeyManager.KEY_MOSTADVANCE);

            }


            return 1;
        }


        /// <summary>
        /// 引数に指定されたライフをセーブする
        /// </summary>
        /// <param name="life"></param>
        public static void SaveLife(int life)
        {
            PlayerPrefs.SetInt(KeyManager.KEY_RESTOFLIFE, life);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// 残りのライフを確認する。
        /// </summary>
        /// <returns></returns>
        public static int RestOfLife()
        {
            if (PlayerPrefs.HasKey(KeyManager.KEY_RESTOFLIFE))
            {
                return PlayerPrefs.GetInt(KeyManager.KEY_RESTOFLIFE);
            }

            return 0;
        }

        /// <summary>
        /// 効果音のONOFF状態を取得する
        /// </summary>
        public static bool GetSoundsBool()
        {
            if (PlayerPrefs.HasKey(KeyManager.KEY_SOUNDSWITCH))
            {
                return PlayerPrefs.GetString(KeyManager.KEY_SOUNDSWITCH) == "false" ? false : true;
            }
            
            return true;
        }

        /// <summary>
        /// 効果音のオンオフ状態を変える。
        /// </summary>
        /// <param name="sound"></param>
        public static void SetSoundsBool(bool sound)
        {
            if (sound)
            {
                PlayerPrefs.SetString(KeyManager.KEY_SOUNDSWITCH, "true");
            }
            else
            {
                PlayerPrefs.SetString(KeyManager.KEY_SOUNDSWITCH, "false");
            }
            PlayerPrefs.Save();
        }

        /// <summary>
        /// 説明が終わったら、初めにプレイヤーに配布しておきたいライフを足し、終わり
        /// </summary>
        public static void SaveExplanationed()
        {
            if (!PlayerPrefs.HasKey(KeyManager.KEY_EXPLANATIONEDBOOL))
            {
                PlayerPrefs.SetString(KeyManager.KEY_EXPLANATIONEDBOOL, "true");
                PlayerPrefs.Save();
                Life.Instance.IncrementLife(200);

            }

        }

        /// <summary>
        /// BGMをつけるかどうかのブールの保存
        /// </summary>
        /// <param name="bgm"></param>
        public static void SetBGMBool(bool bgm)
        {
            if (bgm)
            {
                PlayerPrefs.SetString(KeyManager.KEY_BGMBOOL, "true");
            }
            else
            {
                PlayerPrefs.SetString(KeyManager.KEY_BGMBOOL, "false");
            }
            PlayerPrefs.Save();
        }

        /// <summary>
        /// BGMをつけるかどうかのブール値を返す(デフォルトではフォルスをかえす)
        /// </summary>
        /// <returns></returns>
        public static bool GetBGMBool()
        {
            if (PlayerPrefs.HasKey(KeyManager.KEY_BGMBOOL))
            {
                if(PlayerPrefs.GetString(KeyManager.KEY_BGMBOOL) == "false")
                {
                    return false;
                }
            }

            return true;
        }




        /// <summary>
        /// ステージごとに、クリアまであと何回ゴールすればよいかを保存しておく。
        /// </summary>
        public static void SaveUntilClearCount(int stageNumber, int untilGoal)
        {
            PlayerPrefs.SetInt(KeyManager.UntilClrearedStageKey(stageNumber), untilGoal);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// ゴールまで、何回ゴールすればよいかという情報を持つKEYが存在しているかどうかを返す。まず、数を取得する前に、ここで確認しておく。
        /// </summary>
        /// <param name="stageNumber"></param>
        /// <returns></returns>
        public static bool BoolUntilClrearHasKey(int stageNumber)
        {
            if (PlayerPrefs.HasKey(KeyManager.UntilClrearedStageKey(stageNumber)))
            {

                return true;
            }
            return false;
        }

        /// <summary>
        /// 引数の番号のステージの、クリアまで何回ゴールすればよいかを返す。必ず、BoolUntilCleareHasKey関数で確認してから、このメソッドを呼び出す。キーがなければ０を返す。
        /// </summary>
        /// <param name="stageNumber"></param>
        public static int GetUntilClearCount(int stageNumber)
        {
            if (PlayerPrefs.HasKey(KeyManager.UntilClrearedStageKey(stageNumber)))
            {
                return PlayerPrefs.GetInt(KeyManager.UntilClrearedStageKey(stageNumber));
            }

            return 0;
        }




        /// <summary>
        /// プレイヤーが最後に遊んだステージ数を取得
        /// </summary>
        public static int GetLastStage()
        {
            if (PlayerPrefs.HasKey(KeyManager.KEY_LASTSTAGE)){
                return PlayerPrefs.GetInt(KeyManager.KEY_LASTSTAGE);
            }

            return 1;
        }


        /// <summary>
        /// プレイヤーが最後に遊んだステージとして登録する
        /// </summary>
        public static void SetLastStage(int stageNumber)
        {
            PlayerPrefs.SetInt(KeyManager.KEY_LASTSTAGE, stageNumber);
            PlayerPrefs.Save();
        }


    }


    /// <summary>
    /// ローカライズに適用するためテキストを変えるクラス。テキストはすべて英語で初期化されている
    /// </summary>
    
    public class TextManager : SingletonMonoBehaviour<TextManager>
    {

        public static Dictionary<string, string> stringDictionary = new Dictionary<string, string>();
        public static Font UnipixFont;
        public static bool FontBool;
        public static SystemLanguage playerslanguage;
        public enum KEY
        { 
            ANOTHERANGLE,
            RIGHTCAMERABUTTON,
            LEFTCAMERABUTTON,
            CHACEMODEON,
            CHACEMODEOFF,
            RETRY,
            NEXTSTAGE,
            STAGECLEAR,
            STARTSCREEN,
            STAGEDETAILS,
            POWERTEXT,
            MAINCAMERABACK,
            REWARD,
            UNTILCLEAR,
            SOUNDBUTTONON,
            SOUNDBUTTONOFF,
            STAGESELECTBUTTON,
            CONTINUEBUTTON,
            SELECTBACKBUTTON,
            LETPLAY,
            EXPLANATIONUNTILCLEAR,
            EXPLANATIONLIFE,
            BGMBUTTONON,
            BGMBUTTONOFF,
            STAGENUMBER,
            SHARE,
        }
        public enum FONTKEY {
            Unipix,
            OPENSANS,
            def,
        }



        /// <summary>
        /// 仕様言語を指定することで、ディクショナリーより言語別の単語を調べられるようにする。
        /// </summary>
        /// <param name="lang"></param>
        public static void Init(SystemLanguage lang)
        {
            string filePath;//ファイル名
            playerslanguage = lang;
            switch (lang) {
                case SystemLanguage.English:
                    filePath = "Text/english";   
                    break;
                case SystemLanguage.ChineseSimplified:
                    filePath = "Text/chinesesimplified";
                    break;
                case SystemLanguage.ChineseTraditional:
                    filePath = "Text/chinetraditional";
                    break;
                case SystemLanguage.Japanese:
                    filePath = "Text/japanese";
                    break;
                case SystemLanguage.Chinese:
                    filePath = "Text/chinese";
                    break;
                case SystemLanguage.Korean:
                    filePath = "Text/korean";
                    break;
                case SystemLanguage.Thai:
                    filePath = "Text/thai";
                    break;
                case SystemLanguage.German:
                    filePath = "Text/german";
                    FontBool = true;
                    break;
                case SystemLanguage.French:
                    filePath = "Text/french";
                    FontBool = true;
                    break;
                default:
                    filePath = "Text/english";
                    break;
            }
            stringDictionary.Clear(); //まず初期化しておく
            //指定言語のファイルを取得
            UnipixFont = GetFont(FONTKEY.Unipix);
            TextAsset asset = Resources.Load(filePath, typeof(TextAsset)) as TextAsset;
            Debug.Log(asset.text);
            StringReader str = new StringReader(asset.text);
            //Peekは、次の文字を読み込んだ時nullになるまで繰り返す。(このwhile文の処理で、ディクショナリーにすべての要素を代入しておく。);
            while(str.Peek() > -1)
            {
                string line = str.ReadLine();
                string[] values = line.Split(',');
                stringDictionary.Add(values[0], values[1]);
            }


        }

        /// <summary>
        /// KEYに該当した値を返す。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Font GetFont(FONTKEY key)
        {
            return GetFont(System.Enum.GetName(typeof(FONTKEY), key));

        }
        /// <summary>
        /// KEYに該当した値を返す。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Font GetFont(string key)
        {
            return Resources.Load("Font/"+key, typeof(Font)) as Font;
        }

        /// <summary>
        /// KEYに該当する値を返す。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(KEY key)
        {
            return Get(System.Enum.GetName(typeof(KEY), key));

        }

        /// <summary>
        /// KEYに該当する値を返す。間違い防止のためKEY型のメソッドを呼び出すこと推奨
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            return stringDictionary[key];
        }

    }


    /// <summary>
    /// このクラスは、KeyAndValueで保存する内容のKeyの取得を取得する。勝手に個々でやると複雑になるので
    /// </summary>
    public class KeyManager : MonoBehaviour
    {

        /// <summary>
        /// プレイヤーが現時点で挑戦できるうちで最も進んでいるステージのKEY
        /// </summary>
        public static readonly string KEY_MOSTADVANCE = "mostAdvance";


        /// スタート画面に表示できる最大のキャラクター番号
        /// </summary>
        public static readonly string KEY_START_CHARACTRRMAXCOUNT = "start_characterMax";

        /// <summary>
        /// ステージをクリアしたことを表すValue
        /// </summary>
        public static readonly string VALUE_STAGECLREAED = " clreared";

        /// <summary>
        /// 残りのライフを表すKEy
        /// </summary>
        public static readonly string KEY_RESTOFLIFE = "rest";


        private static readonly string KEY_UNTILCLREAR = "until_stage";

        public static readonly string KEY_EXPLANATIONEDBOOL = "explanationed";

        public static readonly string KEY_BGMBOOL = "bgm";

        public static readonly string KEY_SEBOOL = "se";


        /// <summary>
        /// クリアまで何回ゴールが必要かのKEYを返す。引数にステージの番号を入れる。
        /// </summary>
        /// <param name="stageNumber"></param>
        /// <returns></returns>
        public static string UntilClrearedStageKey(int stageNumber)
        {
            return (KEY_UNTILCLREAR + stageNumber.ToString());
        }

        /// <summary>
        /// 端末の言語用のKEY
        /// </summary>
        public static readonly string KEY_LANGUAGE = "language";


        public static readonly string KEY_SOUNDSWITCH = "sound";

        public static readonly string KEY_LASTSTAGE = "laststage";

    }


    /// <summary>
    /// このクラスは、ステージごとにUPするライフなどの細かい値を取得するためのクラス。
    /// </summary>
    public class ValueManager : SingletonMonoBehaviour<ValueManager>
    {

        /// <summary>
        /// 指定のステージをクリアすることによってライフをどのくらい上げれるかを取得
        /// </summary>
        /// <param name="stageNumber"></param>
        /// <returns></returns>
        public static int GetStageOfUpLife(int stageNumber)
        {
            int bigNum = Mathf.FloorToInt(stageNumber / 10); //１０の位の値を取得
            int smallNum = Mathf.FloorToInt(stageNumber % 10); //１の位を取得
            switch (bigNum)
            {
                //0の場合は　１～９の場合の数値
                case 0:
                    switch (smallNum)
                    {
                        case 0:
                            return 0;
                        case 1:
                            return 3;
                        case 2:
                            return 5;
                        case 3:
                            return 10;
                        case 4:
                            return 12;
                        case 5:
                            return 10;
                        case 6:
                            return 12;
                        case 7:
                            return 12;
                        case 8:
                            return 15;
                        case 9:
                            return 20;
                    }
                    break;
                case 1:
                    switch (smallNum)
                    {
                        case 0:
                            return 12;
                        case 1:
                            return 20;
                        case 2:
                            return 20;
                        case 3:
                            return 25;
                        case 4:
                            return 40;
                        case 5:
                            return 50;
                        case 6:
                            return 20;
                        case 7:
                            return 50;
                        case 8:
                            return 30;
                        case 9:
                            return 100;
                    }

                    break;
                case 2:
                    switch (smallNum)
                    {
                        case 0:
                            return 150;
                        case 1:
                            return 20;
                        case 2:
                            return 50;
                        case 3:
                            return 100;
                        case 4:
                            return 30;
                        case 5:
                            return 50;
                        case 6:
                            return 100;
                        case 7:
                            return 50;
                        case 8:
                            return 80;
                        case 9:
                            return 100;
                    }
                    break;
                case 3:
                    switch (smallNum)
                    {
                        case 0:
                            return 300;
                        case 1:
                            return 300;
                        case 2:
                            return 300;
                        case 3:
                            return 400;
                        case 4:
                            return 300;
                        case 5:
                            return 300;
                        case 6:
                            return 300;
                        case 7:
                            return 500;
                        case 8:
                            return 1000;
                        case 9:
                            return 2000;
                    }
                    break;
                case 4:
                    switch (smallNum)
                    {
                        case 0:
                            return 100;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                            //初期バージョンでの最終ステージは、ここ
                        case 9:
                            break;
                    }
                    break;
                case 5:
                    switch (smallNum)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }
                    break;
                case 6:
                    switch (smallNum)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }
                    break;
                case 7:
                    switch (smallNum)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }
                    break;
                case 8:
                    switch (smallNum)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }
                    break;
                case 9:
                    switch (smallNum)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }
                    break;
                case 10:
                    switch (smallNum)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }
                    break;
                default:
                    return 1;



            }
            return 1;

        }


        /// <summary>
        /// 指定のステージ番号のステージは、クリアまでに何ゴールを要するのかを取得するメソッド
        /// </summary>
        /// <param name="stageNumber"></param>
        /// <returns></returns>
        public static int UntilStageClear(int stageNumber)
        {
            //0の場合は　１～９の場合の数値
            int bigNum = Mathf.FloorToInt(stageNumber / 10); //１０の位の値を取得
            int smallNum = Mathf.FloorToInt(stageNumber % 10); //１の位を取得
            switch (bigNum)
            {
                //0の場合は　１～９の場合の数値
                case 0:
                    switch (smallNum)
                    {
                        case 0:
                            return 1;
                        case 1:
                            return 2;   
                        case 2:
                            return 3;
                        case 3:
                            return 5;
                        case 4:
                            return 6;
                        case 5:
                            return 6;
                        case 6:
                            return 5;
                        case 7:
                            return 5;
                        case 8:
                            return 10;
                        case 9:
                            return 2;
                    }
                    break;
                case 1:
                    switch (smallNum)
                    {
                        case 0:
                            return 4;
                        case 1:
                            return 5;
                        case 2:
                            return 2;
                        case 3:
                            return 2;
                        case 4:
                            return 5;
                        case 5:
                            return 3;
                        case 6:
                            return 7;
                        case 7:
                            return 4;
                        case 8:
                            return 3;
                        case 9:
                            return 5;
                    }

                    break;
                case 2:
                    switch (smallNum)
                    {
                        case 0:
                            return 5;
                        case 1:
                            return 5;
                        case 2:
                            return 10;
                        case 3:
                            return 20;
                        case 4:
                            return 5;
                        case 5:
                            return 20;
                        case 6:
                            return 20;
                        case 7:
                            return 10;
                        case 8:
                            return 4;
                        case 9:
                            return 8;
                    }
                    break;
                case 3:
                    switch (smallNum)
                    {
                        case 0:
                            return 30;
                        case 1:
                            return 30;
                        case 2:
                            return 3;
                        case 3:
                            return 20;
                        case 4:
                            return 40;
                        case 5:
                            return 30;
                        case 6:
                            return 30;
                        case 7:
                            return 20;
                        case 8:
                            return 30;
                        case 9:
                            return 5;
                    }
                    break;
                case 4:
                    switch (smallNum)
                    {
                        case 0:
                            return 1;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }
                    break;
                case 5:
                    switch (smallNum)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }
                    break;
                case 6:
                    switch (smallNum)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }
                    break;
                case 7:
                    switch (smallNum)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }
                    break;
                case 8:
                    switch (smallNum)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }
                    break;
                case 9:
                    switch (smallNum)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }
                    break;
                case 10:
                    switch (smallNum)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }
                    break;
                default:
                    return 1;



            }
            return 1;

        }
    }







    /// <summary>
    ///これを継承するとシングルトンになる
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance {
            get
            {
                if(instance == null)
                {
                    //ここでクラスを継承元のクラスの取得
                    instance = (T)FindObjectOfType(typeof(T));
                }

            return instance;
            }
            
        }
        
    }





}




