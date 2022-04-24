using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private Button startButton;
    private Button exitButton;
    private Label logText;
    // Start is called before the first frame update
    void Start()
    {
       VisualElement root = GetComponent<UIDocument>().rootVisualElement;
       startButton = root.Q<Button>("start-btn");
       exitButton = root.Q<Button>("exit-btn");
       logText = root.Q<Label>("log");

       exitButton.clicked += ShowMessage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void ShowMessage()
    {
        logText.text = "hello";
    }
}
