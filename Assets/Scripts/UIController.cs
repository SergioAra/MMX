using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


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

       exitButton.clicked += ExitGame;
       startButton.clicked += StartGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void  ExitGame()
    {

        #if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;

        #else

            Application.Quit();

        #endif  
    }

    void  StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    } 
}
