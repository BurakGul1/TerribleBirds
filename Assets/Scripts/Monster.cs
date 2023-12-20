using System.Collections;
using UnityEngine;

[SelectionBase]

public class Monster : MonoBehaviour
{
    private Bird _bird;
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private new ParticleSystem particleSystem;
    private SpriteRenderer _spriteRenderer;
    private bool _hasDied;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
        }
    }

    private bool ShouldDieFromCollision(Collision2D collision2D)
    {
        if (_hasDied)
        {
            return false;
        }
        _bird = collision2D.gameObject.GetComponent<Bird>();
        if (_bird != null)
        {
            return true;
        }

        if (collision2D.contacts[0].normal.y < -0.5)
        {
            Debug.Log("canavar öldü..");//Yüzeye dik gelirse
            return true;
        }
        return false;
    }

    IEnumerator Die()
    {
        _hasDied = true;
        _spriteRenderer.sprite = deadSprite;
        particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
