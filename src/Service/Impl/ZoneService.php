<?php

namespace App\Service\Impl;

use App\Entity\Zone;
use App\Repository\ZoneRepositoryInterface;
use App\Service\ZoneServiceInterface;
use Doctrine\ORM\EntityManagerInterface;

class ZoneService implements ZoneServiceInterface
{
    public function __construct(
        private ZoneRepositoryInterface $repository,
        private EntityManagerInterface $em
    ) {}

    public function findAll(): array
    {
        return $this->repository->findAll();
    }

    public function find(int $id): ?Zone
    {
        return $this->repository->find($id);
    }

    public function create(Zone $zone): void
    {
        $this->em->persist($zone);
        $this->em->flush();
    }

    public function update(Zone $zone): void
    {
        $this->em->flush();
    }

    public function delete(Zone $zone): void
    {
        $this->em->remove($zone);
        $this->em->flush();
    }

    public function findCommandesByZone(int $zoneId): array
    {
        $zone = $this->repository->find($zoneId);
        return $zone ? $zone->getCommandes()->toArray() : [];
    }
}
