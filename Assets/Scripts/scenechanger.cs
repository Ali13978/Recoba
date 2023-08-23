using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class scenechanger : MonoBehaviour
{
    public void ChangeScene(string changeto)
    {
        SceneManager.LoadScene(changeto);
        Debug.Log("changeto");
    }

}