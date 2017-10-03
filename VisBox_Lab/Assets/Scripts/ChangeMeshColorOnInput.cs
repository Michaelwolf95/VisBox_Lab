using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMeshColorOnInput : MonoBehaviour
{
    public MeshRenderer _meshRend;
    public int mouseButtonIndex = 0;
    public Color color = Color.red;
	// Use this for initialization
	void Start () {
        if (!_meshRend) _meshRend = this.GetComponent<MeshRenderer>();
	}
	
	void Update ()
    {
		if(MiddleVRInput.GetButtonDown((uint)mouseButtonIndex))
        {
            if(_meshRend)
            {
                _meshRend.material.color = color;
            }
            Debug.Log("Pressed");
        }
        if (MiddleVRInput.GetButton((uint)mouseButtonIndex))
        {
            Debug.Log("Hold");
        }
        if (MiddleVRInput.GetButtonUp((uint)mouseButtonIndex))
        {
            Debug.Log("Released");
        }

    }
}
