using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float move_speed = 5.0f;
    public float look_speed = 3.0f;
    public float jump_force = 300.0f;
    public Transform cam_trans;
    Rigidbody _rigidbody;
    private Vector2 rotation = Vector2.zero;
    public Transform foot_trans;
    public LayerMask ground_layer;
    bool grounded = false;
    public float ground_check_dist = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 move_dir = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        move_dir *= move_speed;
        move_dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = move_dir;

        // jumping
        grounded = Physics.CheckSphere(foot_trans.position, ground_check_dist, ground_layer);
        if(grounded && Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForce(new Vector3(0, jump_force, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // reference to the mouse
        // angles are inverted
        rotation.y += Input.GetAxis("Mouse X") * 2; // mouse x movement extra speed
        // rotation.x is also inverted, need to subtract
        rotation.x -= Input.GetAxis("Mouse Y");

        rotation.x = Mathf.Clamp(rotation.x, -30, 30);
        cam_trans.localEulerAngles = new Vector3(rotation.x, 0, 0) * look_speed;

        transform.eulerAngles = new Vector3(0, rotation.y, 0);

    }
}
