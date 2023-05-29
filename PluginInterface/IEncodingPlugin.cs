namespace PluginInterface
{
    public interface IEncodingPlugin
    {
        string Extension { get; }

        void Encode(Stream stream, Stream output);

        void Decode(Stream stream, Stream output);
    }
}