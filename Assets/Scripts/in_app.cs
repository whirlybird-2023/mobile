using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class in_app : MonoBehaviour
{
	public Text numlives_text;
	public int product_name;

	private string firstplay;
	private int numlives;

	void Start()
	{
		firstplay = PlayerPrefs.GetString("firstplay");

		if (firstplay == "") {
            PlayerPrefs.SetString("firstplay", "nope");
            PlayerPrefs.SetInt("numlives", 100);

            numlives = 100;
        } else {
        	numlives = PlayerPrefs.GetInt("numlives");
        }

        numlives_text.text = numlives + " live(s)";
	}

    public void buy_complete(Product purchased)
    {
    	if (purchased.transactionID != null) {
    		numlives += product_name;

    		PlayerPrefs.SetInt("numlives", numlives);
    		numlives_text.text = numlives + " live(s)";
    	}
    }

    public void get_product()
    {
    	numlives += 5;

    	PlayerPrefs.SetInt("numlives", numlives);
    	numlives_text.text = numlives + " live(s)";
    }

    public void buy_failed(Product purchased, PurchaseFailureReason failure)
    {

    }
}
