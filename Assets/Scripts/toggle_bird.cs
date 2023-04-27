using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggle_bird : MonoBehaviour
{
    public float bird_speed;
    public bool go_left = true;
    public Button delete_button;

    // Start is called before the first frame update
    void Start()
    {
        if (Debug.isDebugBuild) {
            delete_button.gameObject.SetActive(true);
        }

        StartCoroutine(toggleBird());
    }

    IEnumerator toggleBird()
    {
    	float bird_dir, rotateY = 0f;

    	while (true) {
    		bird_dir = go_left ? -bird_speed : bird_speed;
    		rotateY = go_left ? 0f : 180f;

    		this.gameObject.transform.position = new Vector3(
    			transform.position.x + bird_dir,
    			transform.position.y,
    			transform.position.z
    		);
    		this.gameObject.transform.rotation = Quaternion.Euler(0f, rotateY, -60f);

    		yield return new WaitForSeconds(0.001f);
    	}
    }

    public void delete()
    {
    	PlayerPrefs.DeleteAll();
    }
}
