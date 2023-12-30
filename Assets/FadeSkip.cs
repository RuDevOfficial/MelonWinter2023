using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSkip : MonoBehaviour
{
    public void Go() { SceneManager.LoadScene(2); }
}
