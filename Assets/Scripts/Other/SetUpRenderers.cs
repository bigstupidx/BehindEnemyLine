using UnityEngine;
using System.Collections;

public class SetUpRenderers : MonoBehaviour {
	public Renderer[] renderers;
	public int FPV_Renderer_LayerIndex = 12;
	public int TPV_Renderer_LOCAL_LayerIndex = 9;
	public int TPV_Renderer_REMOTE_LayerIndex = 11;
	public bool useRootNetworkView = true;
	
	
	public void SetupViewWeaponRenderer(){
		foreach(Renderer renderer in renderers){
			renderer.gameObject.layer = FPV_Renderer_LayerIndex;
		}
	}
	
	public void SetupRenderer () {
		bool nViewIsMine = useRootNetworkView ?  transform.root.gameObject.networkView.isMine : networkView.isMine;
		foreach(Renderer renderer in renderers){
			renderer.gameObject.layer = nViewIsMine ? TPV_Renderer_LOCAL_LayerIndex : TPV_Renderer_REMOTE_LayerIndex;
		}
	}

}
