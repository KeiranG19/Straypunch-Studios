using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UImanager : MonoBehaviour {

	public int delay = 4;				// Time until the ui will start to fade
	public float fadeSpeed = 2;			// How quickly the ui elements will fade
	public EventSystem buttons;
	private CanvasGroup visibility;		// Referance to the canvasGroup component
	public GameObject topButton;
	//private PlayerController player;	

	void Start ()
	{
		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		visibility = GetComponent<CanvasGroup>();
		visibility.alpha = 0;
		StartCoroutine( showUI());

	}

	private IEnumerator showUI()
	{
		yield return new WaitForSeconds(delay);
		visibility.alpha =  Mathf.Lerp(0, 255, fadeSpeed * Time.deltaTime);
		buttons.SetSelectedGameObject (topButton);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
	
	public void loadScene(int scene)
	{
		Application.LoadLevel(scene);
	}
}
