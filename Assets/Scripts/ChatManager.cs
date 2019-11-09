using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public string username;

    public int maxChatMessages = 100;

    public GameObject chatPanel, textObject;
    public InputField chatInputBox;

    [SerializeField]
    List<ChatMessage> messageList = new List<ChatMessage>();

    void Start()
    {
        
    }

    void Update()
    {
        if(chatInputBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(username + ": " + chatInputBox.text);
                chatInputBox.text = "";
            }
        }
        else
        {
            if(!chatInputBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatInputBox.ActivateInputField();
            }
        }
    }

    public void SendMessageToChat(string text)
    {
        if(messageList.Count >= maxChatMessages)
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
    }
}

[System.Serializable]
public class ChatMessage
{
    public string text;
    public Text textObject;
}
