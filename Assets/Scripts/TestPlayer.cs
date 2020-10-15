using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
	private void Start()
	{
		LocalInputManager.Instance.RegisterAxisListener(LocalInputManager.EAxis.HORIZONTAL, OnHorizontalInput, rawAxis: true, true);
		LocalInputManager.Instance.RegisterAxisListener(LocalInputManager.EAxis.VERTICAL, OnVerticalInput, rawAxis: true, true);
		LocalInputManager.Instance.RegisterAxisListener(LocalInputManager.EAxis.FIRE1, OnFire, rawAxis: true, true);
	}

	private void FixedUpdate()
	{
		transform.position += Time.fixedDeltaTime * m_inputs;
		m_inputs = Vector3.zero;
	}

	private void OnHorizontalInput(LocalInputManager.EAxis axis, float value)
	{
		m_inputs.x = value;
	}

	private void OnVerticalInput(LocalInputManager.EAxis axis, float value)
	{
		m_inputs.y = value;
	}

	private void OnFire(LocalInputManager.EAxis axis, float value)
	{
		Debug.Log("FIRE : " + value);
	}

	private Vector3 m_inputs = Vector3.zero;
}