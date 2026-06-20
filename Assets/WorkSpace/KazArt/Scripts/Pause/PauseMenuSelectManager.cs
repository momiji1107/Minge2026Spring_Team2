using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuSelectManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private int selectedIndex;
    private bool isActive;

    public void startSelect()
    {
        selectedIndex = 0;
        isActive = true;

        if (buttons != null && buttons.Length > 0)
        {
            buttons[selectedIndex].Select();
        }
    }

    private void Update()
    {
        if(isActive)
        {
            selectInput();
        }
    }

    public void stopSelect()
    {
        isActive = false;
    }

    private void selectMenu(int index)
    {
        buttons[index].onClick.Invoke();
    }

    private void selectInput()
    {
        if (!isActive || buttons.Length == 0) return;

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex--;
            if(selectedIndex < 0) selectedIndex = buttons.Length - 1;

            buttons[selectedIndex].Select();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex++;
            if (selectedIndex >= buttons.Length) selectedIndex = 0;

            buttons[selectedIndex].Select();
            Debug.Log(selectedIndex);
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            selectMenu(selectedIndex);
        }
    }
}
