using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;


public static class CSVFile
{
    public static readonly string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)} \\3D Printing Advanced Calculator\\";

    public static Dictionary<int, Dictionary<string, string>> LoadFile(string path, char delimeter = ',')
    {
        using (StreamReader reader = new StreamReader(path))
        {
            string line = reader.ReadLine();
            if (line == null)
                return new();

            List<string> headers = line.Split(delimeter).ToList();
            Dictionary<int, Dictionary<string, string>> data = new();

            int index = 0;
            while ((line = reader.ReadLine()) != null)
            {
                if (line == "" || line == "\t" || line == "\n")
                    continue;

                string[] splitedLine = line.Split(delimeter);
                data.Add(int.Parse(splitedLine[0]), new());
                for (int i = 1; i < splitedLine.Length - 1; i++)
                {
                    data[int.Parse(splitedLine[0])].Add(headers[i], splitedLine[i]);
                }
                index++;
            }
            return data;
        }
    }
}
