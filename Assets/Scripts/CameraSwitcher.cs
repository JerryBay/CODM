using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    [System.Serializable]public class CameraSetting
    {
        public Vector3 _position;

        public int _fieldOfView;
    }

    public Dropdown _dropdown;

    public List<CameraSetting> _cameraSettings = new List<CameraSetting>();


    private void Awake()
    {
        ChangePosition(_dropdown.value);
        _dropdown.onValueChanged.AddListener(
            (index)=>ChangePosition(index)
            );
    }

    public void ChangePosition(int index)
    {
        CameraSetting setting = _cameraSettings[index];
        transform.position = setting._position;
        GetComponent<Camera>().fieldOfView = setting._fieldOfView;
    }
}
