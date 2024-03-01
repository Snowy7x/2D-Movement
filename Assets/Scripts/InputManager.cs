using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager singleton;
    
    Vector2 _moveDir;
    private bool _rMouse;
    private bool _lMouse;
    private bool _lShift;
    private bool _lCtrl;
    private bool _space;

    public Vector2 getMoveDir() => _moveDir;
    
    public bool getRightMouse() => _rMouse;
    public bool getLeftMouse() => _lMouse;
    public bool getLeftShift() => _lShift;
    public bool getLeftControl() => _lCtrl;

    public bool getSpace()
    {
        return _space;
    }

    private void Awake()
    {
        if (singleton)
        {
            Destroy(this);
            return;
        }

        singleton = this;
    }

    private void Update()
    {
        _moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _space = Input.GetButton("Jump");
    }
}
