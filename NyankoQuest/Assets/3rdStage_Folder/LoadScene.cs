using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{ 
    public string NextScene;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(click);
    
    }
    void click()
    {
        if (!string.IsNullOrEmpty(NextScene))
        {
            SceneManager.LoadScene(NextScene);
        }
        else
        {
            Debug.LogError("シーン名が指定されていません。");
        }
    }
}
