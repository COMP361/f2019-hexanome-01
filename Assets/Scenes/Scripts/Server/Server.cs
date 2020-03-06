using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.IO;
public class Server : MonoBehaviour {



    public int port = 6321;
    private List<ServerClient> clients;
    private List<ServerClient> disconnectList;
    private TcpListener server;
    private bool serverStarted;

    private void Start() {
        clients = new List<ServerClient>();
        disconnectList = new List<ServerClient>();
        try {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            startListening();
            serverStarted = true;
            Debug.Log("Server started on port " + port.ToString());
        }
        catch (Exception e) {
            Debug.Log("Socket error: " + e.Message);
        }
    }

    private void Update() {

        if (!serverStarted) return;

        foreach (ServerClient sc in clients) {

            if (!isConnected(sc.tcp)) {

                sc.tcp.Close();
                disconnectList.Add(sc);
                continue;

            } else {

                NetworkStream ns = sc.tcp.GetStream();

                if (ns.DataAvailable) {

                    StreamReader reader = new StreamReader(ns, true);
                    string data = reader.ReadLine();
                    if (data != null) onIncomingData(sc, data);

                }
            }
        }

    }

    private void startListening() {
        server.BeginAcceptTcpClient(acceptTcpClient, server);
    }

    private bool isConnected(TcpClient p_client) {
        try { 
            if (p_client != null && p_client.Client != null && p_client.Client.Connected) {
                if (p_client.Client.Poll(0, SelectMode.SelectRead)) {
                    return !(p_client.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }
                return true;
            } else {
                return false;
            }
        }
        catch {
            return false;
        }
    }

    private void acceptTcpClient(IAsyncResult p_ar) {
        TcpListener listener = (TcpListener)p_ar.AsyncState;

        clients.Add(new ServerClient(listener.EndAcceptTcpClient(p_ar)));
        startListening();

        //send a messages to everyone, acknowledging a new connection
        //broadcast(clients[clients.Count - 1].clientName + " has connected", clients);
        broadcast("%NAME", new List<ServerClient>() { clients[clients.Count - 1] });
    }

    private void onIncomingData(ServerClient p_sc, string data) {
        if (data.Contains("%NAME")) {
            p_sc.clientName = data.Split('|')[1];
            broadcast(p_sc.clientName + " has connected!", clients);
            Debug.Log(p_sc.clientName);
            return;

        }
        broadcast(data, clients);
    }

    private void broadcast(string p_data, List<ServerClient> p_client) {
        foreach (ServerClient sc in p_client) {
            try {
                StreamWriter writer = new StreamWriter(sc.tcp.GetStream());
                writer.WriteLine(p_data);
                writer.Flush();
            } catch (Exception e){
                Debug.Log("Write Error: " + e.Message + " to client " + sc.clientName);
            }
        }
    }
}

public class ServerClient {
    public TcpClient tcp;
    public string clientName;

    public ServerClient(TcpClient p_tcp) {
        clientName = "guest";
        tcp = p_tcp;
    }

}