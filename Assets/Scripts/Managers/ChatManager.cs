using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;
using System;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    public string username;
    public int maxChatMessages = 100;
    //public Client MyClient;
    public GameObject chatPanel, textObject;
    public InputField chatInputBox;
    protected internal AppSettings chatAppSettings;
    public ChatClient chatClient;
    public string roomName;

    [SerializeField]
    List<ChatMessage> messageList = new List<ChatMessage>();

    public void Start()
    {
        username = PhotonNetwork.LocalPlayer.NickName;
        roomName = PhotonNetwork.CurrentRoom.Name;
        //DontDestroyOnLoad(this.gameObject);
        this.chatAppSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
        bool appIdPresent = !string.IsNullOrEmpty(this.chatAppSettings.AppIdChat);
        if (!appIdPresent)
        {
            Debug.LogError("You need to set the chat app ID in the PhotonServerSettings file in order to continue.");
        }

        this.Connect();
    }

    public void Connect()
    {
        this.chatClient = new ChatClient(this);
        this.chatClient.Connect(this.chatAppSettings.AppIdChat, "1.0", new Photon.Chat.AuthenticationValues(this.username));
    }

    public void OnConnected()
    {
        this.chatClient.Subscribe(new string[] { roomName });
    }

    void Update()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Service();
        }

        if ((Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)) && this.chatInputBox.text != "")
        {
            this.chatClient.PublishMessage(roomName, this.username + ": " + this.chatInputBox.text);
            this.chatInputBox.text = "";
        }
        else if (!chatInputBox.isFocused && Input.GetKeyDown(KeyCode.Return))
        {
            chatInputBox.ActivateInputField();
        }
    }

    public void SendMessageToChat(string text)
    {
        if (messageList.Count >= maxChatMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        ChatMessage newMessage = new ChatMessage();
        newMessage.text = text;
        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);

        chatInputBox.text = "";
    }

    public void OnDestroy()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Disconnect();
        }
    }

    public void OnApplicationQuit()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Disconnect();
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
    }

    public void OnDisconnected()
    {
    }

    public void OnChatStateChange(ChatState state)
    {
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        int msgCount = messages.Length;
        for (int i = 0; i < msgCount; i++)
        {
            string sender = senders[i];
            string msg = Convert.ToString(messages[i]);
            this.SendMessageToChat(msg);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        this.chatClient.PublishMessage(roomName, this.username + " has connected to the chat!");
    }

    public void OnUnsubscribed(string[] channels)
    {
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
    }

    public void OnUserSubscribed(string channel, string user)
    {
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
    }
}



[System.Serializable]
public class ChatMessage
{
    public string text;
    public Text textObject;
}