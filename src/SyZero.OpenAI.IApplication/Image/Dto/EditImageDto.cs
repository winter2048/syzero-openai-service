using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyZero.OpenAI.IApplication.Image.Dto
{
    public class EditImageDto
    {
        public string ImageBase64 { get; set; }

        public string MaskBase64 { get; set; }

        public string Message { get; set; }
    }
}
