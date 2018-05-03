﻿using hase.DevLib.Framework.Contract;
using ProtoBuf;

namespace hase.DevLib.Services.FileSystemQuery.Contract
{
    [ProtoContract]
    public class FileSystemQueryRequest : ApplicationRequestMessage
    {
        [ProtoMember(1)]
        public FileSystemQueryTypeEnum QueryType { get; set; }
        [ProtoMember(2)]
        public string FolderPath { get; set; }

        public FileSystemQueryRequest() { }
        public FileSystemQueryRequest(FileSystemQueryTypeEnum queryType, string folderPath)
        {
            QueryType = queryType;
            FolderPath = folderPath;
        }

        public override string ToString() =>
             $"QueryType[{this.QueryType}] Path[{this.FolderPath}]";
    }
}
