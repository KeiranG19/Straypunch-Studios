using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public Menu currentMenu;

	public void Start()
	{
		ShowMenu(currentMenu);
	}

	public void ShowMenu(Menu menu)
	{
		if (currentMenu != null)
			currentMenu.isOpen = false;


		currentMenu = menu;
		currentMenu.isOpen = true;


	}
	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (currentMenu.parent_menu != null)
			{
				currentMenu.isOpen = false;
				currentMenu = currentMenu.parent_menu;
				currentMenu.isOpen = true;
			}

		}
	}

	public void loadScene(int scene)
	{
		Application.LoadLevel(scene);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

}

