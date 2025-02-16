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

public class OrdersBase : MonoBehaviour
{
	public TMP_InputField contentDatabase;
	private Dictionary<int, Dictionary<string, string>> currentDatabase;

	// Start is called before the first frame update
	void Start()
    {
		ChangeDatabaseView();

	}


	public void ChangeDatabaseView()
	{
		currentDatabase = databasesData[(Database)Enum.Parse(typeof(Database), "ordersHistory")];

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
}
