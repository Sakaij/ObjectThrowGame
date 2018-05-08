using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : ObjectGeneraterScript<WallScript.WallObstacle> {
    private WallObstacle[] subArray =
    {
        new WallObstacle(31,new Vector3(0,100,100),new Vector3(100,100,3),new Vector3(0,0,0)),
        new WallObstacle(32,new Vector3(0,170,300),new Vector3(100,200,3),new Vector3(0,0,0)),
        new WallObstacle(33,new Vector3(30,200,200),new Vector3(100,300,3),new Vector3(0,0,0)),
        new WallObstacle(35,new Vector3(0,170,100),new Vector3(100,300,3),new Vector3(0,0,0)),
        new WallObstacle(36,new Vector3(0,190,100),new Vector3(100,300,3),new Vector3(0,0,0)),
        new WallObstacle(37,new Vector3(0,50,170),new Vector3(100,100,3),new Vector3(0,0,0)),
        new WallObstacle(37,new Vector3(0,180,170),new Vector3(100,100,3),new Vector3(0,0,0)),
        new WallObstacle(38,new Vector3(0,50,340),new Vector3(100,100,3),new Vector3(0,0,0)),
        new WallObstacle(38,new Vector3(0,180,340),new Vector3(100,100,3),new Vector3(0,0,0)),
        new WallObstacle(39,new Vector3(0,250,340),new Vector3(100,100,3),new Vector3(0,0,0)),

    };
    private void Start()
    {
        objArray = subArray;
        CheckBoolAndGenerate();
    }


    public class WallObstacle : ObjectGenerater
    {
        private static string thisObjectName = "Wall";

        public WallObstacle(int stageNumber, Vector3 position, Vector3 scale, Vector3 rotation) : base(thisObjectName, stageNumber, position, scale, rotation)
        {

        }



    }
}
