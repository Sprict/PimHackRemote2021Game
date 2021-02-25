using System.Collections;
using UnityEngine;

public class CubeBehaviour : Bolt.EntityEventListener<ICubeState> {

	private float _resetColorTime;
	private Renderer _renderer;

	//特殊なBoltのメソッド，UnityのStartメソッドに相当
	public override void Attached ( ) {
		_renderer = GetComponent<Renderer>();
		state.SetTransforms (state.CubeTransform, transform);

		if (entity.IsOwner) {
			state.CubeColor = new Color (Random.value, Random.value, Random.value);
		}
		state.AddCallback ("CubeColor", ColorChanged); //CubeColorプロパティが値を変更した際にColorChanged関数が呼び出されるように設定
	}

	// SimulateOwnerはオーナー(プレハブをインスタンス化したコンピュータ）だけが呼び出す関数
	public override void SimulateOwner ( ) {
		var speed = 4f;
		var movement = Vector3.zero;

		if (Input.GetKey (KeyCode.W)) { movement.z += 1; }
		if (Input.GetKey (KeyCode.S)) { movement.z -= 1; }
		if (Input.GetKey (KeyCode.A)) { movement.x -= 1; }
		if (Input.GetKey (KeyCode.D)) { movement.x += 1; }

		if (movement != Vector3.zero) {
			transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			var flash = FlashColorEvent.Create(entity);
			flash.FlashColor = Color.red; //ダメージ色
			flash.Send();
		}
	}

	void ColorChanged ( ) {
		GetComponent<Renderer> ( ).material.color = state.CubeColor;
	}

	void OnGUI ( ) {
			if (entity.IsOwner) {
				GUI.color = state.CubeColor;
				GUILayout.Label ("@@@");
				GUI.color = Color.white;
			}
	}

	public override void OnEvent(FlashColorEvent evnt)
	{
		_resetColorTime = Time.time + 0.2f;
		_renderer.material.color = evnt.FlashColor;
	}

	private void Update()
	{
		if(_resetColorTime < Time.time)
		{
			_renderer.material.color = state.CubeColor;
		}
	}
}