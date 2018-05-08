using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;

public class Bound : MonoBehaviour {

    public int boundContloler= 10;
    /// <summary>
    /// 入ってきたオブジェクトの速度を計測し、強さに応じて、y軸に力を加える
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            SoundPlayer.Instance.PlayOneShot(SoundPlayer.jumpSound);
            float boundPower = collision.collider.gameObject.transform.parent.GetComponent<Rigidbody>().velocity.z * boundContloler;
            collision.collider.gameObject.transform.parent.GetComponent<Rigidbody>().AddForce(0, boundPower,0);
        }
    }
}
