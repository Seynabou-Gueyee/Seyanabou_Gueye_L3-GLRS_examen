<?php

namespace App\Service\Impl;

use App\Entity\Commande;
use App\Repository\CommandeRepositoryInterface;
use App\Service\CommandeServiceInterface;
use Doctrine\ORM\EntityManagerInterface;

class CommandeService implements CommandeServiceInterface
{
    public function __construct(
        private CommandeRepositoryInterface $repository,
        private EntityManagerInterface $em
    ) {}

    public function findAll(): array
    {
        return $this->repository->findAll();
    }

    public function find(int $id): ?Commande
    {
        return $this->repository->find($id);
    }

    public function findCommandesEnCoursDuJour(): array
    {
        return $this->repository->findCommandesEnCoursDuJour();
    }

    public function findCommandesValideesDuJour(): array
    {
        return $this->repository->findCommandesValideesDuJour();
    }

    public function findCommandesAnnuleesDuJour(): array
    {
        return $this->repository->findCommandesAnnuleesDuJour();
    }

    public function getRecettesJournalieres(): ?float
    {
        return $this->repository->getRecettesJournalieres();
    }

    public function getBurgersMenusPlusVendusDuJour(): array
    {
        return $this->repository->getBurgersMenusPlusVendusDuJour();
    }

    public function filterCommandes(array $filters = []): array
    {
        return $this->repository->filterCommandes($filters);
    }

    public function create(Commande $commande): void
    {
        $this->em->persist($commande);
        $this->em->flush();
    }

    public function update(Commande $commande): void
    {
        $this->em->flush();
    }

    public function annuler(int $id): void
    {
        $commande = $this->repository->find($id);
        if ($commande) {
            $commande->setEtat('Annulée');
            $this->em->flush();
        }
    }

    public function terminer(int $id): void
    {
        $commande = $this->repository->find($id);
        if ($commande) {
            $commande->setEtat('Terminée');
            $this->em->flush();
        }
    }

    public function getCommandesAAffecter(): array
    {
        return $this->repository->findCommandesAAffecter();
    }
}
