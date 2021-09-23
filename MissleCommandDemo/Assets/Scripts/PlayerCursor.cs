using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    // Referencing components n' stuff
    BoxCollider2D _cursorCollider;
    PlayerMissleManager _playerMissleManager;

    // Config
    [SerializeField]
    private float _cursorMoveSpeed;

    private void Start() // Getting components
    {
        _cursorCollider = GetComponent<BoxCollider2D>();
        _playerMissleManager = FindObjectOfType<PlayerMissleManager>();
    }

    private void Update()
    {
        CursorMove();
        BoundaryCheck();
        Fire();
    }

    private void CursorMove() // cursor movement
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(_cursorMoveSpeed * Time.deltaTime * Vector3.right);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(_cursorMoveSpeed * Time.deltaTime * Vector3.left);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(_cursorMoveSpeed * Time.deltaTime * Vector3.up);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(_cursorMoveSpeed * Time.deltaTime * Vector3.down);
        }
    }

    private void BoundaryCheck()
    {
        if (transform.position.x >= 8.24)
        {
            transform.Translate(-0.1f, 0f, 0f);
        }
        else if (transform.position.x <= -8.24)
        {
            transform.Translate(0.1f, 0f, 0f);
        }
        else if (transform.position.y >= 4.38)
        {
            transform.Translate(0f, -0.1f, 0f);
        }
        else if (transform.position.y <= -2)
        {
            transform.Translate(0f, 0.1f, 0f);
        }
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerMissleManager.KeyTriggered(transform.position);
        }
    }
}
