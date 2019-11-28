using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public bool Grabbing = false;
    public Vector3 Movement = Vector3.zero;
    public Rigidbody2D Rigidbody2D = null;

    public void Init(PlayerController controller)
    {
        Rigidbody2D = controller.GetComponent<Rigidbody2D>();
    }
}
