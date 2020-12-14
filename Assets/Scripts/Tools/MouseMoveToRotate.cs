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

    private Vector3 _distance;
    public Camera _camera;
    public float _minDist;
    public float _maxDist;

    [Range(0.5f,5f)]
    public float ScrollSensitivity=1f;

    private void Start()
    {
        _target = _characterManager._currentCharacter.transform;
        _distance = _target.localPosition - _camera.transform.localPosition;
        _characterManager.AddSubscriber(
            (instance)=> 
            { 
                ChangeCharacter(_characterManager._currentCharacter);
            }
            );
    }

    public void ChangeCharacter(GameObject cha)
    {
        _target = cha.transform;
    }


    private void LateUpdate()
    {
        Vector2 mouseDelta;
        Transform cameraTr = _camera.transform;
        _distance = _target.localPosition - _camera.transform.localPosition;

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

        float moveDist = _distance.magnitude;

        float ratio = moveDist / (_maxDist - _minDist);


        cameraTr.Translate(Input.mouseScrollDelta.y * _distance * ratio * Time.deltaTime * ScrollSensitivity);

        _preMousePos = _nowMousePos;

    }
}

