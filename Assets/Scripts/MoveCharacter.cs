using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MoveCharacter : MonoBehaviour
{
	private int idX = Animator.StringToHash("x"), idY = Animator.StringToHash("y");
	private Animator animator = null;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		if (Mathf.FloorToInt(x) != 0 || Mathf.FloorToInt(y) != 0)
		{
			animator.SetFloat(idX, x);
			animator.SetFloat(idY, y);

			transform.localPosition += new Vector3(x, y) * 0.05f;
		}
	}
}