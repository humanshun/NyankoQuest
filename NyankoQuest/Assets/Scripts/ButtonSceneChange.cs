using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class ButtonSceneChange : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string sceneName;
    void Start()
    {
        button.onClick.AddListener(OnButtonClicked);
    }
    void OnButtonClicked()
    {
        SceneManager.LoadScene(sceneName);
    }
}
