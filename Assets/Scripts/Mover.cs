using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float xSpeed;
    [SerializeField] float ySpeed;
    [SerializeField] float zSpeed;

    float xValue;
    float yValue;
    float zValue;

    [SerializeField] float moveSpeed;
    Vector3 moveDir;

    [SerializeField] Vector3 spawnPos;

    void Start()
    {
        transform.Translate(spawnPos);
    }

    void Update()
    {
        Movement();
	}

    public void Movement()
    {
		xValue = Input.GetAxis("Horizontal");
		zValue = Input.GetAxis("Vertical");
		yValue = 0f;

		moveDir = new Vector3(xValue, yValue, zValue);
		transform.Translate(moveDir * moveSpeed * Time.deltaTime);
	}
}
