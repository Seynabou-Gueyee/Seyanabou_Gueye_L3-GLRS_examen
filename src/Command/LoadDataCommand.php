<?php

namespace App\Command;

use App\Entity\Burger;
use App\Entity\Menu;
use App\Entity\Complement;
use App\Entity\Zone;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Component\Console\Attribute\AsCommand;
use Symfony\Component\Console\Command\Command;
use Symfony\Component\Console\Input\InputInterface;
use Symfony\Component\Console\Output\OutputInterface;
use Symfony\Component\Console\Style\SymfonyStyle;

#[AsCommand(
    name: 'app:load-data',
    description: 'Charger des données de test dans la base de données',
)]
class LoadDataCommand extends Command
{
    public function __construct(
        private EntityManagerInterface $em
    ) {
        parent::__construct();
    }

    protected function execute(InputInterface $input, OutputInterface $output): int
    {
        $io = new SymfonyStyle($input, $output);

        // Créer des Burgers
        $burgers = [
            ['nom' => 'Classic Burger', 'prix' => 3500],
            ['nom' => 'Cheese Burger', 'prix' => 4000],
            ['nom' => 'Double Burger', 'prix' => 5500],
            ['nom' => 'Bacon Burger', 'prix' => 4500],
            ['nom' => 'Chicken Burger', 'prix' => 4000],
            ['nom' => 'Veggie Burger', 'prix' => 3800],
        ];

        $io->section('Création des Burgers');
        foreach ($burgers as $burgerData) {
            $burger = new Burger();
            $burger->setNom($burgerData['nom']);
            $burger->setPrix($burgerData['prix']);
            $burger->setArchive(false);
            $this->em->persist($burger);
            $io->text('✓ ' . $burgerData['nom']);
        }

        // Créer des Compléments
        $complements = [
            ['nom' => 'Frites', 'prix' => 1000],
            ['nom' => 'Onion Rings', 'prix' => 1200],
            ['nom' => 'Nuggets (6 pcs)', 'prix' => 1500],
            ['nom' => 'Coca-Cola', 'prix' => 800],
            ['nom' => 'Fanta', 'prix' => 800],
            ['nom' => 'Sprite', 'prix' => 800],
            ['nom' => 'Eau minérale', 'prix' => 500],
        ];

        $io->section('Création des Compléments');
        foreach ($complements as $complementData) {
            $complement = new Complement();
            $complement->setNom($complementData['nom']);
            $complement->setPrix($complementData['prix']);
            $complement->setArchive(false);
            $this->em->persist($complement);
            $io->text('✓ ' . $complementData['nom']);
        }

        // Flush pour obtenir les IDs
        $this->em->flush();

        // Récupérer les burgers et compléments créés
        $allBurgers = $this->em->getRepository(Burger::class)->findAll();
        $allComplements = $this->em->getRepository(Complement::class)->findAll();

        // Créer des Menus
        $menusData = [
            ['nom' => 'Menu Classic', 'burgers' => [0], 'complements' => [0, 3]], // Classic + Frites + Coca
            ['nom' => 'Menu Cheese', 'burgers' => [1], 'complements' => [0, 4]], // Cheese + Frites + Fanta
            ['nom' => 'Menu Double', 'burgers' => [2], 'complements' => [1, 3]], // Double + Onion Rings + Coca
            ['nom' => 'Menu Family', 'burgers' => [0, 1], 'complements' => [0, 2, 3, 4]], // 2 burgers + frites + nuggets + 2 boissons
        ];

        $io->section('Création des Menus');
        foreach ($menusData as $menuData) {
            $menu = new Menu();
            $menu->setNom($menuData['nom']);
            $menu->setArchive(false);
            
            // Ajouter les burgers
            foreach ($menuData['burgers'] as $burgerIndex) {
                if (isset($allBurgers[$burgerIndex])) {
                    $menu->addBurger($allBurgers[$burgerIndex]);
                }
            }
            
            // Ajouter les compléments
            foreach ($menuData['complements'] as $complementIndex) {
                if (isset($allComplements[$complementIndex])) {
                    $menu->addComplement($allComplements[$complementIndex]);
                }
            }
            
            $this->em->persist($menu);
            $io->text('✓ ' . $menuData['nom'] . ' - Prix: ' . $menu->getPrix() . ' FCFA');
        }

        // Créer des Zones
        $zones = [
            ['nom' => 'Dakar Plateau', 'tarif' => 1500, 'quartiers' => 'Plateau, Rebeuss, Médina'],
            ['nom' => 'Almadies', 'tarif' => 2000, 'quartiers' => 'Almadies, Ngor, Yoff'],
            ['nom' => 'Mermoz', 'tarif' => 1500, 'quartiers' => 'Mermoz, Sacré-Cœur, SICAP'],
            ['nom' => 'Point E', 'tarif' => 1500, 'quartiers' => 'Point E, Amitié, Fann'],
            ['nom' => 'Ouakam', 'tarif' => 2000, 'quartiers' => 'Ouakam, Mamelles'],
            ['nom' => 'Liberté 6', 'tarif' => 1800, 'quartiers' => 'Liberté 6, VDN'],
        ];

        $io->section('Création des Zones');
        foreach ($zones as $zoneData) {
            $zone = new Zone();
            $zone->setNom($zoneData['nom']);
            $zone->setPrixLivraison($zoneData['tarif']);
            $zone->setQuartiers($zoneData['quartiers']);
            $this->em->persist($zone);
            $io->text('✓ ' . $zoneData['nom'] . ' - ' . $zoneData['tarif'] . ' FCFA');
        }

        $this->em->flush();

        $io->success('Toutes les données de test ont été créées avec succès !');
        $io->note('Vous pouvez maintenant voir du contenu dans toutes vos pages.');

        return Command::SUCCESS;
    }
}
