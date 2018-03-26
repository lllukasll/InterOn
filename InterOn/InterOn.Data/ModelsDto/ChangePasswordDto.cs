using System;
using System.Collections.Generic;
using System.Text;

namespace InterOn.Data.ModelsDto
{
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPassword2 { get; set; }
    }
}
