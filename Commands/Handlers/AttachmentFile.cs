﻿namespace Commands.Handlers;

public record AttachmentFile(string FileName, string ContentType, byte[] Bytes);