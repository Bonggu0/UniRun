using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;
    void Update()
    {
        this.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0,0);
    }
}
