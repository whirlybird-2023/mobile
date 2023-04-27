using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scene_nav : MonoBehaviour
{
	public Text nolives;

    public void scene_navigate(string scene_name)
    {
    	int numlives = 0;
    	bool valid_scene = true;
    	string startplay = "";

    	if (scene_name == "game") {
    		numlives = PlayerPrefs.GetInt("numlives");
    		startplay = PlayerPrefs.GetString("startplay");

    		if (numlives == 0 && startplay != "") {
    			valid_scene = false;
    		}
    	}

    	if (valid_scene) {
    		SceneManager.LoadScene(scene_name);
    	} else {
    		nolives.gameObject.SetActive(true);
    	}
    }
}
