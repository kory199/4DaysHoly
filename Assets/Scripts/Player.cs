using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] AudioSource audioSource = null;

    Rigidbody2D rb;
    SpriteRenderer sr;
    PolygonCollider2D pc;

    Vector2 firstScale;
    float limitLeft = -3;
    float limitRight = 3;
    float firstY = 3.8f;
    float timer = 0;
    float ranBoom = 0;
    bool flagClick = false;
    [HideInInspector] public bool flag = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
        if (this.transform.position.y > 3.9f)
        {
            StartCoroutine(AudioPlay());
        }

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<PolygonCollider2D>();

        rb.Sleep();
        firstScale = new Vector2(transform.localScale.x, transform.localScale.y);
        transform.localScale = transform.localScale / 2;
        StartCoroutine(ScaleSize());
    }
    IEnumerator AudioPlay()
    {
        audioSource.enabled = true;
        yield return new WaitForSeconds(1f);
        audioSource.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadLine"))
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                sr.color = Color.red;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadLine"))
        {
            timer = 0f;

            sr.color = Color.white;
        }
    }

    void Update()
    {
        Vector2 mPosition = Input.mousePosition;
        Vector2 target = Camera.main.ScreenToWorldPoint(mPosition);

        if (flag == false)
        {
            this.transform.position = new Vector2(target.x, 4);

            if (transform.position.y >= firstY)
            {
                if (transform.position.x <= limitLeft)
                {
                    transform.position = new Vector2(-3, 4);
                }

                if (transform.position.x >= limitRight)
                {
                    transform.position = new Vector2(3, 4);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (flagClick == false)
            {
                rb.velocity = Vector2.zero;
                flagClick = true;

                StartCoroutine(ColCall());
            }
            flag = true;
            rb.WakeUp();
        }
    }

    IEnumerator ScaleSize()
    {
        yield return new WaitForSeconds(0.08f);
        transform.localScale = transform.localScale * 2.5f;
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(ScaleSize2());
    }

    IEnumerator ScaleSize2()
    {
        yield return new WaitForSeconds(0.2f);
        transform.localScale = firstScale;
    }

    public void Gameover()
    {
        Debug.Log("Gameover1");
        ranBoom = Random.Range(0, 3);
        Debug.Log(ranBoom);
        Destroy(gameObject, ranBoom);
        Debug.Log("GameOver2");
    }

    IEnumerator ColCall()
    {
        if (GetComponent<PolygonCollider2D>() == null)
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.AddComponent<PolygonCollider2D>();
        }
    }
}
