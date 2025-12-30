<?php

namespace App\Repository\Impl;

use App\Entity\Burger;
use App\Repository\BurgerRepositoryInterface;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

class BurgerRepository extends ServiceEntityRepository implements BurgerRepositoryInterface
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, Burger::class);
    }

    public function findNonArchived(): array
    {
        return $this->createQueryBuilder('b')
            ->where('b.archive = :archive')
            ->setParameter('archive', false)
            ->orderBy('b.nom', 'ASC')
            ->getQuery()
            ->getResult();
    }

    public function save(Burger $burger, bool $flush = false): void
    {
        $this->getEntityManager()->persist($burger);
        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    public function remove(Burger $burger, bool $flush = false): void
    {
        $this->getEntityManager()->remove($burger);
        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }
}
