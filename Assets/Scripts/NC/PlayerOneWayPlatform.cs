using System.Collections;
using UnityEngine;

//most of this code was pulled from this video https://www.youtube.com/watch?v=7rCUt6mqqE8
public class PlayerOneWayPlatform : MonoBehaviour
{
    private const string GROUND_LAYER = "GroundLayer";
    
    private GameObject _currentOneWayPlatform;
    [SerializeField] CapsuleCollider2D _playerCollider;
    [SerializeField] PlayerRefactor _player;
    

    private void Update()
    {
        //if the player is dropping then disable the current collision
        if (_player.IsDropping())
        {
            Debug.Log("Platform_Update_Dropping");
            Debug.Log(_currentOneWayPlatform);
            if (_currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    /*
     * NOTES:
     *      The player doesn't get stuck if they jump up through the platform and then try to drop
     *      The player does get stuck if they land on the platform and then try to drop without having gone through it immediately before
     *      I think the _currentOneWayPlatform reference might be wonky, especially if the player is standing on multiple blocks at once
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Platform_CollisionEnter");
        if (collision.gameObject.layer == LayerMask.NameToLayer(GROUND_LAYER))
        {
            Debug.Log("Platform_CollisionEnter_Ground");
            _currentOneWayPlatform = collision.gameObject;
           // _player.SetIsDropping(false); //player should not drop after hitting another block
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Platform_CollisionExit");
        if (collision.gameObject.layer == LayerMask.NameToLayer(GROUND_LAYER))
        {
            _currentOneWayPlatform = null;
            //_player.SetIsDropping(false); //player should not drop after exiting one of the blocks
        }
    }

    private IEnumerator DisableCollision()
    {
        Debug.Log("Platform_DisableCollision");
        BoxCollider2D platformCollider = _currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(_playerCollider, platformCollider);
        yield return new WaitForSeconds(0.2f);
        Physics2D.IgnoreCollision(_playerCollider, platformCollider, false);
        _player.SetIsDropping(false);
    }
}
