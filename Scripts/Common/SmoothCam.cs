using UnityEngine;
using System.Collections;

public class SmoothCam : MonoBehaviour {


	//public Transform target;
	public GameObject player;
	public Vector3 offset;

	void Start ()
	{
		offset = transform.position - player.transform.position;
	}
	void Update()
	{
		//transform.position = new Vector3 (target.position.x + offset.x, target.position.y + offset.y,	offset.z);
		//transform.LookAt (target);
		transform.position = player.transform.position + offset;
	}

}
