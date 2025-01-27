using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Security.Dtos
{
    public class SecurityModel
    {
        public SecurityDto Security { get; set; }
    }
    public class SecurityDto
    {
        public AesDto Aes { get; set; }
    }

    public class AesDto
    {
        public string Key { get; set; }
        public string Iv { get; set; }
    }
}