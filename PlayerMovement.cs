using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed;//take input from userscript in game later
    [SerializeField]
    private float _rotationSpeed;//take input from userscript in game later


    private Rigidbody2D _rigidbody;
    private Vector2 _inputMovement;
    private Vector2 _smoothMovement;
    private Vector2 _smoothMovementVelocity;
    
    private void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotaionInDirection();
    }
    private void SetPlayerVelocity(){
        _smoothMovement = Vector2.SmoothDamp(_smoothMovement, _inputMovement, ref _smoothMovementVelocity, .1f); // for smooth movement

        _rigidbody.velocity= _smoothMovement*_speed;
    }
    private void RotaionInDirection(){
        // if(_inputMovement != Vector2.zero){
        //     float targetAngle = Mathf.Atan2(_smoothMovement.y, _smoothMovement.x) * Mathf.Rad2Deg;
        //     transform.rotation = Quaternion.Euler(0,0,targetAngle - 90);
        // }
        if(_inputMovement!=Vector2.zero){
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothMovement);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            _rigidbody.MoveRotation(rotation);
        }
    }
    private void OnMove(InputValue inputValue){
        _inputMovement = inputValue.Get<Vector2>();
    }
}
