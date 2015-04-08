using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controls : MonoBehaviour 
{
	public GameObject dialogUI;
	public Text dialogText;
	public string[] testDialog;
	int dialogIndex = 0;

	Transform grabbedObject;
	Vector3 oldPosition;
	Quaternion oldRotation;
	Vector3 oldScale;

	// Use this for initialization
	void Start () 
	{
		dialogText.text = testDialog [0];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire1"))
		{
			// test
			// once dialog is exhausted
			if (dialogIndex+1 < testDialog.Length)
			{
				dialogIndex++;
				dialogText.text = testDialog[dialogIndex];
			}
			else
			{
				dialogUI.SetActive (false);
				if (grabbedObject == null) 
				{
					selectObject ();
				}
				else
				{
					resetObject ();
				}
			}
		}
		if (grabbedObject != null) 
		{
			rotateObject ();
		}
	}

	void selectObject ()
	{
		// select
		Ray ray = new Ray (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector3.forward);
		RaycastHit hitInfo;
		Physics.Raycast (ray, out hitInfo);
		if (hitInfo.collider == null) return;
		grabbedObject = hitInfo.collider.transform;

		// save old transformation
		oldPosition = grabbedObject.position;
		oldRotation = grabbedObject.rotation;
		oldScale = grabbedObject.localScale;
		
		// move
		grabbedObject.position = -Vector3.forward*50;
		grabbedObject.localScale *= 5;
	}

	void rotateObject ()
	{
		grabbedObject.RotateAround (grabbedObject.position, Vector3.right, Input.GetAxis ("Mouse Y") * Time.deltaTime * 90);
		grabbedObject.RotateAround (grabbedObject.position, Vector3.up, -Input.GetAxis ("Mouse X") * Time.deltaTime * 90);
	}

	void resetObject ()
	{
		grabbedObject.position = oldPosition;
		grabbedObject.rotation = oldRotation;
		grabbedObject.localScale = oldScale;

		grabbedObject = null;
	}
}
