using UnityEngine;
using System.Collections;

public class QuickAndDirtyCharControl : MonoBehaviour {

	public float moveSpeed = 6;
	Camera viewCamera;
	Vector3 velocity;
	float angle;

	void Start () {
		viewCamera = Camera.main;
	}

	void Update () {
		Vector3 mousePos = viewCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 1000 * Time.deltaTime);
	}
}