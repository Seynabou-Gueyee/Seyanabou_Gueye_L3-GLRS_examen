<?php

namespace App\Repository;

use App\Entity\Commande;

interface CommandeRepositoryInterface
{
    public function findCommandesEnCoursDuJour(): array;
    public function findCommandesValideesDuJour(): array;
    public function findCommandesAnnuleesDuJour(): array;
    public function getRecettesJournalieres(): ?float;
    public function getBurgersMenusPlusVendusDuJour(): array;
    public function filterCommandes(array $filters = []): array;
    public function save(Commande $commande, bool $flush = false): void;
    public function remove(Commande $commande, bool $flush = false): void;
    public function findCommandesAAffecter(): array;
}
