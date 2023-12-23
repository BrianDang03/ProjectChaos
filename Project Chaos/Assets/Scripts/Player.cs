using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    public bool IsWalking { get { return isWalking; } }

    private void Update()
    {
        PlayerMovementInput();
    }

    private void PlayerMovementInput()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += movDir * Time.deltaTime * movementSpeed;
        isWalking = movDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotationSpeed);
    }
}
