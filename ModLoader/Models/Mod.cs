using ReactiveUI.Fody.Helpers;
using Splat.ModeDetection;
using System.Collections.Generic;

namespace ModLoader.Models
{
    public class Mod
    {
        // Configure Many-to-Many Relationships in Code-Firs
        // https://www.entityframeworktutorial.net/code-first/configure-many-to-many-relationship-in-code-first.aspx
        public Mod()
        {
            WebResources = new HashSet<WebResource>();
            Packs = new HashSet<Pack>();
        }
        public virtual ICollection<WebResource> WebResources { get; set; }
        public virtual ICollection<Pack> Packs { get; set; }

        [Reactive] public uint Id { get; set; }
        [Reactive] public string? Name { get; set; }
        [Reactive] public string? Description { get; set;}
        [Reactive] public string? Icon { get; set;}
        [Reactive] public bool isDelited { get; set; }
    }
}
