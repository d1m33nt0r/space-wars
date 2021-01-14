using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidAnim : MonoBehaviour
{
    void FixedUpdate()
    {
        Quaternion rotationZ = Quaternion.AngleAxis(3, new Vector3(0, 0, 1));
        transform.rotation *= rotationZ;
    }
}
