using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    InputAction moveHorizontal = new InputAction(type: InputActionType.Button);

    [SerializeField]
    InputAction moveVertical = new InputAction(type: InputActionType.Button);

    void OnEnable()
    {
        moveHorizontal.Enable();
        moveVertical.Enable();
    }

    void OnDisable()
    {
        moveHorizontal.Disable();
        moveVertical.Disable();
    }

    void Update()
    {
        float horizontal = moveHorizontal.ReadValue<float>();
        float vertical = moveVertical.ReadValue<float>() * 0.7f;
        Vector3 movementVector = new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
        transform.position += movementVector;
    }
}
