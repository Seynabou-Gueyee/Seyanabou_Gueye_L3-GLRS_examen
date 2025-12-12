package services;

import java.util.List;

import entity.Burger;
import entity.Complement;
import entity.Menu;
import entity.Zone;

public interface IRessourceInitializerService {
    List<Burger> creerBurgers();
    List<Complement> creerComplements();
    List<Menu> creerMenus(List<Burger> burgers, List<Complement> complements);
    List<Zone> creerZones();
    void initialiserRessources();
}
