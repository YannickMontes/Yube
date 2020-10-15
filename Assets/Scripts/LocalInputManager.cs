using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalInputManager : Yube.InputManager<LocalInputManager.EAxis, LocalInputManager.EKey>
{
	public enum EAxis
	{
		HORIZONTAL,
		VERTICAL,
		FIRE1
	}

	public enum EKey
	{
	}
}