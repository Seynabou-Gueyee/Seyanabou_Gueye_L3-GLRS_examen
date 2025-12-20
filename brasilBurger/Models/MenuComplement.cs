namespace brasilBurger.Models
{
    public class MenuComplement
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; } = null!;

        public int ComplementId { get; set; }
        public Complement Complement { get; set; } = null!;
    }
}
