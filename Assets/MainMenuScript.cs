using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }
    public void LoadScene(string levelString)
    {
        SceneManager.LoadScene(levelString);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
