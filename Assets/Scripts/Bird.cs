using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    float _launchForce;
    [SerializeField]
    float _maxDragDistance = 3f;
    [SerializeField]
    float _yLimit = -3f;

    Vector2 _startPosition;
    Rigidbody2D _rigidBody2D;
    SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody2D = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        _startPosition = _rigidBody2D.position;
        _rigidBody2D.isKinematic = true;
    }

    private void OnMouseDown()
    {
        _spriteRenderer.color = Color.red;
    }
    private void OnMouseUp()
    {
        _spriteRenderer.color = Color.white;
        var finalPosition = _rigidBody2D.position;
        var direction = _startPosition - finalPosition;
        direction.Normalize();
        _rigidBody2D.isKinematic = false;
        _rigidBody2D.AddForce(direction * _launchForce);
    }

    private void OnMouseDrag()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 desiredPosition = mousePosition;

        //not going right
        if (desiredPosition.x > _startPosition.x)
        {
            desiredPosition.x = _startPosition.x;
        }

        //circunference max position
        var diference = desiredPosition - _startPosition;
        if (diference.magnitude > _maxDragDistance)
        {
            desiredPosition = _startPosition + diference.normalized * _maxDragDistance;
        }

        //not going under ground
        if (desiredPosition.y < _yLimit)
        {
            desiredPosition.y = _yLimit;
        }

        _rigidBody2D.position = desiredPosition;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rigidBody2D.position = _startPosition;
        _rigidBody2D.isKinematic = true;
        _rigidBody2D.velocity = Vector2.zero;
    }
}
