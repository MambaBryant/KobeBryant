using UnityEngine;
using System;


public class EncodingTest : MonoBehaviour 
{

	void Start () 
	{	
		byte[] data = BitConverter.GetBytes (0x1122);
		Debug.Log (data.Length); 
		string str = "";
		for (int i=0; i<data.Length; i++)
		{
			str += ", " + data[i];
		}
		Debug.Log (str);
		//	,34,17,0,0
		//	0001 0001 0010 0010
	}

}
