using brasilBurger.Models;

namespace brasilBurger.ViewModels
{
    public class CatalogueViewModel
    {
        public List<Burger> Burgers { get; set; } = new List<Burger>();
        public List<Menu> Menus { get; set; } = new List<Menu>();
        public List<Complement> Complements { get; set; } = new List<Complement>();
        public string? SearchTerm { get; set; }
        public string? Filter { get; set; } // "burger", "menu", "all"
    }

    public class BurgerDetailViewModel
    {
        public Burger Burger { get; set; } = null!;
        public List<Complement> ComplementsDisponibles { get; set; } = new List<Complement>();
    }

    public class MenuDetailViewModel
    {
        public Menu Menu { get; set; } = null!;
        public List<Complement> ComplementsInclus { get; set; } = new List<Complement>();
    }
}
