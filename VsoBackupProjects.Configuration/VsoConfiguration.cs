using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsoBackupProjects.Cryptography;

namespace VsoBackupProjects.Configuration
{
    public class VsoConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("VsoUserName", IsRequired = true)]
        public string VsoUserName => this["VsoUserName"].ToString();

        [ConfigurationProperty("VsoPassword", IsRequired = true)]
        public string VsoPassword => this["VsoPassword"].ToString().Decrypt();

        [ConfigurationProperty("VsoProjects", IsRequired = true)]
        public string VsoProjects => this["VsoProjects"].ToString();

        [ConfigurationProperty("VsoVersionControlUrl", IsRequired = true)]
        public string VsoVersionControlUrl => this["VsoVersionControlUrl"].ToString();

        [ConfigurationProperty("BackupFolder", IsRequired = true)]
        public string BackupFolder => this["BackupFolder"].ToString();
    }
}
