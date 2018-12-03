using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTTF : MonoBehaviour {

    Vector3 player;
    Vector3 cursor;
    Vector3 pos;
    float maxScreenPoint = .8f;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousepos = Input.mousePosition * maxScreenPoint + new Vector3(Screen.width, Screen.height, 0)*((1f-maxScreenPoint)*.5f);
        cursor = Camera.main.ScreenToWorldPoint(mousepos);
        player = GameManager.Instance.player.transform.position;

        pos = (player - cursor) / 2;
        transform.position = pos;
    }
}
