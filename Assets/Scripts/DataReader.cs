using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class DataReader 
{

    public delegate void OnDataRead(string header, float value);

    private StreamReader reader;
    private FileStream fileStream;
    private string[] headers;
    private int galaxyCount;

    public DataReader(string filename, int galaxiesToSpawn)
    {
        galaxyCount = galaxiesToSpawn;
        reader = new StreamReader(new MemoryStream((Resources.Load("final_objects") as TextAsset).bytes));
    }

    public GalaxyData[] Read(OnDataRead onDataRead)
    {
        List<GalaxyData> galaxyData = new List<GalaxyData>();

        bool header = true;
        int currentEntry = 0;

        while (!reader.EndOfStream && currentEntry <= galaxyCount)
        {
            currentEntry++;
            string input = reader.ReadLine();

            if (header)
            {
                header = false;
                headers = Split(input);
            }
            else
            {
                string[] values = Split(input);
                GalaxyData data = new GalaxyData();

                for (int i = 0; i < values.Length; i++)
                {
                    float floatValue = 0;

                    bool result = float.TryParse(values[i], out floatValue);

                    if (result)
                    {
                        onDataRead.Invoke(headers[i], floatValue);
                        data.SetData(headers[i], floatValue);
                    }
                }

                galaxyData.Add(data);
            }
        }

        return galaxyData.ToArray();
    }

    private string[] Split(string input)
    {
        return input.Split(',');
    }
}
