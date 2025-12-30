<?php

namespace App\Repository\Impl;

use App\Entity\Menu;
use App\Repository\MenuRepositoryInterface;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

class MenuRepository extends ServiceEntityRepository implements MenuRepositoryInterface
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, Menu::class);
    }

    public function findNonArchived(): array
    {
        return $this->createQueryBuilder('m')
            ->where('m.archive = :archive')
            ->setParameter('archive', false)
            ->orderBy('m.nom', 'ASC')
            ->getQuery()
            ->getResult();
    }

    public function save(Menu $menu, bool $flush = false): void
    {
        $this->getEntityManager()->persist($menu);
        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    public function remove(Menu $menu, bool $flush = false): void
    {
        $this->getEntityManager()->remove($menu);
        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }
}
