using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public float direction;

    private void Update()
    {
        // Move the ball in the specified direction at the specified speed
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);
    }
}
