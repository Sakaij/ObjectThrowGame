using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;


/// <summary>
/// BGMの管理をする。ゲームオブジェクトにつけて使用
/// </summary>
public class BGMPlayer : SingletonMonoBehaviour<BGMPlayer> {

    //スタート画面のBGM
    public static AudioClip startBGM;
    //ステージ中のBGM
    public static AudioClip stageBGM;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<AudioSource>().loop = true;
        if (SaveManager.GetBGMBool())
        {
            //ステージ番号が０ならばスタート画面
            if (MySceneManager.nowStageNumber == 0)
            {
                StartBGM(startBGM);
            }
            //ステージの場合の処理
            else
            {
                StartBGM(stageBGM);
            }
        }
	}

    //BGMをとめる
    public void StopBGM()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
    //BGMの再生
    public void StartBGM(AudioClip clip)
    {
        gameObject.GetComponent<AudioSource>().clip = clip;
        gameObject.GetComponent<AudioSource>().Play();
    }


    public static void Init()
    {
        startBGM = Resources.Load("Sounds/StartSceneBGM", typeof(AudioClip)) as AudioClip;
        stageBGM = Resources.Load("Sounds/StageSceneBGM", typeof(AudioClip)) as AudioClip;
    }

}
