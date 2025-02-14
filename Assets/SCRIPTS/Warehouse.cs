using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEditor;
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
	public TMP_Dropdown dropdownField;

	private int printerID, customerID, filamentID, orderID;
	private Dictionary<int, Dictionary<string, string>> currentDatabase;
	private string currentFieldName;
	private string lastIDSelected = "1";
	// Start is called before the first frame update
	void Start()
	{
		selectID_InputField.contentType = TMP_InputField.ContentType.DecimalNumber;

		//Wczytanie danych z bazy klientów
		List<string> names = new();
		FillDropdown();
		ChangeDatabaseView();
		FillFieldDropdown();
		if (selectID_InputField.text != string.Empty)
			OnDropdownFieldChanged();
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
	public void FillFieldDropdown()
	{
		// Fill dropdown with fields names
		List<string> names = new();
		for (int i = 0; i < currentDatabase.ElementAt(0).Value.Count; i++)
		{
			names.Add(currentDatabase.ElementAt(0).Value.ElementAt(i).Key);
		}
		dropdownField.ClearOptions();
		dropdownField.AddOptions(names);
	}

	public void ResetID()
	{
		selectID_InputField.text = "";
	}

	public void OnDropdownFieldChanged()
	{
		if (selectID_InputField.text == "" || !currentDatabase.ContainsKey(int.Parse(selectID_InputField.text)))
			return;

		currentFieldName = dropdownField.options[dropdownField.value].text;
		editValue_InputField.text = currentDatabase[int.Parse(selectID_InputField.text)][currentFieldName];
	}

	public void OnTextInputValueChanged()
	{
		if (selectID_InputField.text == "" || !currentDatabase.ContainsKey(int.Parse(selectID_InputField.text)))
			return;

		currentDatabase[int.Parse(selectID_InputField.text)][currentFieldName] = editValue_InputField.text;
		ChangeDatabaseView();
	}

	public void OnTextInputIDChanged()
	{
		if (selectID_InputField.text == "" || !currentDatabase.ContainsKey(int.Parse(selectID_InputField.text)))
		{
			editValue_InputField.text = "";
			return;
		}

		//if (!currentDatabase.ContainsKey(int.Parse(selectID_InputField.text)))
		//	selectID_InputField.text = lastIDSelected;
		lastIDSelected = selectID_InputField.text;
		OnDropdownFieldChanged();
	}

	public void SaveDatabase()
	{
		transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.parent = null;
	}

	public void NewEntry()
	{
		int newIndex = currentDatabase.ContainsKey(int.Parse(selectID_InputField.text)) ? currentDatabase.Max(x => x.Key) + 1 : int.Parse(selectID_InputField.text);
		currentDatabase.Add(newIndex, currentDatabase.ElementAt(0).Value.ToDictionary(entry => entry.Key, entry => ""));
		selectID_InputField.text = newIndex.ToString();
		lastIDSelected = newIndex.ToString();
		dropdownField.value = 0;

		ChangeDatabaseView();
	}

	public void DeleteEntry()
	{
		if (selectID_InputField.text == "")
			return;

		currentDatabase.Remove(int.Parse(selectID_InputField.text));
		ChangeDatabaseView();
		selectID_InputField.text = "";
		lastIDSelected = "1";
		OnDropdownFieldChanged();
		//lastIDSelected
		//currentDatabase
	}

}
