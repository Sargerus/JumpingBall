using System;
using UnityEngine;

public class Player : MonoBehaviour, ISetupObject
{
    private const float _upBound = 2.18f;
    private const float _bottomBound = 0.5f;
    private float time;

    private Vector3 _moveTarget;
    private Vector3 _cameraTopLeft;
    private Vector3 _cameraBottomRight;

    //private GameManager _gameManager;
    private PlatformsManager _platformManager;

    private void Awake()
    {

        //var gameManageGO = GameObject.FindGameObjectWithTag("GameManager");
        //if (gameManageGO)
        //    _gameManager = gameManageGO.GetComponent<GameManager>();

        _platformManager = GameObject.FindGameObjectWithTag("PlatformsContainer").GetComponent<PlatformsManager>();

        _cameraTopLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 3f));
        _cameraBottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 3f));

        _moveTarget = Vector3.zero;
    }

    public void SetupObject()
    {
        foreach (Transform transform in transform)
        {
            ISetupObject isetupobject = transform.GetComponent<ISetupObject>();
            if (isetupobject != null)
                isetupobject.SetupObject();
        }
    }

    private void Update()
    {
        if (transform.localPosition.y >= _upBound)
            MoveDown();

        HandleScreenTouch();
    }

    public void MoveUp(Transform transform = null)
    {
        time = _platformManager.GetTime(transform);
        LeanTween.moveY(gameObject, _upBound, time / 2).setEaseOutQuad();
    }

    public void MoveDown()
    {
        LeanTween.moveY(gameObject, _bottomBound, time / 2).setEaseInQuad();
    }

    private void HandleScreenTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                _moveTarget = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 2f));

                _moveTarget.x = Mathf.Clamp(_moveTarget.x, _cameraTopLeft.x + transform.localScale.x * 1.5f, _cameraBottomRight.x - transform.localScale.x * 1.5f);
                _moveTarget.z = transform.position.z;
                _moveTarget.y = transform.position.y;
            }
            else
            {
                _moveTarget = Vector3.zero;
            }
        }

        if (_moveTarget != Vector3.zero)
        {
            _moveTarget = Vector3.MoveTowards(transform.position, _moveTarget, 2f * Time.deltaTime);

            _moveTarget.z = -8f;
            transform.position = _moveTarget;
        }
    }
}
