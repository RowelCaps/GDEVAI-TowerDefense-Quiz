using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical, 0f, moveHorizontal * -1);

        Vector3 targetPos = movement + transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, 5 * Time.deltaTime);
	}
}
