
using System;
using Newtonsoft.Json;

namespace CloudFs.Models
{
    public class UserForm
    {
        [JsonProperty("id")]
        public Guid Id {get;set;}

        [JsonProperty("username")]
        public string Username {get;set;}

        [JsonProperty("password")]
        public string Password {get;set;}
    }
}