using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagmerScript : MonoBehaviour {
public void QuitApplication() {
        Application.Quit();
    }

    public void LoadScene(string myScene) {
        SceneManager.LoadScene(myScene);
    }
}
