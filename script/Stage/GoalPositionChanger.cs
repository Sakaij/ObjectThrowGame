using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;

/// <summary>
/// シーンごとにゴールの位置、角度、大きさを変えるためのクラス
/// </summary>
public class GoalPositionChanger : MonoBehaviour {

    //一番入りやすい位置は、new Vector3(0,50,170);
    private GoalInfo[] goalInfo =
    {
        //new GoalInfo(ステージの番号, 位置, 大きさ, 角度)
        null,
        null,
        new GoalInfo(2,new Vector3(0,50,170),new Vector3(30,30,40),new Vector3(200,0,0)),
        new GoalInfo(3,new Vector3(0,50,170),new Vector3(30,30,40),new Vector3(180,25,0)),
        new GoalInfo(4,new Vector3(0,50,170),new Vector3(30,30,40),new Vector3(270,0,0)),
        new GoalInfo(5,new Vector3(10,50,170),new Vector3(20,20,20),new Vector3(200,0,0)),
        new GoalInfo(6,new Vector3(0,50,170),new Vector3(30,30,40),new Vector3(290,0,0)),
        new GoalInfo(7,new Vector3(-30,100,300),new Vector3(30,30,20),new Vector3(200,0,0)),
        new GoalInfo(8,new Vector3(10,100,170),new Vector3(30,30,40),new Vector3(200,20,0)),
        new GoalInfo(9,new Vector3(0,100,220),new Vector3(10,10,10),new Vector3(200,0,0)),
        new GoalInfo(10,new Vector3(30,120,200),new Vector3(20,20,20),new Vector3(270,0,0)),
        new GoalInfo(11,new Vector3(0,100,600),new Vector3(50,50,50),new Vector3(270,40,0)),
        new GoalInfo(12,new Vector3(-190,100,400),new Vector3(30,30,20),new Vector3(270,0,0)),
        new GoalInfo(13,new Vector3(70,70,750),new Vector3(50,50,40),new Vector3(200,40,0)),
        new GoalInfo(14,new Vector3(20,100,790),new Vector3(20,20,30),new Vector3(270,0,0)),
        new GoalInfo(15,new Vector3(300,50,1260),new Vector3(60,60,40),new Vector3(270,0,0)),
        new GoalInfo(16,new Vector3(0,200,800),new Vector3(30,30,40),new Vector3(270,0,0)),
        new GoalInfo(17,new Vector3(0,200,500),new Vector3(30,30,100),new Vector3(90,0,0)),
        new GoalInfo(18,new Vector3(70,320,600),new Vector3(30,30,30),new Vector3(180,60,0)),
        new GoalInfo(19,new Vector3(-50,470,600),new Vector3(30,30,30),new Vector3(180,0,0)),
        new GoalInfo(20,new Vector3(410,400,1750),new Vector3(50,50,20),new Vector3(270,0,0)),
        new GoalInfo(21,new Vector3(-50,-300,200),new Vector3(30,30,40),new Vector3(270,0,0)),
        new GoalInfo(22,new Vector3(-4,-156,200),new Vector3(8,8,50),new Vector3(270,0,0)),
        new GoalInfo(23,new Vector3(-4,-56,200),new Vector3(8,8,50),new Vector3(270,0,0)),
        new GoalInfo(24,new Vector3(-4,-70,-38),new Vector3(10,10,10),new Vector3(270,0,0)),
        new GoalInfo(25,new Vector3(0,5,280),new Vector3(10,10,50),new Vector3(180,0,0)),
        new GoalInfo(26,new Vector3(10,5,280),new Vector3(10,10,50),new Vector3(180,0,0)),
        new GoalInfo(27,new Vector3(0,-150,-100),new Vector3(30,30,40),new Vector3(270,0,0)),
        new GoalInfo(28,new Vector3(0,-200,280),new Vector3(30,30,40),new Vector3(270,0,0)),
        new GoalInfo(29,new Vector3(15,-178,-100),new Vector3(8,8,40),new Vector3(270,0,0)),
        new GoalInfo(30,new Vector3(70,-60,-50),new Vector3(30,30,20),new Vector3(270,0,0)),
        new GoalInfo(31,new Vector3(0,30,140),new Vector3(10,10,20),new Vector3(270,0,0)),
        new GoalInfo(32,new Vector3(0,30,353),new Vector3(10,10,20),new Vector3(260,0,0)),
        new GoalInfo(33,new Vector3(30,10,337),new Vector3(10,10,30),new Vector3(270,0,0)),
        new GoalInfo(34,new Vector3(0,150,200),new Vector3(10,10,100),new Vector3(80,0,0)),
        new GoalInfo(35,new Vector3(6,10,353),new Vector3(20,20,20),new Vector3(180,0,0)),
        new GoalInfo(36,new Vector3(20,60,270),new Vector3(10,10,10),new Vector3(270,0,0)),
        new GoalInfo(37,new Vector3(0,15,183),new Vector3(10,10,20),new Vector3(270,0,0)),
        new GoalInfo(38,new Vector3(0,30,353),new Vector3(10,10,20),new Vector3(270,0,0)),
        new GoalInfo(39,new Vector3(0,220,353),new Vector3(10,10,20),new Vector3(270,0,0)),
        new GoalInfo(40,new Vector3(0,20,300),new Vector3(30,30,20),new Vector3(240,0,0)),
    };
    



    // Use this for initialization
    void Start () {
        //もし、ステージナンバーと違っていたら、なにもしない）
        GoalInfo info = goalInfo[MySceneManager.nowStageNumber];
        if(info.stageNumber == MySceneManager.nowStageNumber && info !=null)
        {
            gameObject.transform.position = info.position;
            gameObject.transform.rotation = Quaternion.Euler(info.rotation);
            gameObject.transform.localScale = info.scale;
        }
	}


    /// <summary>
    /// ゴールの位置などの情報をまとめたクラス
    /// </summary>
    class GoalInfo
    {
        public int stageNumber;
        public Vector3 position;
        public Vector3 scale;
        public Vector3 rotation;

        /// <summary>
        /// いい感じになるのは、new GoalInfo(num,new Vector3(0,50,170),new Vector3(30,30,30),new Vector3(180,0,0):
        /// </summary>
        /// <param name="stageNumber"></param>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        /// <param name="rotation"></param>
        public GoalInfo(int stageNumber,Vector3 position, Vector3 scale, Vector3 rotation)
        { 
            this.stageNumber = stageNumber;
            this.position = position;
            this.scale = scale;
            this.rotation = rotation;
        }
            
            
    }




}
