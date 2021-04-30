using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class readData : MonoBehaviour
{
    // http://codesaying.com/unity-parse-excel-in-unity3d/
    // https://docs.unity3d.com/ScriptReference/TextAsset.html
    public int xAxisCol;
    public int yAxisCol;
    public int zAxisCol;
    public GameObject visualizer;
    public GameObject visualizationParent;
    public TextAsset csvFile;

    // using the comma makes this automatically a multidimenional array
    string[,] grid;

    string[] xColumn;
    string[] yColumn;
    string[] zColumn;

    private InputDevice targetDevice;

    // Start is called before the first frame update
    void Start()
    {
        // set up VR input devices
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }

        // retrieve desired column data
        (xColumn, yColumn, zColumn) = parseCSV(csvFile.text, xAxisCol, yAxisCol, zAxisCol);

        // generate objects in scene based on column data
        generateObjects(xColumn, yColumn, zColumn);
    }

    void Update()
    {
        // add variables to check if VR controller buttons are down in addition to just keyboard keys
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);

        if (primaryButtonValue)
        {
            Debug.Log("Pressing button");
        }

        // check for key pressed and reinstantiate objects per designated coordinate system
        // type 1 - axis shift "up"
        // type 2 - axis shift "down"
        if (Input.GetKeyDown("u") || primaryButtonValue)
        {
            // destroy current visualization
            foreach (Transform child in visualizationParent.transform) 
            {
                GameObject.Destroy(child.gameObject);
            }

            generateObjects(zColumn, xColumn, yColumn);
        } 
        else if (Input.GetKeyDown("i") || primaryButtonValue)
        {
            foreach (Transform child in visualizationParent.transform) 
            {
                GameObject.Destroy(child.gameObject);
            }

            generateObjects(yColumn, zColumn, xColumn);
        }
    }

    (string[], string[], string[]) parseCSV(string csvFileText, int xAxisCol, int yAxisCol, int zAxisCol)
    {
        // csvLines - array of csv ROWS (each row element separated by a comma)
        string[] csvRows = csvFileText.Split("\n"[0]);
        string[] csvColumnNames = csvRows[0].Split(',');

        // consider using list not arrar: var csvColumns = new List<string>();
        string[,] csvColumns = new string[csvColumnNames.Length, csvRows.Length];

        // we want to get COLUMNS of data since that is the way our data is organized
        // we'll need to generate arrays representing each column from arrays that represent each row
        // essentially, if we look at csvRows as a "matrix" we want the transpose
        // TODO - is there a more efficient way to do this? eg, some kind of library?
        for (int i = 1; i < csvRows.Length; i++)
        {
            // get current row
            string[] currentRow = csvRows[i].Split(',');

            // break up each row element into the appropriate column array
            for (int j = 0; j < currentRow.Length; j++) 
            {
                // TODO - be sure to catch empty cells
                // assgins each element to its appropriate column (flips row array)
                csvColumns[j, i] = currentRow[j];
            }
        }

        // grab the columns for each axis
        string[] xColumn = PopulateColumns(xAxisCol, csvColumns, csvRows);
        string[] yColumn = PopulateColumns(yAxisCol, csvColumns, csvRows);
        string[] zColumn = PopulateColumns(zAxisCol, csvColumns, csvRows);
        //string[] colorColumn = PopulateColumns(colorAxisCol, csvColumns, csvRows);

        return (xColumn, yColumn, zColumn);
    }

    string[] PopulateColumns(int columnNumber, string[,] csvColumns, string[] csvRows)
    {
        string[] column = new string[csvRows.Length];

        for (int i = 0; i < csvRows.Length; i++)
        {
            column[i] = csvColumns[columnNumber, i];
        }

        return column;
    }

    // TOOD - make spheres child of one prefab
    void generateObjects(string[] xColumn, string[] yColumn, string[] zColumn)
    {
        for (int i = 0; i < xColumn.Length; i++)
        {
            if (xColumn[i] != null && yColumn[i] != null && zColumn[i] != null)
            {
                // create game object
                GameObject obj = Instantiate(visualizer, new Vector3(float.Parse(xColumn[i]), float.Parse(yColumn[i]), float.Parse(zColumn[i])), Quaternion.identity);
                obj.transform.parent = visualizationParent.transform;
            }
        }
    }

    // helper function to get mean of array
    // float mean(string[] colorColumn)
    // {
    //     float sum = 0;

    //     for (int i = 0; i < colorColumn.Length; i++)
    //     {
    //         if (colorColumn[i] != null)
    //         {
    //             sum += float.Parse(colorColumn[i]);
    //         } 
    //     }

    //     return sum / colorColumn.Length;
    // }
}
