using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    private void Update()
    {
        Vector2 inputVector = Vector2.zero;


        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1f;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDirection * (Time.deltaTime * speed);
    }
}