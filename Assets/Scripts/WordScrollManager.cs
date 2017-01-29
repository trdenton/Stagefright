using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordScrollManager : Singleton<WordScrollManager> {

    public const float START_X = 520f;
    public const float WORD_INTERVAL = 5f;
    public float spawnNewWordTimer = 0f;

    private float RIGHT_POS = 100f;
    private float LEFT_POS = 0f;

    List<Text> wordObjects = new List<Text>();

	List<string> wordsList = new List<string> ();
	private int wordIndex=0;

	public TextAsset speech;

    public Text focusedWord = null;
    public Text typedWordTextMesh;
    int focusedLetterIndex = 0;

    void Start () {
        spawnNewWordTimer = WORD_INTERVAL;

        typedWordTextMesh.text = "";

		//populate wordsList with strings from speech

		String allTheText = speech.text;

		foreach (String line in allTheText.Split("\n"[0])) {
			
			foreach (String word in line.Split(" "[0])) {
				wordsList.Add (word);
			}
		}


        //SpawnNewWord();
    }
	
	void Update () {
        spawnNewWordTimer += Time.deltaTime;

        if (spawnNewWordTimer >= WORD_INTERVAL)
        {
            spawnNewWordTimer -= WORD_INTERVAL;
            SpawnNewWord();
        }

        for(int i = 0; i < wordObjects.Count; i++)
        {
            if(UpdateWord(wordObjects[i]))//removed
                i--;
        }

        HandleLetterInput();
    }

    void HandleLetterInput()
    {
        if (Input.inputString != "")
        {
            char tmp = Input.inputString[0];
            Debug.Log(tmp);
            if(focusedWord != null && Char.ToLower(focusedWord.text.ToCharArray()[focusedLetterIndex]) == tmp)
            {
                focusedLetterIndex++;
                UpdateTypedWordText();
                if (focusedLetterIndex > focusedWord.text.Length -1)
                {
                    focusedWord.color = Color.green; //succeeded
                }
            }
        }
    }

    string typedText = "";
    void UpdateTypedWordText()
    {
        typedText = "";
        for (int i = 0; i < focusedLetterIndex; i++)
        {
            typedText += focusedWord.text.ToCharArray()[i];
        }
        typedWordTextMesh.text = typedText;
    }

    bool UpdateWord(Text t)
    {
        t.transform.position += Vector3.left * 1f;

        if (t.transform.position.x < -300f)
        {
            wordObjects.Remove(t);
            t.gameObject.DestroySelf();

            return true;
        }

        if (t != focusedWord && t.transform.localPosition.x < RIGHT_POS + t.preferredWidth / 2f && !(t.transform.localPosition.x < LEFT_POS - (t.preferredWidth / 2f)))
        {
            focusedLetterIndex = 0;
            t.color = Color.white;
            focusedWord = t;
            UpdateTypedWordText();
        }

        if (t == focusedWord && t.transform.localPosition.x < LEFT_POS - (t.preferredWidth / 2f))
        {
            if (focusedLetterIndex < focusedWord.text.Length)
            {
                focusedWord.color = Color.red; //failed
            }
            focusedLetterIndex = 0;
            UpdateTypedWordText();
            focusedWord = null;
        }

        return false;
    }

    void SpawnNewWord()
    {
        Text newText = Helpers.CreateInstance<Text>("WordObject", this.transform, true);

        newText.transform.SetLocalPositionX(START_X);
        wordObjects.Add(newText);

        newText.text = wordsList[wordIndex];
		wordIndex++;
    }
}
