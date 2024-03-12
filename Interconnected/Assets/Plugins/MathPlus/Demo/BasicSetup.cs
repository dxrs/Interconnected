using UnityEngine;
using System.Collections;

using MathPlus; //Add this line to your script to use math plus!

public class BasicSetup : MonoBehaviour {

	void Start () {
		Debug.Log(mathPlus.RandomString(new string[]{"a","b","c","1","7","3","h"}, 10)); // To use math plus type: "mathPlus.functionName(arguments);"	
		Debug.Log(mathPlus.RemoveCommasFromString("10,203,582,400"));
	}
}
