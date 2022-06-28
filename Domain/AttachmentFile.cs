namespace Domain;

public class AttachmentFile
{
    public AttachmentFile(string fileName, string contentType, byte[] bytes)
    {
        FileName = fileName;
        ContentType = contentType;
        Bytes = bytes;
    }

    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Bytes { get; set; }
}