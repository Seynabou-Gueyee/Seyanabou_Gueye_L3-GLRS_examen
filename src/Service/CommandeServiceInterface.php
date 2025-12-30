<?php

namespace App\Service;

use App\Entity\Commande;

interface CommandeServiceInterface
{
    public function findAll(): array;
    public function find(int $id): ?Commande;
    public function findCommandesEnCoursDuJour(): array;
    public function findCommandesValideesDuJour(): array;
    public function findCommandesAnnuleesDuJour(): array;
    public function getRecettesJournalieres(): ?float;
    public function getBurgersMenusPlusVendusDuJour(): array;
    public function filterCommandes(array $filters = []): array;
    public function create(Commande $commande): void;
    public function update(Commande $commande): void;
    public function annuler(int $id): void;
    public function terminer(int $id): void;
    public function getCommandesAAffecter(): array;
}
