using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuSelectManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private AudioManager audioManager;
    private int selectedIndex;
    private bool isActive;

    public void StartSelect()
    {
        selectedIndex = 0;
        isActive = true;

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

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex--;
            if(selectedIndex < 0) selectedIndex = buttons.Length - 1;

            isChanged = true;
            audioManager.Select();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex++;
            if (selectedIndex >= buttons.Length) selectedIndex = 0;

            isChanged = true;
            audioManager.Select();
        }

        if (isChanged)
        {
            UpdateSelectedButtonSize();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectMenu(selectedIndex);
            audioManager.Confirm();
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
