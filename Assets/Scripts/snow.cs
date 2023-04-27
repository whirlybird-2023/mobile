using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snow : MonoBehaviour
{
	public float snow_speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(moveSnow());
    }

    IEnumerator moveSnow()
    {
    	while (!GameObject.Find("background").GetComponent<background>().game_over) {
    		while (
    			GameObject.Find("background").GetComponent<background>().start_game
    			&& !GameObject.Find("background").GetComponent<background>().pause_game
    		) {
	    		this.gameObject.transform.position = new Vector3(
	    			transform.position.x,
	    			transform.position.y + snow_speed,
	    			transform.position.z
	    		);

	    		if (this.gameObject.transform.position.y >= 10.6f) {
	    			Destroy(this.gameObject);
	    		}

	    		yield return new WaitForSeconds(0.005f);
	    	}

	    	yield return new WaitForSeconds(0.01f);
    	}
    }
}
