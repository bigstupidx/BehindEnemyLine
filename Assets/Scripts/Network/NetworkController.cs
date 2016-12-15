using UnityEngine;
using System.Collections;

public class NetworkController : MonoBehaviour {
	public ArrayList playerList = new ArrayList();
	
	[HideInInspector]	public string connectIP = "127.0.0.1";
	[HideInInspector]	public int connectPORT = 7777;
	[HideInInspector]	public int serverPORT = 7777;
	[HideInInspector]	public int maxPlayers = 8;
	[HideInInspector]	public string username;
	
	public struct PlayerInfo{
		public bool host;
		public string username;
		public NetworkPlayer player;
	
		public PlayerInfo(bool host, string username, NetworkPlayer player){
			this.host = host;
			this.username = username;
			this.player = player;
		}
	}
	
	public void Start(){
		DontDestroyOnLoad(gameObject);
		Application.LoadLevel(1);
		
		username = Utils.CreateRandomString(7);
	}
	
	public void Connect(){
		Network.Connect(connectIP, connectPORT);
		Utils.CLog("[NETWORK]", "Connecting to " + connectIP + ":" + connectPORT, "grey");
	}
	
	public void Disconnect(int _TIMEOUT){
		Network.Disconnect(_TIMEOUT);
		Utils.CLog("[NETWORK]", "Disconnected", "grey");
	}
	
	public void CloseConnection(NetworkPlayer player, string username){
		Network.CloseConnection(player, true);
		Utils.CLog("[NETWORK]", "Connection closed for player " + username, "grey");
	}
	
	public void StartServer(){
		Network.InitializeSecurity();
    	Network.InitializeServer(maxPlayers, serverPORT, !Network.HavePublicAddress());	
		networkView.RPC("LoadLevel", RPCMode.AllBuffered);
	}
	
	[RPC]
	void AddPlayerToList(NetworkPlayer player, string username, bool host){
		PlayerInfo newPlayerInfo = new PlayerInfo(host, username, player);
		
		playerList.Add(newPlayerInfo);
		Utils.CLog("[NETWORK]", "New palyer: " + username, "grey");
	}
	
	[RPC]
	void RemovePlayerFromList(NetworkPlayer player){
		foreach (PlayerInfo playerInstance in playerList) {
			if (player == playerInstance.player){
				Utils.CLog("[NETWORK]", "Player left: " + username, "grey");
				playerList.Remove(playerInstance);
				break;
			}
		}
	}
	
	[RPC]
 	IEnumerator LoadLevel() {
		Network.SetSendingEnabled(0, false);	
		Network.isMessageQueueRunning = false;
		
		AsyncOperation asyncOp = Application.LoadLevelAsync(2);
      	Utils.CLog(">>", "Loading...", "orange");    
		while (!asyncOp.isDone){
			yield return null;
		}
		Utils.CLog(">>", "Level loaded.", "orange");
		Network.isMessageQueueRunning = true;
		Network.SetSendingEnabled(0, true);
		
		foreach (GameObject go in MonoBehaviour.FindObjectsOfType(typeof (GameObject)))
			go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
	}

	void OnServerInitialized() {
		Utils.CLog("[NETWORK]", "Server initialized " + connectIP + ":" + connectPORT, "grey");
	}
	
	void OnConnectedToServer() {
		Utils.CLog("[NETWORK]", "Player connected from: " + Network.player.ipAddress + ":" + Network.player.port + connectIP + ":" + connectPORT, "grey");
	}
	
	void OnPlayerDisconnected(NetworkPlayer player) {
		Utils.CLog("[NETWORK]", "Clean up after player " + player.ipAddress + ":" + player.port, "grey");
		
		networkView.RPC("RemovePlayerFromList", RPCMode.All, player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
        playerList.Clear();
		Application.LoadLevel(1);
		Utils.CLog("[NETWORK]", Network.isServer ? ("Local server connection disconnected") : 
			(info == NetworkDisconnection.LostConnection  ? "Lost connection to the server" : "Diconnected from the server"), "grey");
	}
	
	void OnNetworkLoadedLevel(){
		networkView.RPC("AddPlayerToList",RPCMode.AllBuffered, Network.player, username, Network.isServer ? true : false);
		
		GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
		Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
		GameObject playerGO = Network.Instantiate(Resources.Load("Prefabs/Players/Player_CT_urban"), 
			randomSpawnPoint.position, randomSpawnPoint.rotation, 0) as GameObject;
		playerGO.SendMessage("SetupRenderer", SendMessageOptions.DontRequireReceiver);
	}
}
