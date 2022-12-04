using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader.Models
{
    public class WebResource
    {
        // Configure Many-to-Many Relationships in Code-Firs
        // https://www.entityframeworktutorial.net/code-first/configure-many-to-many-relationship-in-code-first.aspx
        public WebResource()
        {
            Mods = new HashSet<Mod>();
        }
        public virtual ICollection<Mod> Mods { get; set; }


        public uint Id { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }
        //public DateTime DateUpdate { get; set; }
        public string? Link { get; set; }
        public string? SourseDownload { get; set; }
        public string? LinkDownload { get; set; }
        public string? AboutMod { get; set; }
    }
}
