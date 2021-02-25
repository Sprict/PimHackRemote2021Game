using System.Collections;
using UnityEngine;

[BoltGlobalBehaviour]　//この属性をつけたクラスをBoltが自動インスタンス化
public class NetworkCallbacks : Bolt.GlobalEventListener {
    public override void SceneLoadLocalDone (string scene) {

        // randomize a position
        var spawnPosition = new Vector3 (Random.Range (-8, 8), 0, Random.Range (-8, 8));

        // instantiate cube
        BoltNetwork.Instantiate (BoltPrefabs.Cube, spawnPosition, Quaternion.identity);
    }
}