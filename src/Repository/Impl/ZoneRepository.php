<?php

namespace App\Repository\Impl;

use App\Entity\Zone;
use App\Repository\ZoneRepositoryInterface;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

class ZoneRepository extends ServiceEntityRepository implements ZoneRepositoryInterface
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, Zone::class);
    }

    public function save(Zone $zone, bool $flush = false): void
    {
        $this->getEntityManager()->persist($zone);
        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    public function remove(Zone $zone, bool $flush = false): void
    {
        $this->getEntityManager()->remove($zone);
        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }
}
