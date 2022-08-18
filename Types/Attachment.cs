namespace Types;

public record Attachment(string Name, string ContentType, string BlobUri, byte[] Bytes);