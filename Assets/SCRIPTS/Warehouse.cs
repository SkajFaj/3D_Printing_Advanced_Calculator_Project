using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
		databasesData[(Database)Enum.Parse(typeof(Database), dropdownDatabase.options[dropdownDatabase.value].text)] = databasesData[(Database)Enum.Parse(typeof(Database), dropdownDatabase.options[dropdownDatabase.value].text)].OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
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
		selectID_InputField.text = "1";
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
		if (int.Parse(selectID_InputField.text) < 1)
			selectID_InputField.text = "1";

		OnDropdownFieldChanged();
	}

	public void SaveDatabase()
	{
		string databaseName = dropdownDatabase.options[dropdownDatabase.value].text;
		string oldFileFullPath = CSVFile.path + databaseName + ".csv";
		string newFilefullPath = CSVFile.path + databaseName + "TEMP.csv";
		CSVFile.SaveFile(newFilefullPath, databasesData[(Database)Enum.Parse(typeof(Database), databaseName)]);
		File.Delete(oldFileFullPath);
		File.Move(newFilefullPath, oldFileFullPath);
	}

	public void NewEntry()
	{
		int selectedID;
		if (!int.TryParse(selectID_InputField.text, out selectedID))
			selectedID = 1;

		#region newIndex

		int newIndex = selectedID;
		if (currentDatabase.Count == 0)
			newIndex = 1;
		else if (currentDatabase.ContainsKey(selectedID))
		{
			// Get biggest index + 1
			//newIndex = currentDatabase.Max(x => x.Key) + 1;

			// Get next biggest index
			newIndex = currentDatabase.Where(x => x.Key >= selectedID).Max(x => x.Key) + 1;
			int lastKey = currentDatabase.Where(x => x.Key >= selectedID).ElementAt(0).Key;
			foreach (var key in currentDatabase.Keys.Where(x => x >= selectedID))
			{
				if (key - lastKey == 1)
				{
					lastKey = key;
					continue;
				}
				else if (key - lastKey > 1)
				{
					newIndex = lastKey+1;
					break;
				}
			}
		}

		#endregion

		currentDatabase.Add(newIndex, currentDatabase.ElementAt(0).Value.ToDictionary(entry => entry.Key, entry => ""));
		selectID_InputField.text = newIndex.ToString();
		dropdownField.value = 0;

		ChangeDatabaseView();
	}

	public void DeleteEntry()
	{
		if (selectID_InputField.text == "")
			return;

		currentDatabase.Remove(int.Parse(selectID_InputField.text));
		ChangeDatabaseView();
		if (currentDatabase.Count > 0)
		{
			try
			{
				selectID_InputField.text = currentDatabase.Where(x => x.Key < int.Parse(selectID_InputField.text)).Max(x => x.Key).ToString();
			}
			catch
			{
				try
				{
					selectID_InputField.text = currentDatabase.Where(x => x.Key > int.Parse(selectID_InputField.text)).Min(x => x.Key).ToString();
				}
				catch
				{
					selectID_InputField.text = "1";
				}
			}
		}
		else
		{
			selectID_InputField.text = "1";
		}
		OnDropdownFieldChanged();
		//currentDatabase
	}

}
