using UnityEngine;

public class ObjectHit : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if (GetComponent<MeshRenderer>().material.color != Color.red)
			{
				GetComponent<MeshRenderer>().material.color = Color.red;
			}
			else
			{
				GetComponent<MeshRenderer>().material.color = Color.blue;
			}
		}
	}


}
