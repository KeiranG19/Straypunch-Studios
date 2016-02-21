using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UImanager : MonoBehaviour {

	public Menu currentMenu;			// current open menu
	public int delay = 4;				// Time until the ui will start to fade
	public int fadeSpeed = 2;			// How quickly the ui elements will fade
	public bool visible = false;		// If the UI is visible
	
	private CanvasGroup visibility;		// Referance to the canvasGroup component
	//private PlayerController player;	

	void Start ()
	{
		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		visibility = GetComponent<CanvasGroup>();
		visibility.alpha = 0;
	}

	void Update ()
	{
		if(visible == true)
		{
			StartCoroutine( showUI());

		}
	}

	public void ShowMenu(Menu menu)
	{
		if (currentMenu != null)
			currentMenu.isOpen = false;
		
		
		currentMenu = menu;
		currentMenu.isOpen = true;

	}

	private IEnumerator showUI()
	{
		visibility.alpha = 1;
		yield return new WaitForSeconds(delay);
		visibility.alpha =  Mathf.Lerp(visibility.alpha, 0, fadeSpeed * Time.deltaTime);
		visible = false;

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
