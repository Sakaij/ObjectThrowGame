using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;
/// <summary>
/// 主に、11～20のうちのステージで使用
/// </summary>
public class ObstacleScript : StickAndBaseGeneraterScript<ObstacleScript.Obstacle>
{
    public static  string objName = "Obstacle";

    private Obstacle[] subArray =
    {
        new Obstacle(11,new Vector3(0,90,300), new Vector3(100,100,20),new Vector3(80,0,0)),
        new Obstacle(13, new Vector3(-100,20,480), new Vector3(50,50,10), new Vector3(120,120,0)),
        new Obstacle(14, new Vector3(0,20,380), new Vector3(50,50,10), new Vector3(100,0,0) ),
        new Obstacle(15,new Vector3(-20,150,550), new Vector3(80,80,10), new Vector3(60,250,0)),
        new Obstacle(16,new Vector3(-140,20,530),new Vector3(50,50,10), new Vector3(110,300,0)),
        new Obstacle(17,new Vector3(0,30,480), new Vector3(50,50,10), new Vector3(80,0,0)),
        new Obstacle(18,new Vector3(-30,150,400),new Vector3(50,50,10),new Vector3(120,120,0)),
        new Obstacle(19,new Vector3(30,50,300),new Vector3(50,50,10),new Vector3(40,30,0)),
        new Obstacle(20,new Vector3(-20,30,550), new Vector3(80,80,10), new Vector3(290,60,0)),
        new Obstacle(20,new Vector3(260,20,1270), new Vector3(50,50,10), new Vector3(120,0,0)),
        new Obstacle(39,new Vector3(0,80,170),new Vector3(50,20,50),new Vector3(150,0,0))
    };

    public void Start()
    {
        objArray = subArray;
        CheckBoolAndGenerate();
    }




    public class Obstacle : StickAndBaseObjectGenerator {
        private static string thisObjectName = "Obstacle";


        public Obstacle(int stageNumber,Vector3 position,Vector3 scale,Vector3 rotation) : base(thisObjectName,stageNumber,position,scale,rotation)
        {
            
        }



    }




}

