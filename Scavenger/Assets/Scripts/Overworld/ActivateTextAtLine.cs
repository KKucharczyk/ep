using UnityEngine;
using System.Collections;

public class ActivateTextAtLine : MonoBehaviour {

	public TextAsset text;

	public int startLine;
	public int endLine;

	public bool requireButtonPress;
	private bool waitForPress;

	public TextBoxManager textBoxManager;

	public bool destroyWhenActivated;

	// Use this for initialization
	void Start () {
	
		textBoxManager = FindObjectOfType<TextBoxManager> ();
		Application.LoadLevel("Overworld");

	}
	
	// Update is called once per frame
	void Update () {
	
		if (waitForPress && Input.GetKeyDown (KeyCode.J)) {
			textBoxManager.ReloadScript(text);
			textBoxManager.currentLine = startLine;
			textBoxManager.endAtLine = endLine;
			textBoxManager.EnableTextBox();
			
			if(destroyWhenActivated)
			{
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			if(requireButtonPress){
				waitForPress = true;
				return;
			}

			textBoxManager.ReloadScript(text);
			textBoxManager.currentLine = startLine;
			textBoxManager.endAtLine = endLine;
			textBoxManager.EnableTextBox();

			if(destroyWhenActivated)
			{
				Destroy (gameObject);
			}
		}
		
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			waitForPress = false;
		}
	}

}
