using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAssets : MonoBehaviour
{
    public List<GameObject> _gos;

    private void OnEnable()
    {
        foreach (var go in _gos)
        {
            go.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (var go in _gos)
        {
            go.gameObject.SetActive(false);
        }
    }


}
