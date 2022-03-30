using UnityEngine;
using System.Collections;

public class QuickAndDirtyCharControl : MonoBehaviour {

	public float moveSpeed = 6;

	//Rigidbody rigidbody;
	Camera viewCamera;
	Vector3 velocity;

	void Start () {
		//rigidbody = GetComponent<Rigidbody> ();
		viewCamera = Camera.main;
	}

	void Update () {
		Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
		transform.LookAt (mousePos + Vector3.up * transform.position.y);
		velocity = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")).normalized * moveSpeed;
	}

	void FixedUpdate() {
		GetComponent<Rigidbody2D>().MovePosition(new Vector2(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y) + new Vector2(velocity, velocity) * Time.fixedDeltaTime);
	}
}