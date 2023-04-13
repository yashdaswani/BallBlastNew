using UnityEngine;
using TMPro;
public class ChatHandler : MonoBehaviour
{
    public static ChatHandler instance;
    public AuthManager authManager;

    //public TMP_InputField senderIf;
    public TMP_InputField textIf;

    public GameObject messagePrefab;
    public Transform messageCont;
    public Vector3 pos = Vector3.zero;
   
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        authManager.ListenForMessage(InstantiateMessage,Debug.Log);
        
    }


    public void InstantiateMessage(Message message)
    {
        var newMessage = Instantiate(messagePrefab, transform.position, Quaternion.identity);
        newMessage.transform.SetParent(messageCont);
        newMessage.GetComponent<TextMeshProUGUI>().text = $"{message.sender} : {message.text}";
        newMessage.transform.localScale = Vector3.one;
    }

    public void SendMessage()
    {
        name = PlayerPrefs.GetString("username");
        authManager.PostMessage(new Message(name , textIf.text) ,
            callback: () => Debug.Log(message: "message sent"), Debug.Log);
        textIf.text = "";
    }

}
