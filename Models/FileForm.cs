
using System;
using Newtonsoft.Json;

namespace CloudFs.Models
{
    public class FileForm
    {
        [JsonProperty("id")]
        public Guid Id {get;set;}

        [JsonProperty("folderId")]
        public Guid FolderId {get;set;}

        [JsonProperty("filename")]
        public string Filename {get;set;}

        [JsonProperty("content")]
        public string Content {get;set;}
    }
}