using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    GameManager GameManager;
    
    private void Awake()
    {
        this.GameManager = GameManager.Instance;
    }
        
    public void StartMatch()
    {
        GameManager.LoadMatchManager();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void loadScene(string scene)
	{
		
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}

}
