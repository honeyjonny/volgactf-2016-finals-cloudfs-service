

using System;
using Newtonsoft.Json;

namespace CloudFs.Models
{
    public class FolderForm
    {
        [JsonProperty("id")]
        public Guid Id {get;set;}

        [JsonProperty("parentId")]
        public Guid ParentId {get;set;}

        [JsonProperty("foldername")]
        public string Foldername {get;set;} 
    }
}