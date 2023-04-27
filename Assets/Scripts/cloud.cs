using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour
{
    public float cloud_speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnCloud());
    }

    IEnumerator spawnCloud()
    {
    	while (!GameObject.Find("background").GetComponent<background>().game_over) {
    		while (
    			GameObject.Find("background").GetComponent<background>().start_game
    			&& !GameObject.Find("background").GetComponent<background>().pause_game
    		) {
	    		this.gameObject.transform.position = new Vector3(
	    			transform.position.x,
	    			transform.position.y + cloud_speed,
	    			transform.position.z
	    		);

	    		yield return new WaitForSeconds(0.005f);
	    	}

	    	yield return new WaitForSeconds(0.01f);
    	}
    }

    void OnTriggerEnter(Collider other)
    {
        bool hitend = GameObject.Find("background").GetComponent<background>().hitend;

        if (other.gameObject.tag == "Player" && !hitend) {
            this.gameObject.GetComponent<SphereCollider>().isTrigger = false;

            foreach(Collider c in this.gameObject.GetComponents<Collider>()) {
                c.enabled = false;
            }

            GameObject.Find("background").GetComponent<background>().game_over = true;
            GameObject.Find("background").GetComponent<background>().start_game = false;

            if (Debug.isDebugBuild) {
                StartCoroutine(delayGame());
            }
        }
    }

    IEnumerator delayGame()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject.Find("background").GetComponent<background>().game_over = false;
        GameObject.Find("background").GetComponent<background>().start_game = true;
    }
}
