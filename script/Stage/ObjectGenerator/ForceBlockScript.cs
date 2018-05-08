using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;

/// <summary>
/// オブジェクトに直接アタッチするクラス
/// </summary>
public class ForceBlockScript : StickAndBaseGeneraterScript<ForceBlockScript.ForceBlock> {


    private ForceBlock[] subArray = new ForceBlock[]
    {

        new ForceBlock(33,new Vector3(30,100,300),new Vector3(10,50,5),new Vector3(100,0,0)),
        new ForceBlock(34,new Vector3(0,30,200),new Vector3(50,100,5),new Vector3(100,0,0)),
        new ForceBlock(36,new Vector3(11,87,240),new Vector3(20,50,5),new Vector3(100,0,0)),
        new ForceBlock(37,new Vector3(0,10,140),new Vector3(10,10,50),new Vector3(0,0,0)),
        new ForceBlock(38,new Vector3(0,10,300),new Vector3(10,10,50),new Vector3(0,0,0)),
        new ForceBlock(39,new Vector3(0,210,300),new Vector3(10,10,50),new Vector3(0,0,0))


    };


    // Use this for initialization
    void Start()
    {
        objArray = subArray;
        CheckBoolAndGenerate();
    }


    public class ForceBlock : StickAndBaseObjectGenerator
    {
        private static string thisObjectName = "ForceBlock";

        public ForceBlock(int stageNumber, Vector3 position, Vector3 scale, Vector3 rotation) : base(thisObjectName, stageNumber, position, scale, rotation)
        {

        }



    }
}
