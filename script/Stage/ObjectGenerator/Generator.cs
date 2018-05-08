using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;

/// <summary>
/// 障害物などにつけて使用
/// </summary>
public class ObjectGenerater : MonoBehaviour
{
    private static string directoryName = "GameObject/";
    public string objectName;
    public int stageNumber;
    public Vector3 position;
    public Vector3 scale;
    public Vector3 rotation;

    protected ObjectGenerater(string objectName, int stageNumber, Vector3 position, Vector3 scale, Vector3 rotation)
    {
        this.objectName = objectName;
        this.stageNumber = stageNumber;
        this.position = position;
        this.scale = scale;
        this.rotation = rotation;
    }


    public virtual void Generate()
    {
        GameObject obj = Resources.Load(directoryName + objectName, typeof(GameObject)) as GameObject;
        Instantiate(obj, position, Quaternion.Euler(rotation)).transform.localScale = scale;
    }


}


/// <summary>
/// オブジェクトにアタッチして使う
/// </summary>
public class ObjectGeneraterScript<T> : MonoBehaviour where T : ObjectGenerater
{
    protected T[] objArray;

    /// <summary>
    /// 今のステージが、Tを生成するステージかどうかをチェックして生成(Start関数で呼び出し)
    /// </summary>
    public virtual void CheckBoolAndGenerate()
    {
        int nowStage = MySceneManager.nowStageNumber;
        for (int count = 0; count < objArray.Length; count++)
        {
            if (objArray[count].stageNumber == nowStage)
            {
                objArray[count].Generate();
            }
        }
    }
}

/// <summary>
/// ポールと、床がセットなったものを扱うクラスにつける
/// </summary>
/// <typeparam name="T"></typeparam>
public class StickAndBaseObjectGenerator :MonoBehaviour
{
    private static string directoryName = "GameObject/";
    public string objectName;
    public int stageNumber;
    public Vector3 position;
    public Vector3 scale;
    public Vector3 rotation;

    ///objetcNameには、リソースフォルダーに保存されている実際の名前を入れる
    protected StickAndBaseObjectGenerator(string objectName, int stageNumber,Vector3 position, Vector3 scale, Vector3 rotation)
    {
        this.objectName = objectName;
        this.stageNumber = stageNumber;
        this.position = position;
        this.scale = scale;
        this.rotation = rotation;
    }


    public virtual void Generate()
    {
        GameObject obj = Resources.Load(directoryName + objectName, typeof(GameObject)) as GameObject;
        GameObject target = Instantiate(obj, position, Quaternion.identity) as GameObject;
        Transform floor, stick;
        Transform[] array = target.GetComponentsInChildren<Transform>();
        foreach (Transform obs in array)
        {
            if (obs.tag == "Base")
            {
                floor = obs;
                floor.localScale = scale;
                floor.rotation = Quaternion.Euler(rotation);
            }
            if (obs.tag == "Stick")
            {
                stick = obs;
            }
        }

        target.transform.position = position;
    }
}

public class  StickAndBaseGeneraterScript<T> :MonoBehaviour where T : StickAndBaseObjectGenerator
{
    protected T[] objArray;

    /// <summary>
    /// 今のステージが、Tを生成するステージかどうかをチェックして生成(Start関数で呼び出し)
    /// </summary>
    public virtual void CheckBoolAndGenerate()
    {
        int nowStage = MySceneManager.nowStageNumber;
        for (int count = 0; count < objArray.Length; count++)
        {
            if (objArray[count].stageNumber == nowStage)
            {
                objArray[count].Generate();
            }
        }
    }
}




