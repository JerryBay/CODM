using UnityEngine;


[ExecuteInEditMode]
public class MouseMoveToRotate : MonoBehaviour
{
    public CharacterManager _characterManager;
    private Transform _target;
    [Range(0, 20)]
    public int _rotateSpeed = 20;
        
    private float _preMousePosX;
    private float _nowMousePosX;

    private void Start()
    {
        _target = _characterManager._currentCharacter.transform;
        _characterManager.AddSubscriber(
            (instance)=>ChangeCharacter(_characterManager._currentCharacter)
            );
        _preMousePosX = Input.mousePosition.x;
    }

    public void ChangeCharacter(GameObject cha)
    {
        _target = cha.transform;
    }

    private void LateUpdate()
    {
        _nowMousePosX = Input.mousePosition.x;

        if (Input.GetMouseButton(1))
        {
            float deltaX = (_nowMousePosX - _preMousePosX);
            _target.Rotate(0, -deltaX * _rotateSpeed * Time.deltaTime, 0);
        }

        _preMousePosX = _nowMousePosX;
    }
}

