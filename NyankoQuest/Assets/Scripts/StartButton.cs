using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        if (button != null)
        {
            button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClick);
        }
    }

    void OnClick()
    {
        SceneManager.LoadScene("First stage");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
