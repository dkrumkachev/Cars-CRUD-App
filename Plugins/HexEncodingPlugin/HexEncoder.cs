using PluginInterface;
using System.Text;

namespace HexEncodingPlugin
{
    public class HexEncoder : IEncodingPlugin
    {
        public string Extension { get; } = ".hex";

        void IEncodingPlugin.Encode(Stream stream, Stream output)
        {
            long position = stream.Position;
            stream.Position = 0;
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            stream.Position = position;
            string encoded = Convert.ToHexString(memoryStream.ToArray());
            output.Write(Encoding.UTF8.GetBytes(encoded));
        }

        void IEncodingPlugin.Decode(Stream stream, Stream output)
        {
            long position = stream.Position;
            stream.Position = 0;
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            stream.Position = position;
            string encoded = Encoding.UTF8.GetString(memoryStream.ToArray());
            output.Write(Convert.FromHexString(encoded));
        }
    }
}