using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _mousePos;
    private Vector2 _startPos;
    private Camera _camera;
    [SerializeField] private float launchForce = 1000;
    [SerializeField] private float maxDragDistance = 3.5f;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }
    void Start()
    {
        _rigidbody2D.isKinematic = true;
        _startPos = _rigidbody2D.position;
    }
    private void OnMouseDown() //Fareyle tıkladığımızda ne olacak ona karar veriyor.
    {
        _spriteRenderer.color = Color.red;
    }
    private void OnMouseUp() //Fareyi kaldırdığımızda ne olacak ona karar veriyor.
    {
        _spriteRenderer.color = Color.white;
        var currentPos = _rigidbody2D.position;
        Vector2 direction = _startPos - currentPos;
        direction.Normalize();
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * launchForce);
    }
    private void OnMouseDrag() //Mouse hareketlerini tutar.
    {
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = _mousePos;
        float distance = Vector2.Distance(desiredPosition, _startPos);
        if (distance > maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPos;
            direction.Normalize();
            desiredPosition = _startPos + (direction * maxDragDistance);
        }
        if (desiredPosition.x > _startPos.x)
        {
            desiredPosition.x = _startPos.x;
        }
        _rigidbody2D.position = desiredPosition;
    }

    private void OnCollisionEnter2D()
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        _rigidbody2D.position = _startPos;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
    }
}
