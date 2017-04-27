using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour {

    public GameObject Target;
    public GameObject Item;

	void Start () { }
	
	void Update () {
        Item.transform.position = Target.transform.position;
    }
}
