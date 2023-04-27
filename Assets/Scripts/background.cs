using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class background : MonoBehaviour
{
    public float bird_speed, cloud_range, box_collider;
    public bool start_game = false, game_over = false, pause_game = false, hitend = true;
	public GameObject snow, bird, cloud;
	public Text countdown, instruction, points_text, highestpoints_text, numlives_text;
    public Button continue_button;

    private string firstplay;
    private bool change_direction = false;
    private int numlives = 0, points = 0, highestpoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        int width, height;

        numlives = PlayerPrefs.HasKey("numlives") ? PlayerPrefs.GetInt("numlives") : 100;
        highestpoints = PlayerPrefs.HasKey("highestpoints") ? PlayerPrefs.GetInt("highestpoints") : 0;
        firstplay = PlayerPrefs.GetString("firstplay");

        if (!Debug.isDebugBuild) {
            numlives_text.text = numlives + " live(s) left";
            highestpoints_text.text = highestpoints + " highest point(s)";
        }

        if (firstplay == "") {
            PlayerPrefs.SetString("firstplay", "nope");
            PlayerPrefs.SetInt("numlives", 100);
        }

        width = Screen.width;
        height = Screen.height;

        if (width == 750 && height == 1334) {
            // iPhone 8, 7, 6s, 6, SE 2nd
            cloud_range = 2.28f;
            box_collider = 6.8f;
        } else if (width == 1080 && height == 1920) {
            // iPhone 8+, 7+, 6+
            cloud_range = 2.28f;
            box_collider = 6.8f;
        } else if (width == 1125 && height == 2436) {
            // iPhone X, 11 Pro
            cloud_range = 1.78f;
            box_collider = 5.5f;
        } else if (width == 1242 && height == 2208) {
            // iPhone 6s+, 8+
            cloud_range = 2.28f;
            box_collider = 2.3f;
        } else if (width == 640 && height == 1136) {
            // iPhone SE 1st
            cloud_range = 2.28f;
            box_collider = 6.8f;
        } else if (width == 828 && height == 1792) { // continue here
            // iPhone 11, Xr
            cloud_range = 1.78f;
            box_collider = 5.5f;
        } else if (width == 1242 && height == 2688) {
            // iPhone 11 Pro Max, Xs Max
            cloud_range = 1.78f;
            box_collider = 5.5f;
        } else if (width == 1170 && height == 2532) {
            // iPhone 12, 12 Pro
            cloud_range = 1.78f;
            box_collider = 5.5f;
        } else if (width == 1284 && height == 2778) {
            // iPhone 12 Pro Max
            cloud_range = 1.78f;
            box_collider = 5.5f;
        }

        GameObject.Find("background").GetComponent<BoxCollider>().size = new Vector3(box_collider, 7.83f, 1f);

        StartCoroutine(moveBirdDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (
            this.gameObject.GetComponent<background>().start_game
            && !this.gameObject.GetComponent<background>().pause_game
        ) {
            bool left = GameObject.Find("bird").GetComponent<bird>().go_left;

            if (
                Input.touchCount == 1 && !change_direction
                &&
                !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)
            ) {
                if (!left) {
                    bird.transform.rotation = Quaternion.Euler(0f, 0f, -60f);
                } else if (bird.transform.rotation.y == 0f) {
                    bird.transform.rotation = Quaternion.Euler(0f, 180f, -60f);
                }

                left = !left;
                change_direction = true;
            } else if (Input.touchCount == 0) {
                change_direction = false;
            }

            GameObject.Find("bird").GetComponent<bird>().go_left = left;
        } 
    }

    IEnumerator moveBirdDown()
    {
    	while (bird.transform.position.y > 3.5f) {
    		bird.transform.position = new Vector3(
    			bird.transform.position.x, 
    			bird.transform.position.y - bird_speed,
    			bird.transform.position.z
    		);

    		yield return new WaitForSeconds(0.01f);
    	}

    	StartCoroutine(startCountdown());
    }

    IEnumerator startCountdown()
    {
    	int num = 3;
    	bool shrink = false;

    	countdown.gameObject.SetActive(true);
        instruction.gameObject.SetActive(true);

    	while (num > 0) {
    		if (shrink) {
    			countdown.fontSize -= 10;
    		} else {
    			countdown.fontSize += 10;
    		}

    		yield return new WaitForSeconds(0.03f);

    		if (countdown.fontSize <= 10) {
    			shrink = false;

    			num--;

    			countdown.text = num.ToString();
			} else if (countdown.fontSize >= 110) {
				shrink = true;
			}
    	}

        countdown.gameObject.SetActive(false);
        instruction.gameObject.SetActive(false);
        bird.transform.rotation = Quaternion.Euler(0f, 0f, -60f);

        start_game = true;

    	StartCoroutine(startSpawnClouds());
        StartCoroutine(startSpawnSnows());
    }

    IEnumerator startSpawnClouds()
    {
        while (!this.gameObject.GetComponent<background>().game_over) {
            while (
                this.gameObject.GetComponent<background>().start_game
                && !this.gameObject.GetComponent<background>().pause_game
            ) {
                Instantiate(cloud, new Vector3(Random.Range(-cloud_range, cloud_range), -5.2f, -0.25f), Quaternion.identity);

                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(0.01f);
        }

        continue_button.gameObject.SetActive(false);

        if (points > highestpoints) {
            highestpoints_text.text = points + " highest point(s)";

            if (!Debug.isDebugBuild) {
                PlayerPrefs.SetInt("highestpoints", points);
                PlayerPrefs.SetInt("numlives", (numlives - 1));
            }
        }
    }

    IEnumerator startSpawnSnows()
    {
        Instantiate(snow, new Vector3(0f, -10.4f, -0.05f), Quaternion.identity);

        while (!this.gameObject.GetComponent<background>().game_over) {
            while (
                this.gameObject.GetComponent<background>().start_game
                && !this.gameObject.GetComponent<background>().pause_game
            ) {
                Instantiate(snow, new Vector3(0f, -10.4f, -0.05f), Quaternion.identity);

                yield return new WaitForSeconds(2f);
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        string tagName = other.gameObject.tag;
        GameObject fieldObject = other.gameObject;
        bool go_left;
        float back, back_factor = 0.05f;

        if (tagName == "Respawn") {
            foreach(Collider c in other.gameObject.GetComponents<Collider>()) {
                c.enabled = false;
            }
            
            Destroy(fieldObject);

            points++;

            points_text.text = points + " point(s)";
        } else if (tagName == "Player") {
            go_left = !GameObject.Find("bird").GetComponent<bird>().go_left;
            back = go_left ? -back_factor : back_factor;

            GameObject.Find("bird").GetComponent<bird>().transform.position = new Vector3(
                GameObject.Find("bird").GetComponent<bird>().transform.position.x + back,
                GameObject.Find("bird").GetComponent<bird>().transform.position.y,
                GameObject.Find("bird").GetComponent<bird>().transform.position.z
            );
            GameObject.Find("bird").GetComponent<bird>().go_left = go_left;

            if (Debug.isDebugBuild && !hitend) {
                GameObject.Find("background").GetComponent<background>().start_game = false;

                StartCoroutine(delayGame());
            }
        }
    }

    IEnumerator delayGame()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject.Find("background").GetComponent<background>().start_game = true;
    }

    public void continue_game()
    {
        this.gameObject.GetComponent<background>().pause_game = false;

        continue_button.gameObject.SetActive(false);
    }
}
