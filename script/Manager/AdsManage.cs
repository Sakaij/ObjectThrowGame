using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

namespace ManagerRelation
{
    /// <summary>
    /// リワード広告の管理をするクラス。
    /// </summary>
    public class AdsManage : SingletonMonoBehaviour<AdsManage>
    {

        private static int movieUpLife = 50;

        private bool lifeUpBool = true;



        /// <summary>
        /// 動画のスタートはこの関数(引数のボタンを一時的にクリックできないようにする
        /// </summary>
        public void MovieStart()
        {
            if (CanPlay())
            {
                var options = new ShowOptions { resultCallback = HandleShowResult };
                Advertisement.Show("rewardedVideo", options);
            }

        }                                                                                                                 

        /// <summary>
        /// 動画再生ができるかどうかを返す
        /// </summary>
        /// <returns></returns>
        public bool CanPlay()
        {
            return Advertisement.isSupported && Advertisement.IsReady("rewardedVideo");
        }

        private void HandleShowResult(ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    //ここで報酬発生
                    StartCoroutine(LifeUp());
                    Debug.Log("動画視聴が正常に行われました");


#if true

#endif
                    break;
                case ShowResult.Failed:

                    Debug.Log("動画視聴が失敗しました");
                    break;
                case ShowResult.Skipped:
                    Debug.Log("動画視聴中にプレイヤーがスキップをしました");
                    break;

            }

        }

        /// <summary>
        /// 何度もクリックされた場合５０以上UPしてしまうので、５秒判定期間を設けて、
        /// </summary>
        /// <returns></returns>
        IEnumerator LifeUp()
        {
            if (lifeUpBool)
            {
                lifeUpBool = false;
                Life.Instance.IncrementLife(movieUpLife);
                yield return new WaitForSeconds(5);
                lifeUpBool = true;
            }
            yield break;
        }

    }

   

    
}
