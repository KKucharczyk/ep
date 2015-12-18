using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject textBox;

	public Text text;

	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	public bool isActive;
	public static bool stopPlayerMovement;


	// Use this for initialization
	void Start () {
		if (textFile != null) {
			textLines = textFile.text.Split('\n');
		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}

		if (isActive) {
			EnableTextBox ();
		} else {
			DisableTextBox();
		}

	}

	void Update() {

		if (!isActive) {
			return;
		}

		text.text = textLines[currentLine];

		if (Input.GetKeyDown (KeyCode.Return)) {
			currentLine++;
		}

		if (currentLine >= endAtLine) {
			DisableTextBox();
		}

	}

	public void EnableTextBox() {
		textBox.SetActive(true);
		isActive = true;
		TextBoxManager.stopPlayerMovement = true;
	}

	public void DisableTextBox() {
		textBox.SetActive(false);
		isActive = false;
		TextBoxManager.stopPlayerMovement = false;
	}

	public void ReloadScript(TextAsset _textFile) {
		if (_textFile != null) {
			textLines = new string[1];
			textLines = _textFile.text.Split('\n');
		}
	}

}
