using EntityWorker.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube.Manager.Models.Container.DB_models
{
    public class Files : Base_Entity
    {
        public string Name { get; set; }

        public byte[] Content { get; set; }

      
    }
}
