using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //Loads GameScene
    public void GoToGameScene()
    {
        SceneManager.LoadScene("GameScene"); 
    }
}
