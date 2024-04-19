using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 5.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerUpStrength = 15f;
    public GameObject powerUpIndicator;

    public bool hasPowerUp;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))

        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            StartCoroutine(PowerUpCountDownRoutine());
            powerUpIndicator.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("COLLISON ENTEr" + collision.gameObject.name + "Has power" + hasPowerUp);
            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }
}
