using UnityEngine;

public class FlashLight : MonoBehaviour 
{
	public Light light1;

	void Start(){
		light1.enabled = false;
	}

	void Update() {
		if(Input.GetKeyDown ("f")){
			ToggleFlashLight();
		}
	}

	public void ToggleFlashLight () {
		
		if (light1.enabled == true) 
		{
			light1.enabled = false;
		}
		else
		{
			light1.enabled = true;
		}
	}
}
