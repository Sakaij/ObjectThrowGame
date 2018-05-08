using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManagerRelation {
    /// <summary>
    /// 効果音の操作を行う。ゲームオブジェクトにつけて使用
    /// </summary>
    public class SoundPlayer : SingletonMonoBehaviour<SoundPlayer> {

        /// <summary>
        /// ボタンを押したときになる音
        /// </summary>
        public static AudioClip buttonSound;
        /// <summary>
        /// オブジェクトフォースできない場合の音
        /// </summary>
        public static AudioClip cantForceSound;
        /// <summary>
        /// オブジェクトがゴールした時の音
        /// </summary>
        public static AudioClip goalTimeSound;
        /// <summary>
        /// ステージクリアの時の音
        /// </summary>
        public static AudioClip clearTimeSound;
        /// <summary>
        /// ライフを上げる時に使う音
        /// </summary>
        public static AudioClip lifeUpSound;
        /// <summary>
        /// オブジェクトえお投げる時に使う音
        /// </summary>
        public static AudioClip forceTimeSound;
        /// <summary>
        /// オブジェクトを消滅させるときに使う音
        /// </summary>
        public static AudioClip destroyTimeSound;

        public static AudioClip jumpSound;


        public static void Init()
        {
            buttonSound = Resources.Load("Sounds/button", typeof(AudioClip)) as AudioClip;
            cantForceSound = Resources.Load("Sounds/cantForceSE", typeof(AudioClip)) as AudioClip;
            goalTimeSound = Resources.Load("Sounds/whenGoalEffectMusic", typeof(AudioClip)) as AudioClip;
            forceTimeSound = Resources.Load("Sounds/forceTime", typeof(AudioClip)) as AudioClip;
            lifeUpSound= Resources.Load("Sounds/lifeUp", typeof(AudioClip)) as AudioClip;
            destroyTimeSound = Resources.Load("Sounds/destroyTime", typeof(AudioClip)) as AudioClip;
            jumpSound = Resources.Load("Sounds/jump", typeof(AudioClip)) as AudioClip;
            
            //clearTimeAudio = Resources.Load("Sounds/whenClear", typeof(AudioClip)) as AudioClip;
        }


        /// <summary>
        /// 引数の音を一回発生させる
        /// </summary>
        /// <param name="clip"></param>
        public void PlayOneShot(AudioClip clip)
        {
            if (SaveManager.GetSoundsBool())
            {
                gameObject.GetComponent<AudioSource>().clip = clip;
                gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
            }
        }

	}
}
