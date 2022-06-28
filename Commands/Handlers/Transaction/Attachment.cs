namespace Commands.Handlers.Transaction;

public record Attachment(string Name, string ContentType, string BlobUri, byte[] Bytes);