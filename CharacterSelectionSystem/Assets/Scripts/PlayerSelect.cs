using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    public static PlayerSelect Instance;

    public Transform characterHolder;
    public Button nextButton;
    public Button previousButton;
    public Text characterNameText;
    public int playerSelectionNumber;
    public GameObject[] characters;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(characters[playerSelectionNumber]);
    }

    private void Start()
    {

        playerSelectionNumber = 0;
    }

    private void Update()
    {
        characterNameText.text = characters[playerSelectionNumber].name;
    }

    public void NextPlayer()
    {
        playerSelectionNumber += 1;
        if(playerSelectionNumber >= characters.Length)
        {
            playerSelectionNumber = 0;
        }

        StartCoroutine(Rotate(Vector3.up, characterHolder, 90, 1f));
        nextButton.interactable = false;
        previousButton.interactable = false;
    }

    public void PreviousPlayer()
    {
        playerSelectionNumber -= 1;
        if(playerSelectionNumber < 0)
        {
            playerSelectionNumber = characters.Length - 1;
        }

        StartCoroutine(Rotate(Vector3.up, characterHolder, -90, 1f));
        nextButton.interactable = false;
        previousButton.interactable = false;
    }

    public void OnSelect()
    {
        Hashtable playerSelectionProp = new Hashtable { {Selection.PLAYER_SELECTION_NUMBER, playerSelectionNumber} };
        Debug.Log("Selected character number is: " + playerSelectionProp[Selection.PLAYER_SELECTION_NUMBER]);

        SceneManager.LoadScene("MainScene");
    }

    public void OnExit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

    private IEnumerator Rotate(Vector3 axis, Transform charactersTransform, float angle, float duration)
    {
        duration = 1f;

        Quaternion initialRotation = charactersTransform.rotation;
        Quaternion finalRotation = charactersTransform.rotation * Quaternion.Euler(axis * angle);

        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            charactersTransform.rotation = Quaternion.Slerp(initialRotation, finalRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        charactersTransform.rotation = finalRotation;
        nextButton.interactable = true;
        previousButton.interactable = true;
    }
}