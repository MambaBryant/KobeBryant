using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System;
using System.Threading;

public class SocketTest : MonoBehaviour
{
	private Socket socket;
	void Start ()
	{
		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		IAsyncResult result = socket.BeginConnect 
			(IPAddress.Parse ("127.0.0.1"),8080,
			(IAsyncResult ast) => Debug.Log ("callback"),
			socket);

		if (socket.Connected)
		{
			Thread thread = new Thread(new ThreadStart ( 
				() => {
					while (true)
					{
						byte[] bytesLens = new byte[4];
						Debug.Log ("socket receive"); 
						socket.Receive (bytesLens);
					}
			}));
			thread.IsBackground = true;
			thread.Start();
		}

	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.P))
		{
			Debug.Log ("keydow p"); 
			byte[] bytes = {9,5,2,7};
			socket.Send (bytes);
		}
	}


}
