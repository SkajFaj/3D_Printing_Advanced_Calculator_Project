using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown startScreenDropdown;
	private const string startScreenKey = "StartScreen";


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

	}

    public void StartScreenSelector()
    {
        int currentIndex;
        currentIndex = startScreenDropdown.value;

        SaveStartScreen(currentIndex);
	}

	private void SaveStartScreen(int index)
	{
		PlayerPrefs.SetInt(startScreenKey, index);
		PlayerPrefs.Save();
	}

	
	
}
