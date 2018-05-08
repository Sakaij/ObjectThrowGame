using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeScript : ObjectGeneraterScript<SlopeScript.Slope> {

    private Slope[] subArray =
    {
        new Slope(24, new Vector3(-4,-42,-15), new Vector3(10,20,3),new Vector3(60,0,0)),
        new Slope(25, new Vector3(0,3,120),new Vector3(2,200,3),new Vector3(90,0,0)),
        new Slope(25, new Vector3(0,20,120),new Vector3(2,200,3),new Vector3(90,0,0)),
        new Slope(26, new Vector3(5,3,122),new Vector3(2,200,3),new Vector3(90,182,0)),
        new Slope(26, new Vector3(5,20,122),new Vector3(2,200,3),new Vector3(90,182,0)),
        new Slope(27, new Vector3(-5,-80,20),new Vector3(7,200,3),new Vector3(80,0,0)),
        new Slope(28, new Vector3(0,0,150), new Vector3(7,200,3),new Vector3(90,0,0)),
        new Slope(28, new Vector3(0,30,300), new Vector3(7,200,3),new Vector3(80,0,0)),
        new Slope(28, new Vector3(0,52,400), new Vector3(7,10,3) ,new Vector3(0,0,0)),
        new Slope(29, new Vector3(-5,-80,20),new Vector3(7,200,3),new Vector3(80,0,0)),
        new Slope(30, new Vector3(0,-20,0), new Vector3(100,200,3),new Vector3(80,0,0)),
        new Slope(33, new Vector3(10,50,225),new Vector3(30,130,3),new Vector3(40,0,0)),
        new Slope(35, new Vector3(4,10,180),new Vector3(1,300,5),new Vector3(90,1,0)),
    };

    private void Start()
    {
        objArray = subArray;
        CheckBoolAndGenerate();
    }


    public class Slope : ObjectGenerater
    {
        private static string thisObjectName = "OneSlope";

        public Slope(int stageNumber, Vector3 position, Vector3 scale, Vector3 rotation) : base(thisObjectName, stageNumber, position, scale, rotation)
        {

        }
    }
}
