using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;

public class GoalMover : MonoBehaviour {

    //-200 < x < 200
    //２０＜ｙ＜４００
    //zは、１００＜ｚ＜450

	// Use this for initialization
	void Start () {
        if (MySceneManager.nowStageNumber == 40)
        {
            StartCoroutine(GoalMove());
        }
	}


    IEnumerator GoalMove()
    {
        int i = 0;
        while (i == 0)
        {
            yield return new WaitForSeconds(2);
            gameObject.transform.position = new Vector3(Random.Range(-200, 200), Random.Range(20, 400), Random.Range(100, 450));
        }
    }
	
}
