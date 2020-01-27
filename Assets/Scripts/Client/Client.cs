using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.IO;
using System;

public class Client : MonoBehaviour {

    public ChatManager chatManager;
    public GameObject ChatContainer, ChatMessagePrefab;
    public GameObject IsConnectedLabel;
    private bool socketReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;


    private void Update() {
        if (socketReady) {
            if (stream.DataAvailable) {
                string data = reader.ReadLine();
                if (data != null) onIncomingData(data);
            }
        }
    }

    public void connectToServer() {
        if (socketReady) return;

        string host = "127.0.0.1";
        int port = 6321;

        string h;
        string user;
        int p;
        h = GameObject.Find("HostField").GetComponent<InputField>().text;
        int.TryParse(GameObject.Find("PortField").GetComponent<InputField>().text, out p);
        user = GameObject.Find("UsernameField").GetComponent<InputField>().text;
        if (h != "") host = h;
        if (p != 0) port = p;
        if (user != "") chatManager.username = user;

        try {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
            IsConnectedLabel.SetActive(true);

        } catch (Exception e) {
            Debug.Log("Socket Error: " + e.Message);
        }
    }

    private void onIncomingData(string data) {
        if (data == "%NAME") {
            Send("%NAME|" + chatManager.username);
            return;
        }
        chatManager.SendMessageToChat(data);
    }

    public void Send(string data) {
        if (!socketReady) return;
        writer.WriteLine(data);
        writer.Flush();
    }

}
