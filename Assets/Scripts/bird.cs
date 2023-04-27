using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bird : MonoBehaviour
{
    public float bird_speed;
	public bool go_left = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(moveBird());
    }

    IEnumerator moveBird()
    {
    	while (!GameObject.Find("background").GetComponent<background>().game_over) {
	    	while (
	    		GameObject.Find("background").GetComponent<background>().start_game
	    		&& !GameObject.Find("background").GetComponent<background>().pause_game
	    	) {
                if (go_left) {
                    transform.rotation = Quaternion.Euler(0f, 0f, -60f);
	    			transform.position = new Vector3(
	    				transform.position.x - bird_speed,
	    				transform.position.y,
	    				transform.position.z
	    			);
	    		} else {
                    transform.rotation = Quaternion.Euler(0f, 180f, -60f);
	    			transform.position = new Vector3(
	    				transform.position.x + bird_speed,
	    				transform.position.y,
	    				transform.position.z
	    			);
	    		}

	    		yield return new WaitForSeconds(0.0001f);
	    	}

	    	yield return new WaitForSeconds(0.005f);
    	}
    }
}
