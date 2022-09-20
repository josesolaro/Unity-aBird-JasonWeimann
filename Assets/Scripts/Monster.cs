using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private Sprite _deadSprite;
    [SerializeField]
    private ParticleSystem _particleSystem;
    private bool _hasDie = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
            StartCoroutine(Die());
    }
    private IEnumerator Die()
    {
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
        _hasDie = true;
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
    private bool ShouldDieFromCollision(Collision2D collision)
    {
        return !_hasDie && (collision.gameObject.GetComponent<Bird>() != null
            || collision.contacts.Any(c => c.normal.y < -0.5));
    }
}
