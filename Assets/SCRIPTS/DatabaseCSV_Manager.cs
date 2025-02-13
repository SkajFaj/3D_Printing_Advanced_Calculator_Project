using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class DatabaseCSV_Manager : MonoBehaviour
{
	public static Dictionary<Database, Dictionary<int, Dictionary<string, string>>> databasesData = new();

	public enum Database
	{
		customers,
		printers,
		filaments,
		ordersHistory
	}

	// Start is called before the first frame update
	void Awake()
	{
		if (!Directory.Exists(CSVFile.path))
		{
			try
			{
				Directory.CreateDirectory(CSVFile.path);
			}
			catch (Exception)
			{
				throw;
			}
		}

        //LoadCSVFiles
        foreach (Database database in Enum.GetValues(typeof(Database)))
		{
			try
			{
				databasesData.Add(database, CSVFile.LoadFile($"{CSVFile.path}{database}.csv"));
			}
			catch (Exception)
			{
				throw;
			}
		}

	}



	//void Load_csvCustomers()
	//{
	//    if (csvCustomers != null)
	//    {
	//        string[] data = csvCustomers.text.Split(new char[] { '\n' });

	//        for (int i = 1; i < data.Length; i++) // Zaczynamy od 1, aby pomin¹æ nag³ówek
	//        {
	//            string[] fields = data[i].Split(new char[] { ',' });
	//            if (fields.Length == 7)
	//            {
	//                Person person = new Person();
	//                person.ID = int.Parse(fields[0]);
	//                person.FirstName = fields[1];
	//                person.LastName = fields[2];
	//                person.ContactNumber = fields[3];
	//                person.DateAdded = fields[4];
	//                person.OrdersAmount = int.Parse(fields[5]);
	//                person.Email = fields[6];

	//                people.Add(person);

	//                // Wyœwietlamy dane ka¿dej osoby
	//                Debug.Log("ID: " + person.ID +
	//                          ", FirstName: " + person.FirstName +
	//                          ", LastName: " + person.LastName +
	//                          ", ContactNumber: " + person.ContactNumber +
	//                          ", DateAdded: " + person.DateAdded +
	//                          ", OrdersAmount: " + person.OrdersAmount +
	//                          ", Email: " + person.Email);
	//            }
	//        }
	//    }
	//    else
	//    {
	//        Debug.LogError("Nie znaleziono pliku customers.csv!");
	//    }
	//}


	



}
