using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightRotation : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public bool clockwise = true;
    public Transform player;
    public LayerMask wallMask;
    public LayerMask playerMask;
    public float detectionRadius = 5f;

    public float coneAngle = 45f;
    public int rayCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotationAmount = clockwise ? -rotationSpeed : rotationSpeed;

        transform.Rotate(0f,0f, rotationAmount * Time.deltaTime);
        CheckVision();
    }
    void CheckVision()
    {
        float startAngle = -coneAngle / 2;
        
        for (int i = 0; i < rayCount; i++)
        {
            float angle = startAngle + (coneAngle / (rayCount - 1)) * i;

            Vector2 direction = new Vector2(
                Mathf.Cos((angle + transform.eulerAngles.z) * Mathf.Deg2Rad),
                Mathf.Sin((angle + transform.eulerAngles.z) * Mathf.Deg2Rad)
            );

        

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadius, wallMask | playerMask);

            if(hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player Detected!");
                }
                else if (hit.collider.CompareTag("Wall"))
                {
                    Debug.Log("Wall blocking vision");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        float startAngle = -coneAngle / 2;
        for (int i = 0; i < rayCount; i++)
        {
            float angle = startAngle + (coneAngle / (rayCount - 1)) * i;

            Vector2 direction = new Vector2(
                Mathf.Cos((angle + transform.eulerAngles.z) * Mathf.Deg2Rad),
                Mathf.Sin((angle + transform.eulerAngles.z) * Mathf.Deg2Rad));

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction * detectionRadius);
        }
    }
}


//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Debug.Log("Player Detected!");
//         }
//     }
// }


