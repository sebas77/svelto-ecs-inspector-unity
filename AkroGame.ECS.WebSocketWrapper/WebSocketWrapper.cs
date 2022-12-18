#if SIMPLE_WEB_TRANSPORT
using AkroGame.ECS.Websocket;
using System;
using System.Threading.Tasks;
using JamesFrowen.SimpleWeb;
using Svelto.ECS;
using UnityEngine;

static public class SveltoInspector
{
    static bool _mustQuit;

    public static void Attach(EnginesRoot enginesRoot)
    {
        WebSocketWrapper wrapper = new WebSocketWrapper();

        InspectorService inspector = new InspectorService(wrapper, enginesRoot);

        Update(wrapper, inspector);
    }

    async static void Update(WebSocketWrapper wrapper, InspectorService inspector)
    {
        DateTime time = DateTime.Now;

        Task.Run(() => StartWebSocket(wrapper));

        while (Application.isPlaying)
        {
            inspector.Update((DateTime.Now - time));

            await Task.Yield();
        }

        _mustQuit = true;
    }

    async static void StartWebSocket(WebSocketWrapper wrapper)
    {
        while (_mustQuit == false)
        {
            wrapper.server.ProcessMessageQueue();

            await Task.Delay(100);
        }
    }

    public class WebSocketWrapper: IWebSocket
    {
        internal readonly SimpleWebServer server;

        public WebSocketWrapper()
        {
            var tcpConfig = new TcpConfig(true, 5000, 5000);
            server = new SimpleWebServer(5000, tcpConfig, 32000, 5000, default);
            // listen for events
            server.onDisconnect += (id) => { OnClose?.Invoke(id); };
            server.onData += (id, data) => { OnData?.Invoke(new Envelope<int, ArraySegment<byte>>(id, data)); };

            // start server listening on port 9300
            server.Start(9300);
        }

        public event Action<Envelope<int, ArraySegment<byte>>> OnData;
        public event Action<int> OnClose;

        public void Send(int connectionId, ArraySegment<byte> source)
        {
            server.SendOne(connectionId, source);
        }
    }
}

#endif