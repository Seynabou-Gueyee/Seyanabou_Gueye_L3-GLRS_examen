<?php

namespace App\Service\Impl;

use App\Entity\Burger;
use App\Repository\BurgerRepositoryInterface;
use App\Service\BurgerServiceInterface;
use Doctrine\ORM\EntityManagerInterface;

class BurgerService implements BurgerServiceInterface
{
    public function __construct(
        private BurgerRepositoryInterface $repository,
        private EntityManagerInterface $em
    ) {}

    public function findAll(): array
    {
        return $this->repository->findAll();
    }

    public function findNonArchived(): array
    {
        return $this->repository->findNonArchived();
    }

    public function find(int $id): ?Burger
    {
        return $this->repository->find($id);
    }

    public function create(Burger $burger): void
    {
        $this->em->persist($burger);
        $this->em->flush();
    }

    public function update(Burger $burger): void
    {
        $this->em->flush();
    }

    public function archive(int $id): void
    {
        $burger = $this->repository->find($id);
        if ($burger) {
            $burger->setArchive(!$burger->isArchive());
            $this->em->flush();
        }
    }

    public function delete(Burger $burger): void
    {
        $this->em->remove($burger);
        $this->em->flush();
    }
}
