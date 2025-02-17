using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBuildDisplay : MonoBehaviour
{
    //UI Text element in the Inspector
    public TMP_Text versionText;
    public TMP_Text gameNameText;
    public TMP_Text companyNameText;

    void Start()
    {
        // Get the version number from the PlayerSettings
        string version = Application.version;
        string gameName = Application.productName;
        string companyName = Application.companyName;


        // Display the version number in the UI Text element
        versionText.text = "Version: " + version;
        gameNameText.text = gameName;
        companyNameText.text = "By: " + companyName;
    }
}
