using System.Collections;
using UnityEngine;

[BoltGlobalBehaviour]　//この属性をつけたクラスをBoltが自動インスタンス化
public class NetworkCallbacks : Bolt.GlobalEventListener {
    public override void SceneLoadLocalDone (string scene) {

        // randomize a position
        var spawnPosition = new Vector3 (Random.Range (-16, 16), 0, Random.Range (-16, 16));

        // instantiate cube
        BoltNetworkUtils.Instantiate (BoltPrefabs.Cube, spawnPosition, Quaternion.identity);
    }
}