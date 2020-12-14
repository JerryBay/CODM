using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffection : MonoBehaviour
{
    public Button[] _buttons;
    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].onClick.AddListener(ChangeColor);
        }
    }

    private void ChangeColor()
    {
        for (int j = 0; j < _buttons.Length; j++)
        {
            _buttons[j].GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
        }
        GameObject button = EventSystem.current.currentSelectedGameObject;
        button.GetComponent<RawImage>().color = new Color(0.5f, 0.5f, 0.5f, 1);
    }
}
