using UnityEngine;
using System.Collections;

public class ChatTest : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private string ip = "127.0.0.1";
	private int port = 10001;
	private int connectCount = 15;
	private bool useNAT = false;
	private string recvMsg = "";
	private string sendMsg = "";

	void OnGUI ()
	{
		switch (Network.peerType)
		{
			case NetworkPeerType.Disconnected:
				StartCreate (); 
				break;
			case NetworkPeerType.Server:
				OnServer ();
				break;
			case NetworkPeerType.Client:
				OnClient ();
				break;
			case NetworkPeerType.Connecting:
				Debug.Log ("Connecting ...");
				break;
		}
	}

	void StartCreate ()
	{
		GUILayout.BeginVertical();
		if (GUILayout.Button ("新建服务器"))
		{
			//	type -> server
			NetworkConnectionError error = Network.InitializeServer (connectCount,port, useNAT);
			Debug.Log (error); 
		}
		if (GUILayout.Button ("连接服务器"))
		{
			//	type -> client
			NetworkConnectionError error = Network.Connect (ip, port);
			Debug.Log (error); 
		}
		GUILayout.EndVertical();
	}

	void OnServer ()
	{
		GUILayout.Label ("新建服务器成功, 等待客户端连接");
		string ip = Network.player.ipAddress;
		int port = Network.player.port;

		GUILayout.Label ("IP地址: " + ip + "\n端口号: " + port);
		int length = Network.connections.Length;

		for (int i=0; i<length; i++)
		{
			GUILayout.Label ("连接的IP: " + Network.connections[i].ipAddress);
			GUILayout.Label ("连接的端口: " + Network.connections[i].port);
		}
		if (GUILayout.Button("断开连接"))
		{
			//	type -> disconnected
			Network.Disconnect();
		}
		GUILayout.TextArea (recvMsg);
		sendMsg = GUILayout.TextField (sendMsg);

		if (GUILayout.Button ("发送消息"))
		{
			GetComponent<NetworkView>().RPC ("SendMsg", RPCMode.All, Network.player + "Say:" + sendMsg);
		}
	}

	void OnClient ()
	{
		GUILayout.Label ("连接成功");
		if (GUILayout.Button ("断开连接"))
		{
			//	type -> disconnted
			Network.Disconnect();
		}
		GUILayout.TextArea (recvMsg);
		sendMsg = GUILayout.TextField (sendMsg);
		if (GUILayout.Button ("发送消息"))
		{
			GetComponent<NetworkView>().RPC ("SendMsg", RPCMode.All, Network.player + "Say:" + sendMsg);
		}
	}
	[RPC]
	void SendMsg (string msg)
	{
		this.recvMsg += "\n";
		this.recvMsg += msg;
	}
}
