using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MENU : MonoBehaviour
{
    // Start is called before the first frame update
    public string levelToLoad;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void load_map(){
        SceneManager.LoadScene(levelToLoad);
    }
}
