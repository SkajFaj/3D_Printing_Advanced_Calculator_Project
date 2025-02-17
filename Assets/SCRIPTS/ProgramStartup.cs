using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramStartup : MonoBehaviour
{
	public GameObject[] menus;
	public string startScreenKey = "StartScreen";

	// Start is called before the first frame update
	void Start()
    {
		LoadStartScreen();

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void LoadStartScreen()
	{
		int currentIndex;
		if (PlayerPrefs.HasKey(startScreenKey))
		{
			currentIndex = PlayerPrefs.GetInt(startScreenKey);
		}
		else
		{
			currentIndex = 0;
		}

		menus[currentIndex].SetActive(true);
	}
}
