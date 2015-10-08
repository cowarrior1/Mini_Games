using UnityEngine;
using System.Collections;

public class buttonsToDisable : MonoBehaviour {

	public GameObject [] buttons;
	public GUIManager _GUI;

	// Use this for initialization
	void Start () {
		disableButtons();
	}

	void OnEnable() {
		disableButtons();
	}

	void disableButtons()
	{
		for (int i=0;i<buttons.Length;i++)
		{
			if(buttons[i] != null && buttons[i].collider != null)
			{
				buttons[i].collider.enabled = false;
			}
		}
	}

	void enableButtons()
	{
		for (int i=0;i<buttons.Length;i++)
		{
			if(buttons[i] != null && buttons[i].collider != null)
			{
				buttons[i].collider.enabled = true;
			}
		}
	}

	public void CloseButtonClickHandler()
	{
		enableButtons();

		if (this.gameObject.GetComponent<btnActionsConfigurationScreen>() != null)
			this.gameObject.GetComponent<btnActionsConfigurationScreen>().btnCloseClicked();

		if (this.gameObject.GetComponent<btnActionsConfigurationScreenOnPauseScren>() != null)
			this.gameObject.GetComponent<btnActionsConfigurationScreenOnPauseScren>().btnCloseClicked();

		if (this.gameObject.GetComponent<btnActionsConfigurationsScreenGame>() != null)
			this.gameObject.GetComponent<btnActionsConfigurationsScreenGame>().btnCloseClicked();

		if (this.gameObject.GetComponent<btnActionsFailScreen>() != null)
			this.gameObject.GetComponent<btnActionsFailScreen>().btnCloseClicked();

		if (this.gameObject.GetComponent<btnActionsPauseScreen>() != null)
			this.gameObject.GetComponent<btnActionsPauseScreen>().btnCloseClicked();

		if (this.gameObject.GetComponent<btnActionsTutorialScreenOnGame>() != null)
			this.gameObject.GetComponent<btnActionsTutorialScreenOnGame>().btnCloseClicked();

		if (this.gameObject.GetComponent<btnActionsTutorialScreen>() != null)
			this.gameObject.GetComponent<btnActionsTutorialScreen>().btnCloseClicked();

		if (this.gameObject.GetComponent<btnActionsQuitScreen>() != null)
			this.gameObject.GetComponent<btnActionsQuitScreen>().btnCloseClicked();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
