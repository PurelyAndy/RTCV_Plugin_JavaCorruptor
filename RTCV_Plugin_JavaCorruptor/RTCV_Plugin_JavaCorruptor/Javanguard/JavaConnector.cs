using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Java_Corruptor.Javanguard;

public class JavaConnector
{
    private const int Port = 27878;
    private const int JavaPort = 26767;
    private static TcpClient _client;
    private static NetworkStream _clientStream;
    private static TcpListener _listener;
    private static TcpClient _javaClient;
    private static NetworkStream _javaStream;
    
    public static void StartListening()
    {
        Task.Run(() =>
        {
            _listener = new(IPAddress.Loopback, Port);
            _listener.Start();
            while (true)
            {
                _client = _listener.AcceptTcpClient();
                _clientStream = _client.GetStream();
                byte[] lengthBytes = new byte[4];
                _clientStream.Read(lengthBytes, 0, lengthBytes.Length);
                int length = BitConverter.ToInt32(lengthBytes, 0);
                int bytesRead = 4;
                byte[] fullBytes = new byte[length];
                lengthBytes.CopyTo(fullBytes, 0);
                while (bytesRead < length)
                {
                    byte[] nameLengthBytes = new byte[2];
                    int read = 0;
                    while (read < nameLengthBytes.Length)
                        read += _clientStream.Read(nameLengthBytes, 0, nameLengthBytes.Length - read);
                    bytesRead += read;
                    nameLengthBytes.CopyTo(fullBytes, bytesRead - nameLengthBytes.Length);
                    ushort nameLength = BitConverter.ToUInt16(nameLengthBytes, 0);
                    byte[] classNameBytes = new byte[nameLength];
                    read = 0;
                    while (read < classNameBytes.Length)
                        read += _clientStream.Read(classNameBytes, read, classNameBytes.Length - read);
                    bytesRead += read;
                    classNameBytes.CopyTo(fullBytes, bytesRead - classNameBytes.Length);
                    string className = Encoding.UTF8.GetString(classNameBytes);

                    byte[] classLengthBytes = new byte[4];
                    read = 0;
                    while (read < classLengthBytes.Length)
                        read += _clientStream.Read(classLengthBytes, 0, classLengthBytes.Length - read);
                    bytesRead += read;
                    classLengthBytes.CopyTo(fullBytes, bytesRead - classLengthBytes.Length);
                    int classLength = BitConverter.ToInt32(classLengthBytes, 0);
                    byte[] classBytes = new byte[classLength];
                    read = 0;
                    while (read < classBytes.Length)
                        read += _clientStream.Read(classBytes, read, classBytes.Length - read);
                    bytesRead += read;
                    classBytes.CopyTo(fullBytes, bytesRead - classBytes.Length);
                    CorruptModeInfo.ClassData[className] = classBytes;
                }

                File.WriteAllBytes(@"C:\Fuck2.raw", fullBytes);
            }
        });
        _javaClient = new(IPAddress.Loopback.ToString(), JavaPort);
        _javaStream = _javaClient.GetStream();
    }

    public static void SendMessage(ReplaceClasses message)
    {
        _javaStream.WriteByte((byte)MessageID.ReplaceClass);
        _javaStream.Write(BitConverter.GetBytes(message.Definitions.Count), 0, 4);
        
        foreach (var v in message.Definitions)
        {
            _javaStream.Write(BitConverter.GetBytes((ushort)v.Key.Length), 0, 2);
            _javaStream.Write(Encoding.UTF8.GetBytes(v.Key), 0, v.Key.Length);
            _javaStream.Write(BitConverter.GetBytes(v.Value.Length), 0, 4);
            _javaStream.Write((byte[])(Array)v.Value, 0, v.Value.Length);
        }
    }
    public static void SendResetOrder()
    {
        _javaStream.WriteByte(4);
    }
    
    enum MessageID : byte {
        CorruptInstruction = 0,
        ReplaceClass = 1,
        ResetClass = 2,
    }

    private static Type[] ids = [typeof(CorruptInstruction), typeof(ReplaceClasses), typeof(ResetClass)];

    class CorruptInstruction {
        public ushort NameLength;
        public string QualifiedName;
        public ushort Index;
        public ushort BytesReplaced;
        public byte InstructionsLength;
        public byte[] InstructionBytes;

        public CorruptInstruction()
        {
            byte[] nameLength = new byte[2];
            byte[] qualifiedName = new byte[NameLength];
            byte[] index = new byte[2];
            byte[] bytesReplaced = new byte[2];

            _clientStream.Read(nameLength, 0, nameLength.Length);
            _clientStream.Read(qualifiedName, 0, qualifiedName.Length);
            _clientStream.Read(index, 0, index.Length);
            _clientStream.Read(bytesReplaced, 0, bytesReplaced.Length);
            
            NameLength = BitConverter.ToUInt16(nameLength, 0);
            QualifiedName = Encoding.UTF8.GetString(qualifiedName);
            NameLength = BitConverter.ToUInt16(index, 0);
            BytesReplaced = BitConverter.ToUInt16(bytesReplaced, 0);
            InstructionsLength = (byte)_clientStream.ReadByte(); //ReadByte returns an int, 10/10 api design very good very make much sense
            InstructionBytes = new byte[InstructionsLength];
            _clientStream.Read(InstructionBytes, 0, InstructionBytes.Length);
        }
    }
    class ResetClass {
        public ushort NameLength;
        public string QualifiedName;

        public ResetClass()
        {
            byte[] nameLength = new byte[2];
            byte[] qualifiedName = new byte[NameLength];
            
            _clientStream.Read(nameLength, 0, nameLength.Length);
            _clientStream.Read(qualifiedName, 0, qualifiedName.Length);
            
            NameLength = BitConverter.ToUInt16(nameLength, 0);
            QualifiedName = Encoding.UTF8.GetString(qualifiedName);
        }
    }
    public class ReplaceClasses
    {
        public IDictionary<string, sbyte[]> Definitions;

        public ReplaceClasses(IDictionary<string, sbyte[]> definitions)
        {
            Definitions = definitions;
        }
    }
}