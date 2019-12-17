using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerController playerTarget;
	[SerializeField] private float speed;
	[SerializeField] private Rigidbody rb;

    [SerializeField] private bool onLight = false;

    [SerializeField] private Animator anim;
    [SerializeField] private float timeAnim = 1f;

    void Start()
    {
        Thunder.instance.AddInEnemies(gameObject);

        if (!anim)
        {
            throw new System.InvalidOperationException($"no {anim} in {this.gameObject.name} in Inspector");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (playerTarget.gameObject.transform.position.x < transform.position.x)
        {
			rb.velocity = new Vector3(-speed * Time.deltaTime, rb.velocity.y, rb.velocity.z);
        }
        else if (playerTarget.gameObject.transform.position.x > transform.position.x)
		{
			rb.velocity = new Vector3(speed * Time.deltaTime, rb.velocity.y, rb.velocity.z);
		}
    }

    public void Onlight()
    {
        onLight = true;

        StartCoroutine("killAnim", timeAnim);
    }

    IEnumerator killAnim(float delai)
    {
        anim.SetBool("playDeath", true);
        AudioManager.instance.Play("tireDeath");

        yield return new WaitForSeconds(delai);

        gameObject.SetActive(false);
    }
}
