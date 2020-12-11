using UnityEngine;



public class MouseMoveToRotate : MonoBehaviour
{
    public CharacterManager _characterManager;
    private Transform _target;
    [Range(0, 20)]
    public int _rotateSpeed = 20;
        
    private Vector2 _preMousePos;
    private Vector2 _nowMousePos;

    private const int _orientationControlButton= 1;
    private const int _pedestalControlButton = 2;

    private void Start()
    {
        _target = _characterManager._currentCharacter.transform;
        _characterManager.AddSubscriber(
            (instance)=>ChangeCharacter(_characterManager._currentCharacter)
            );
    }

    public void ChangeCharacter(GameObject cha)
    {
        _target = cha.transform;
    }

    private void LateUpdate()
    {
        Vector2 mouseDelta;

        if (Input.GetMouseButtonDown(_orientationControlButton))
        {
            _preMousePos = Input.mousePosition;
        }
        _nowMousePos = Input.mousePosition;
        if (Input.GetMouseButton(_orientationControlButton))
        {
            mouseDelta = _nowMousePos - _preMousePos;
            _target.Rotate(0, -mouseDelta.x * _rotateSpeed * Time.deltaTime, 0);
        }

        _preMousePos = _nowMousePos;
    }
}

