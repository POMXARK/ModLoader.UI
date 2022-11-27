using ReactiveUI.Fody.Helpers;

namespace ModLoader.Models
{
    public class Mod
    {
        [Reactive] public uint Id { get; set; }
        [Reactive] public string? Name { get; set; }
        [Reactive] public string? Description { get; set;}
        [Reactive] public string? Icon { get; set;}
        [Reactive] public bool isDelited { get; set; }
    }
}
