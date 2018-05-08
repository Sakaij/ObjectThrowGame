using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;


/// <summary>
/// 穴が開いた障害物につけるやつ
/// </summary>
public class HoleObstacleScript : ObjectGeneraterScript<HoleObstacleScript.HoleObstacle> {

    private HoleObstacle[] inf =
    {
        new HoleObstacle(22,new Vector3(0,-100,200), new Vector3(50,50,50), new Vector3(-1,0,0)),
        new HoleObstacle(23,new Vector3(0,0,200), new Vector3(50,50,50), new Vector3(-1,0,0)),
        new HoleObstacle(24,new Vector3(0,-20,-20), new Vector3(50,50,50), new Vector3(-1,0,0)),
        new HoleObstacle(27,new Vector3(0,-50,100), new Vector3(50,50,50), new Vector3(-1,0,0)),
        new HoleObstacle(29,new Vector3(0,-50,100), new Vector3(50,50,50), new Vector3(-1,0,0)),
        new HoleObstacle(29,new Vector3(20,-130,-100), new Vector3(50,50,50), new Vector3(-1,0,0)),
    };

    //slope(-4,-38,10),(60,0,0),(10,30,3):
    public void Start()
    {
        objArray = inf;
        CheckBoolAndGenerate();
    }





    public class HoleObstacle : ObjectGenerater {

        private static string thisObjectName = "HoleObstacle";


        public HoleObstacle(int stageNumber,Vector3 position,Vector3 scale, Vector3 rotation) : base(thisObjectName,stageNumber,position,scale,rotation)
        {

        }


    }




	
}
