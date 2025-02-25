using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ControlNScore : MonoBehaviour
{
    [SerializeField] private GameObject balloon;
    [SerializeField] private UnityEngine.UI.Slider _slider;
    private SerialPort stream;
    [SerializeField] private string portName = "COM3"; // Set your port name
    [SerializeField] private int baudRate = 9600;
    //SerialPort stream = new SerialPort("COM3", 19200);

    private string[] strData = new string[7];
    private float f1, f2, f3, qw, qx, qy, qz;

    private float netGrip;
    public delegate void NetGripChangedHandler(float netGrip);
    public event NetGripChangedHandler OnNetGripChanged;
    void Awake()
    {
        //stream.Open(); //Open the Serial Stream.
        // Initialize serial port
        stream = new SerialPort(portName, baudRate);
        stream.Open();
    }

    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (stream.IsOpen)
        {
            try
            {
                strData = stream.ReadLine().Split(',');//Read the information  

                f1 = float.Parse(strData[0]);
                f2 = float.Parse(strData[1]);
                f3 = float.Parse(strData[2]);
                qw = float.Parse(strData[3]);
                qx = float.Parse(strData[4]);
                qy = float.Parse(strData[5]);
                qz = float.Parse(strData[6]);

                netGrip = (f1 + f2 + f3)/3 ;
                _slider.value = netGrip;
                OnNetGripChanged?.Invoke(netGrip); // Raise the event
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Error reading from serial port: " + e.Message);
            }
        }
    }

    private void OnDestroy()
    {
        stream.Close();
    }
    void OnApplicationQuit()
    {
        // Close the serial port when the application quits
        if (stream != null && stream.IsOpen)
        {
            stream.Close();
        }
    }
}
