<?php

namespace App\Repository\Impl;

use App\Entity\Complement;
use App\Repository\ComplementRepositoryInterface;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

class ComplementRepository extends ServiceEntityRepository implements ComplementRepositoryInterface
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, Complement::class);
    }

    public function findNonArchived(): array
    {
        return $this->createQueryBuilder('c')
            ->where('c.archive = :archive')
            ->setParameter('archive', false)
            ->orderBy('c.nom', 'ASC')
            ->getQuery()
            ->getResult();
    }

    public function save(Complement $complement, bool $flush = false): void
    {
        $this->getEntityManager()->persist($complement);
        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    public function remove(Complement $complement, bool $flush = false): void
    {
        $this->getEntityManager()->remove($complement);
        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }
}
