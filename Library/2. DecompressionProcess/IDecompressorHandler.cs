namespace DS_ProgramingChallengeLibrary
{
    public interface IDecompressorHandler
    {
        void DecompressFiles();
        string DecompressFile(string fileNamePath);
    }
}