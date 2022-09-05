using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Vector3 forceVector;
    public float rotationSpeed;
    public float rotation;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        // Vector3 default initializes all components to 0.0f
        forceVector.x = 1.0f;
        rotationSpeed = 2.0f;
    }

    /* forced changes to rigid body physics parameters should be done through the FixedUpdate()
    method, not the Update() method
    */
    void FixedUpdate()
    {
        // force thruster
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(forceVector);
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            rotation += rotationSpeed;
            Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            GetComponent<Rigidbody>().MoveRotation(rot);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            rotation -= rotationSpeed;
            Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            GetComponent<Rigidbody>().MoveRotation(rot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire! " + rotation);

            /* we don’t want to spawn a Bullet inside our ship, so some
            Simple trigonometry is done here to spawn the bullet
            at the tip of where the ship is pointed.
            */
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.x += 1.5f * Mathf.Cos(rotation * Mathf.PI / 180);
            spawnPos.z -= 1.5f * Mathf.Sin(rotation * Mathf.PI / 180);

            // instantiate the Bullet
            GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;

            // get the Bullet Script Component of the new Bullet instance
            BulletScript b = obj.GetComponent<BulletScript>();

            // set the direction the Bullet will travel in
            Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            b.heading = rot;
        }
    }
}
