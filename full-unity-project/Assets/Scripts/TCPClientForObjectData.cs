using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
public class TCPClientForObjectData : MonoBehaviour
{
    [SerializeField] private string server;
    [SerializeField] GameObject objWithInputComponent;
    private string message = null;
    [SerializeField] private ushort port;

    public void ButtonClickHandler()
    {
        RetrieveTextValueToSend();
        string jsonData = GetJSONDataFromServer();
        ObjectManager.instance.InstantiateObjectsFromJSON(jsonData);
    }

    private void RetrieveTextValueToSend()
    {
        message = objWithInputComponent.GetComponent<InputField>().text;
        Debug.Log(message);

    }

    public string GetJSONDataFromServer()
    {
        // working sample to send text:
        byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
        TcpClient client = new TcpClient(server, port);
        NetworkStream stream = client.GetStream();
        // Send the message to the connected TcpServer.
        stream.Write(data, 0, data.Length);

        string returnData = "";
        if (stream.CanRead)
        {
            byte[] bytes = new byte[client.ReceiveBufferSize];
            stream.Read(bytes, 0, (int)client.ReceiveBufferSize);
            returnData = System.Text.Encoding.UTF8.GetString(bytes);
        }

        stream.Close();
        client.Close();

        return returnData;
    }
}