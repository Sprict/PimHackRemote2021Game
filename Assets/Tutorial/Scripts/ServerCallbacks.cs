using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

[BoltGlobalBehaviour(BoltNetworkModes.Server)] //このクラスはサーバーでしか作成・実行されない
public class ServerCallbacks : Bolt.GlobalEventListener
{
    public override void Connected(BoltConnection connection) //クライアントがこのインスタンスに接続されるとコールバックがトリガーされる。
    {
        var log = LogEvent.Create(); //新たなイベント作成、イベントが誰に送信されるのか、どのように届けられるのか指定することもできる
        log.Message = string.Format("{0} connected!", connection.RemoteEndPoint);
        log.Send();
    }

    public override void Disconnected(BoltConnection connection) //リモートサーバーから切断されたときにコールバックがトリガーされる
    {
        var log = LogEvent.Create();
        log.Message = string.Format("{0} disconnected", connection.RemoteEndPoint);
        log.Send();
    }
}
