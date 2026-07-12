using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuSelectManager : MonoBehaviour
{
    [SerializeField] private Button[] pauseButtons;
    [SerializeField] private Button[] homeButtons;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private PauseMenuManager menuManager;
 
    private int selectedIndex;
    private bool isActive;
    private Button[] buttons;

    private const int homeIndex = 2;

    public void StartSelect()
    {
        selectedIndex = 0;
        isActive = true;
        buttons = pauseButtons;

        if (buttons != null && buttons.Length > 0)
        {
            UpdateSelectedButtonSize();
        }
    }

    private void Update()
    {
        if (!isActive) return;
        
        SelectInput();
        
    }

    public void StopSelect()
    {
        isActive = false;
    }

    private void SelectMenu(int index)
    {
        buttons[index].onClick.Invoke();
    }

    private void SelectInput()
    {
        if (buttons == null || buttons.Length == 0) return;

        bool isChanged = false;

        if (buttons == pauseButtons)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = buttons.Length - 1;

                isChanged = true;
                audioManager.Select();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                selectedIndex++;
                if (selectedIndex >= buttons.Length) selectedIndex = 0;

                isChanged = true;
                audioManager.Select();
            }
        }
        else if(buttons == homeButtons)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = buttons.Length - 1;

                isChanged = true;
                audioManager.Select();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                selectedIndex++;
                if (selectedIndex >= buttons.Length) selectedIndex = 0;

                isChanged = true;
                audioManager.Select();
            }
        }

        if (isChanged)
        {
            UpdateSelectedButtonSize();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectMenu(selectedIndex);
            audioManager.Confirm();

            if(buttons == pauseButtons)
            {
                if(selectedIndex == homeIndex)
                {
                    buttons = homeButtons;
                    selectedIndex = 0;

                    UpdateSelectedButtonSize();
                }
            }
            else if(buttons == homeButtons)
            {
                if(selectedIndex == 0)
                {
                    menuManager.OnConfirmYes();
                }
                else
                {
                    menuManager.OnConfirmNo();

                    buttons = pauseButtons;
                    selectedIndex = homeIndex;
                    UpdateSelectedButtonSize();
                }
            }
        }
    }

    private void UpdateSelectedButtonSize()
    {
        for(int i=0; i<buttons.Length; i++)
        {
            if (buttons[i] == null) continue;

            if(i == selectedIndex)
            {
                buttons[i].Select();
                buttons[i].transform.localScale = new Vector3(1.2f, 1.2f, 1f);
            }
            else
            {
                buttons[i].transform.localScale = Vector3.one;
            }
        }
    }
}
