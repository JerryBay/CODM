using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public List<GameObject> _characters;

    public GameObject _currentCharacter;

    public int _curChaIndex;

    public Dropdown _swapper;

    public delegate void SwapSubscriber(int index);

    public List<SwapSubscriber> _subscribers = new List<SwapSubscriber>();

    private void Awake()
    {
        SwapCharacter(_swapper.value);
        AddSubscriber(SwapCharacter);
        _swapper.onValueChanged.AddListener(index=>
        InvokeSubscribers(index)
        );
    }

    private void InvokeSubscribers(int index)
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.Invoke(index);
        }
    }

    public void AddSubscriber(SwapSubscriber swapSubscriber)
    {
        _subscribers.Add(swapSubscriber);
    }

    private void SwapCharacter(int index)
    {
        _curChaIndex = index;
        foreach (var cha in _characters)
        {
            cha.SetActive(false);
        }
        _characters[index].SetActive(true);
        _currentCharacter = _characters[index];
    }
}
