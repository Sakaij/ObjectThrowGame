using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;
public class IncrementLife : MonoBehaviour {


    public void incrementLife()
    {
        Life.Instance.IncrementLife(1);
    }
}
