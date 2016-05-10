using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsoBackupProjects.VisualStudioOnline.Models
{
    public class Value
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Project Project { get; set; }
        public string DefaultBranch { get; set; }
        public string RemoteUrl { get; set; }
    }
}
