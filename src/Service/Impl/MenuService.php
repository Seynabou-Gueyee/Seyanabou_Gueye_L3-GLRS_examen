<?php

namespace App\Service\Impl;

use App\Entity\Menu;
use App\Repository\MenuRepositoryInterface;
use App\Service\MenuServiceInterface;
use Doctrine\ORM\EntityManagerInterface;

class MenuService implements MenuServiceInterface
{
    public function __construct(
        private MenuRepositoryInterface $repository,
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

    public function find(int $id): ?Menu
    {
        return $this->repository->find($id);
    }

    public function create(Menu $menu): void
    {
        $this->em->persist($menu);
        $this->em->flush();
    }

    public function update(Menu $menu): void
    {
        $this->em->flush();
    }

    public function archive(int $id): void
    {
        $menu = $this->repository->find($id);
        if ($menu) {
            $menu->setArchive(!$menu->isArchive());
            $this->em->flush();
        }
    }

    public function delete(Menu $menu): void
    {
        $this->em->remove($menu);
        $this->em->flush();
    }
}
