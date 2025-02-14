using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using static DatabaseCSV_Manager;

public class Calculator : MonoBehaviour
{
	// SELEKTOR MAR�Y
	Dictionary<int, int> marginTypes = new Dictionary<int, int>()
	{
		{0, 50},
		{1, 35},
		{2, 45},
		{3, 25},
		{4, 0 }
	};
	public void MarginSelector(int index)
	{
		if (index == 4)
			customMarginSwitcher.SetActive(false);
		else
			customMarginSwitcher.SetActive(true);

		margin = marginTypes[index];
		return;
	}

	public GameObject customMarginSwitcher;
	public TMP_InputField customMargin;
	private float margin = 0f;

	private int printerID, customerID, filamentID;



	//SELEKTOR FILAMENTU
	private float filamentPricePerKilogram;

	public TMP_Dropdown filamentDropdown;



	//SELEKTOR KLIENTA
	private string customer;
	public TMP_Dropdown customerDropdown;



	//SELEKTOR DRUKARKI
	private float printerPowerConsumption;
	public TMP_Dropdown printerDropdown;



	//WAGA WYDRUKU
	private float printWeight;
	public TMP_InputField printWeight_InputField;



	//STAWKA ENERGETYCZNA
	private float electricityCost;
	public TMP_InputField electricityCost_InputField;



	//CZAS DRUKOWANIA
	private float printingTimeInMinutes;
	public TMP_InputField printingTime_InputField;




	//KOSZT FINALNY
	private float finalCost;
	public TMP_InputField finalCostReturn;





	// Start is called before the first frame update
	void Start()
	{
		// Ustawienie pola tekstowego na akceptowanie wy��cznie liczb dziesi�tnych.
		customMargin.contentType = TMP_InputField.ContentType.DecimalNumber;


		//Wczytanie danych z bazy klient�w
		List<string> names = new();
		FillDropdown(Database.customers, (x) => names.Add($"{x["LastName"]} {x["FirstName"]}"),
															 () => { names.Sort(); customerDropdown.AddOptions(names); names.Clear(); });
		//Wczytanie danych z bazy filament�w
		FillDropdown(Database.filaments, (x) => names.Add($"{x["Brand"]} {x["Type"]}"),
															 () => { names.Sort(); filamentDropdown.AddOptions(names); names.Clear(); });
		//Wczytanie danych z bazy drukarek
		FillDropdown(Database.printers, (x) => names.Add($"{x["Brand"]} {x["Model"]}"),
															() => { names.Sort(); printerDropdown.AddOptions(names); names.Clear(); });

		UpdateDropdowns();
		MarginSelector(0);
	}

	public void UpdateDropdowns()
	{
		customerID = databasesData[Database.customers].FirstOrDefault(x => customerDropdown.options[customerDropdown.value].text == $"{x.Value["LastName"]} {x.Value["FirstName"]}").Key;
		filamentID = databasesData[Database.filaments].FirstOrDefault(x => filamentDropdown.options[filamentDropdown.value].text == $"{x.Value["Brand"]} {x.Value["Type"]}").Key;
		printerID = databasesData[Database.printers].FirstOrDefault(x => printerDropdown.options[printerDropdown.value].text == $"{x.Value["Brand"]} {x.Value["Model"]}").Key;
	}

	/// <summary>
	/// Funkcja uzupe�niaj�ca wybrany dropdown
	/// </summary>
	/// <param name="database"></param>
	/// <param name="eachEntryAction"></param>
	/// <param name="lastAction"></param>
	void FillDropdown(Database database, System.Action<Dictionary<string, string>> eachEntryAction, System.Action lastAction)
	{
		foreach (var entry in databasesData[database])
		{
			eachEntryAction.Invoke(entry.Value);
		}
		lastAction.Invoke();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (!customMarginSwitcher.activeSelf)
		{
			float.TryParse(customMargin.text, out margin);
		}
		float.TryParse(printWeight_InputField.text, out printWeight);
		float.TryParse(electricityCost_InputField.text, out electricityCost);
		float.TryParse(printingTime_InputField.text, out printingTimeInMinutes);





		// OBLICZANIE FNALNEGO KOSZTU DRUKU
		finalCost = (((float.Parse(databasesData[Database.printers][printerID]["PowerConsumption"]) / 1000f) * 
					(printingTimeInMinutes / 60f) * electricityCost) + //koszt pr�du
					(printWeight / 1000f) * float.Parse(databasesData[Database.filaments][filamentID]["PricePerKilogram"])) * //koszt materia�u
					(1 + (margin / 100)); //mar�a


		//wy�wietlenie wyniku w GUI
		finalCostReturn.text = finalCost.ToString(); 


	}

	public void SaveToOrdersHistory()
	{
		//CustomerID,PrinerID,FilamentID,Magin,PrintWeight,ElectricityCost,PrintingTime,FinalCost


	}


}
