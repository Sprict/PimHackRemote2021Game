﻿using System;
using Bolt;
using Bolt.Matchmaking;
using UdpKit;
using UnityEngine;

public class Menu : GlobalEventListener {
    void OnGUI ( ) {
        GUILayout.BeginArea (new Rect (10, 10, Screen.width - 20, Screen.height - 20));

        if (GUILayout.Button ("Start Server", GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true))) {
            // START SERVER
            BoltLauncher.StartServer ( );
        }

        if (GUILayout.Button ("Start Client", GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true))) {
            // START CLIENT
            BoltLauncher.StartClient ( );
        }

        GUILayout.EndArea ( );
    }

    public override void BoltStartDone ( ) {
        if (BoltNetwork.IsServer) {
            string matchName = Guid.NewGuid ( ).ToString ( );

            BoltMatchmaking.CreateSession (
                sessionID: matchName,
                sceneToLoad: "Tutorial1"
            );
        }
    }

    public override void SessionListUpdated (Map<Guid, UdpSession> sessionList) {
        Debug.LogFormat ("Session list updated: {0} total sessions", sessionList.Count);

        foreach (var session in sessionList) {
            UdpSession photonSession = session.Value as UdpSession;

            if (photonSession.Source == UdpSessionSource.Photon) {
                BoltMatchmaking.JoinSession (photonSession);
            }
        }
    }
}