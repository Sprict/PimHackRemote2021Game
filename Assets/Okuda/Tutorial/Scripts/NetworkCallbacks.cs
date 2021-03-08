using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

[BoltGlobalBehaviour]　//この属性をつけたクラスをBoltが自動インスタンス化
public class NetworkCallbacks : Bolt.GlobalEventListener {
    public override void SceneLoadLocalDone (string scene) {

        // randomize a position
        var spawnPosition = new Vector3 (Random.Range (-8, 8), 3.0f, Random.Range (-8, 8));

        // instantiate cube
        BoltNetwork.Instantiate (BoltPrefabs.Player, spawnPosition, Quaternion.identity);

    }

    List<string> logMessages = new List<string>();

    public override void OnEvent(LogEvent evnt)
    {
        logMessages.Insert(0, evnt.Message);
    }

    private void OnGUI()
    {
        int maxMessages = Mathf.Min(5, logMessages.Count);

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);

        for (int i = 0; i < maxMessages; ++i)
        {
            GUILayout.Label(logMessages[i]);
        }
        GUILayout.EndArea();
    }
}