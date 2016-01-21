using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{

	private Animator animator;
	private CanvasGroup canvasGroup;
	public Menu parent_menu;

	public bool isOpen
	{
		get{return animator.GetBool("isOpen");}
		set{animator.SetBool("isOpen", value);}
	}

	public void Awake()
	{
		animator = GetComponent<Animator>();
		canvasGroup = GetComponent<CanvasGroup>();

		var rect = GetComponent<RectTransform>();
		rect.offsetMax = rect.offsetMin = new Vector2(0, 0);

	}

	public void Update()
	{
		if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
		{
			canvasGroup.blocksRaycasts = canvasGroup.interactable = false;
		}
		else
		{
			canvasGroup.blocksRaycasts = canvasGroup.interactable = true; 
		}
	/*	//if(!parent_menu == null)
	//	{
			if(Input.GetKeyDown (KeyCode.Escape))
			{
				manager.currentMenu = parent_menu;

			}
		//}
	*/}
}
