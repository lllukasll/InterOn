using System;
using System.Collections.Generic;
using System.Text;

namespace InterOn.Data.ModelsDto
{
    public class LoginResponseData
    {
        public int ClientId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
