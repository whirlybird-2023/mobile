using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_background : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("width: " + Screen.width + ", height: " + Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
    	bool go_left;

        if (other.gameObject.tag == "Toggler") {
        	go_left = GameObject.Find("bird").GetComponent<toggle_bird>().go_left;
        	
        	GameObject.Find("bird").GetComponent<toggle_bird>().go_left = !go_left;
        }
    }
}
