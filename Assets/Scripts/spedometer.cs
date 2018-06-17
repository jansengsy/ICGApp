using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spedometer : MonoBehaviour {

    [SerializeField]
    private GameObject speedometer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        speedometer.transform.position = transform.position + transform.forward * 10;
        speedometer.transform.LookAt(transform);
	}
}
