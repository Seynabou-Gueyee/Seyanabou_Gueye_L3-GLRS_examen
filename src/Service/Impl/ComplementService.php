<?php

namespace App\Service\Impl;

use App\Entity\Complement;
use App\Repository\ComplementRepositoryInterface;
use App\Service\ComplementServiceInterface;
use Doctrine\ORM\EntityManagerInterface;

class ComplementService implements ComplementServiceInterface
{
    public function __construct(
        private ComplementRepositoryInterface $repository,
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

    public function find(int $id): ?Complement
    {
        return $this->repository->find($id);
    }

    public function create(Complement $complement): void
    {
        $this->em->persist($complement);
        $this->em->flush();
    }

    public function update(Complement $complement): void
    {
        $this->em->flush();
    }

    public function archive(int $id): void
    {
        $complement = $this->repository->find($id);
        if ($complement) {
            $complement->setArchive(!$complement->isArchive());
            $this->em->flush();
        }
    }

    public function delete(Complement $complement): void
    {
        $this->em->remove($complement);
        $this->em->flush();
    }
}
