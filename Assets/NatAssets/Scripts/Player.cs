using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 7f;
    //[SerializeField] private float jumpHeight = 3f;

    private bool _isGrounded;
    private bool _canJump;
    
    void Start()
    {
        gameInput.OnJump += GameInput_OnJump;
    }

    private void GameInput_OnJump(object sender, EventArgs e)
    {
        if (_canJump)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector3.down, 1.0f);
        Debug.Log(_isGrounded);
        if(_isGrounded) _canJump = true;
        
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, 0);
        
        float moveDistance = moveSpeed * Time.deltaTime;

        if (_isGrounded)
        {
            transform.position += moveDirection * moveDistance;
        }
        //transform.position += moveDirection * moveDistance;

        Debug.Log(moveDirection.x);
        


    }
}
