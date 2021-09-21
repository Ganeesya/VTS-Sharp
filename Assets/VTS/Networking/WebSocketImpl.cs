using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace VTS.Networking.Impl{
    public class WebSocketImpl : IWebSocket
    {
        private static UTF8Encoding ENCODER = new UTF8Encoding();
        private const UInt64 MAX_READ_SIZE = 1 * 1024 * 1024;

        // WebSocket
        private ClientWebSocket _ws = new ClientWebSocket();

        // Queues
        public ConcurrentQueue<string> RecieveQueue { get; }
        private BlockingCollection<ArraySegment<byte>> SendQueue { get; }
        
        // Threads
        private Thread _receiveThread { get; set; }
        private Thread _sendThread { get; set; }

        public WebSocketImpl(){
            _ws = new ClientWebSocket();
            RecieveQueue = new ConcurrentQueue<string>();
            _receiveThread = new Thread(RunReceive);
            _receiveThread.Start();
            SendQueue = new BlockingCollection<ArraySegment<byte>>();
            _sendThread = new Thread(RunSend);
            _sendThread.Start();
        }

        public void Close()
        {
            _receiveThread.Interrupt();
            _sendThread.Interrupt();
        }

        public async Task Connect(string URL, System.Action onConnect, System.Action onError)
        {
            Uri serverUri = new Uri(URL);
            Debug.Print("Connecting to: " + serverUri);
            await _ws.ConnectAsync(serverUri, CancellationToken.None);
            while(IsConnecting())
            {
                Debug.Print("Waiting to connect...");
                Task.Delay(50).Wait();
            }
            Debug.Print("Connect status: " + _ws.State);
            if(_ws.State == WebSocketState.Open){
                onConnect();
            }else{
                onError();
            }
        }

        #region Status
        public bool IsConnecting()
        {
            return _ws.State == WebSocketState.Connecting;
        }

        public bool IsConnectionOpen()
        {
            return _ws.State == WebSocketState.Open;
        }
        #endregion

        #region Send
        public void Send(string message)
        {
            byte[] buffer = ENCODER.GetBytes(message);
            // Debug.Print("Message to queue for send: " + buffer.Length + ", message: " + message);
            ArraySegment<byte> sendBuf = new ArraySegment<byte>(buffer);
            SendQueue.Add(sendBuf);
        }

        private async void RunSend()
        {
            Debug.Print("WebSocket Message Sender looping.");
            ArraySegment<byte> msg;
            try
            {
                while (true)
                {
                    while (!SendQueue.IsCompleted && this.IsConnectionOpen())
                    {
                        msg = SendQueue.Take();
                        // Debug.Print("Dequeued this message to send: " + msg);
                        await _ws.SendAsync(msg, WebSocketMessageType.Text, true /* is last part of message */,
                            CancellationToken.None);
                    }
                    Task.Delay(10).Wait();
                }
            }
            catch (ThreadInterruptedException e)
            {

            }
        }
        #endregion

        #region Receive
        private async Task<string> Receive(UInt64 maxSize = MAX_READ_SIZE)
        {
            // A read buffer, and a memory stream to stuff unknown number of chunks into:
            byte[] buf = new byte[4 * 1024];
            MemoryStream ms = new MemoryStream();
            ArraySegment<byte> arrayBuf = new ArraySegment<byte>(buf);
            WebSocketReceiveResult chunkResult = null;
            if (IsConnectionOpen())
            {
                do
                {
                    chunkResult = await _ws.ReceiveAsync(arrayBuf, CancellationToken.None);
                    ms.Write(arrayBuf.Array, arrayBuf.Offset, chunkResult.Count);
                    //Debug.Print("Size of Chunk message: " + chunkResult.Count);
                    if ((UInt64)(chunkResult.Count) > MAX_READ_SIZE)
                    {
                        Console.Error.WriteLine("Warning: Message is bigger than expected!");
                    }
                } while (!chunkResult.EndOfMessage);
                ms.Seek(0, SeekOrigin.Begin);
                // Looking for UTF-8 JSON type messages.
                if (chunkResult.MessageType == WebSocketMessageType.Text)
                {
                    return StreamToString(ms, Encoding.UTF8);
                }
            }
            return "";
        }

        private async void RunReceive()
        {
            Debug.Print("WebSocket Message Receiver looping.");
            string result;
            try
            {
                while (true)
                {
                    result = await Receive();
                    if (result != null && result.Length > 0)
                    {
                        RecieveQueue.Enqueue(result);
                    }
                    else
                    {
                        Task.Delay(50).Wait();
                    }
                }
            }
            catch (ThreadInterruptedException e)
            {
            }
        }
        #endregion

        private static string StreamToString(MemoryStream ms, Encoding encoding)
        {
            string readString = "";
            if (encoding == Encoding.UTF8)
            {
                using (var reader = new StreamReader(ms, encoding))
                {
                    readString = reader.ReadToEnd();
                }
            }
            return readString;
        }
    }
}