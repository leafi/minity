using UnityEngine;
using System;
using System.Collections;
using MineLib.Network;
using MineLib.Network.Enums;
using MineLib.Network.Packets;
using MineLib.Network.Packets.Client;
using MineLib.Network.Packets.Client.Login;
using MineLib.Network.Packets.Client.Status;

public class ClientGame : MonoBehaviour, IMinecraftClient {

  public NetworkHandler networkHandler;
	

  #region IMinecraftClient implementation

  public string ServerIP { get; set; }

  public short ServerPort { get; set; }

  public MineLib.Network.Enums.ServerState State { get; set; }

  public string AccessToken { get; set; }

  public string SelectedProfile { get; set; }

  #endregion

  public bool Dead = false;

  // Use this for initialization
  void Start () {
    ServerIP = "127.0.0.1";
    ServerPort = 25565;
    print("creating nh");
    networkHandler = new NetworkHandler(this);
    print("nh.start");
    networkHandler.OnPacketHandled += (MineLib.Network.Packets.IPacket packet, int id, ServerState? state) => {
      if ((PacketsServer)id == PacketsServer.LoginSuccess)
        State = ServerState.Play;
      print(((PacketsServer)id).ToString());

    };

    networkHandler.Start();
    print("nh...");
    //PlayerHandl
    print(networkHandler.Crashed);
    print(networkHandler.Connected);

    StartCoroutine(GameGame());
  }

  IEnumerator GameGame() {
    Action<string> waitness = (string err) => {
      if (networkHandler.Crashed)
        throw new UnityException("ClientGame coroutine error: " + err);
    };


    print("hallo from coroutine");

    while (!networkHandler.Connected) {
      waitness("failed to connect to " + ServerIP + ":" + ServerPort);
      yield return null;
    }

    print("connected! :D");

    /*Minecraft.SendPacket(new HandshakePacket
                {
                    ProtocolVersion = 5,
                    ServerAddress = Minecraft.ServerIP,
                    ServerPort = Minecraft.ServerPort,
                    NextState = NextState.Login,
                });

                Minecraft.SendPacket(new LoginStartPacket {Name = Minecraft.ClientName});
*/

    networkHandler.Send(new HandshakePacket {
      ProtocolVersion = 5,
      ServerAddress = ServerIP,
      ServerPort = ServerPort,
      NextState = NextState.Login
    });

    networkHandler.Send(new LoginStartPacket { Name = "leafi" });

    while (State != ServerState.Play) {
      waitness("failed to handshake");
      yield return null;
    }

    networkHandler.Send(new ClientStatusPacket { Status = ClientStatus.Respawn });

    print("i did it!");
  }

  void FixedUpdate() {
    //_minecraft.SendPacket(new PlayerPacket { OnGround = _minecraft.Player.Position.OnGround });


    /*if (Dead)
      return;

    try {
      GameGame();
    } catch (Exception ex) {
      print("caught exception " + ex.ToString() + ". ClientGame coroutine STOP");
      Dead = true;
    }*/

  }
}
