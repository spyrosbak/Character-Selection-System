using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] characterPrefabs;

    private void Awake()
    {
        Debug.Log("Selected Character number: " + PlayerSelect.Instance.playerSelectionNumber);
        Instantiate(characterPrefabs[PlayerSelect.Instance.playerSelectionNumber], new Vector3(0, characterPrefabs[PlayerSelect.Instance.playerSelectionNumber].transform.position.y, 0), Quaternion.Euler(0, 180, 0));
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
}