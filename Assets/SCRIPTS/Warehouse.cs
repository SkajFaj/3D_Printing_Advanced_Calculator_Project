using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static DatabaseCSV_Manager;

public class Warehouse : MonoBehaviour
{
	public TMP_InputField contentDatabase;

	public TMP_InputField selectID_InputField;
	public TMP_Text selectID_InputField_Placeholder;

	public TMP_InputField editValue_InputField;
	public TMP_Text editValue_InputField_Placeholder;



	public TMP_Dropdown dropdownDatabase;

	private int printerID, customerID, filamentID, orderID;
	private Dictionary<int, Dictionary<string, string>> currentDatabase;

	// Start is called before the first frame update
	void Start()
	{
		selectID_InputField.contentType = TMP_InputField.ContentType.DecimalNumber;

		//Wczytanie danych z bazy klientów
		List<string> names = new();
		FillDropdown();
		ChangeDatabaseView();

	}
	



	// Update is called once per frame
	void FixedUpdate()
	{


	}

	void FillDropdown()
	{
		List<string> names = new();
		foreach (var database in databasesData)
		{
			names.Add(database.Key.ToString());
		}
		dropdownDatabase.AddOptions(names);
	}

	public void ChangeDatabaseView()
	{
		currentDatabase = databasesData[(Database)Enum.Parse(typeof(Database), dropdownDatabase.options[dropdownDatabase.value].text)];
		
		// Convert the currentDatabase dictionary to a readable string format
		StringBuilder sb = new StringBuilder();
		foreach (var outerPair in currentDatabase)
		{
			sb.AppendLine($"ID: {outerPair.Key}");
			foreach (var innerPair in outerPair.Value)
			{
				sb.AppendLine($"  {innerPair.Key}: {innerPair.Value}");
			}
		}
		contentDatabase.text = sb.ToString();
	}

	void LoadDatabase()
	{

	}

	void SaveDatabase()
	{

	}

	void DeleteDatabase()
	{

	}

}
