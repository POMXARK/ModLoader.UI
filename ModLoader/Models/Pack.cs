using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader.Models
{
    public class Pack
    {
        public Pack()
        {
            Mods = new HashSet<Mod>();
        }
        public virtual ICollection<Mod> Mods { get; set; }

        [Reactive] public uint Id { get; set; }
        [Reactive] public string? Name { get; set; }
        //public DateTime DateUpdate { get; set; }
    }
}
