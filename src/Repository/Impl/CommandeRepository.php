<?php

namespace App\Repository\Impl;

use App\Entity\Commande;
use App\Repository\CommandeRepositoryInterface;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

class CommandeRepository extends ServiceEntityRepository implements CommandeRepositoryInterface
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, Commande::class);
    }

    public function findCommandesEnCoursDuJour(): array
    {
        $today = new \DateTime('today');
        return $this->createQueryBuilder('c')
            ->where('c.dateCommande >= :today')
            ->andWhere('c.etat = :etat')
            ->setParameter('today', $today)
            ->setParameter('etat', 'En cours')
            ->getQuery()
            ->getResult();
    }

    public function findCommandesValideesDuJour(): array
    {
        $today = new \DateTime('today');
        return $this->createQueryBuilder('c')
            ->where('c.dateCommande >= :today')
            ->andWhere('c.etat = :etat')
            ->setParameter('today', $today)
            ->setParameter('etat', 'Validée')
            ->getQuery()
            ->getResult();
    }

    public function findCommandesAnnuleesDuJour(): array
    {
        $today = new \DateTime('today');
        return $this->createQueryBuilder('c')
            ->where('c.dateCommande >= :today')
            ->andWhere('c.etat = :etat')
            ->setParameter('today', $today)
            ->setParameter('etat', 'Annulée')
            ->getQuery()
            ->getResult();
    }

    public function getRecettesJournalieres(): ?float
    {
        $today = new \DateTime('today');
        return $this->createQueryBuilder('c')
            ->select('SUM(c.montantTotal) as recette')
            ->where('c.dateCommande >= :today')
            ->andWhere('c.etat != :annulee')
            ->setParameter('today', $today)
            ->setParameter('annulee', 'Annulée')
            ->getQuery()
            ->getSingleScalarResult();
    }

    public function getBurgersMenusPlusVendusDuJour(): array
    {
        $today = new \DateTime('today');
        return $this->createQueryBuilder('c')
            ->select('b.nom as burger, m.nom as menu, SUM(lc.quantite) as total')
            ->join('c.ligneCommandes', 'lc')
            ->leftJoin('lc.burger', 'b')
            ->leftJoin('lc.menu', 'm')
            ->where('c.dateCommande >= :today')
            ->andWhere('c.etat != :annulee')
            ->setParameter('today', $today)
            ->setParameter('annulee', 'Annulée')
            ->groupBy('b.id, m.id')
            ->orderBy('total', 'DESC')
            ->setMaxResults(10)
            ->getQuery()
            ->getResult();
    }

    public function filterCommandes($filters = []): array
    {
        $qb = $this->createQueryBuilder('c')
            ->leftJoin('c.client', 'cl')
            ->leftJoin('c.ligneCommandes', 'lc')
            ->leftJoin('lc.burger', 'b')
            ->leftJoin('lc.menu', 'm');

        if (isset($filters['burger']) && $filters['burger']) {
            $qb->andWhere('b.id = :burger')
                ->setParameter('burger', $filters['burger']);
        }

        if (isset($filters['menu']) && $filters['menu']) {
            $qb->andWhere('m.id = :menu')
                ->setParameter('menu', $filters['menu']);
        }

        if (isset($filters['date']) && $filters['date']) {
            $date = new \DateTime($filters['date']);
            $qb->andWhere('DATE(c.dateCommande) = :date')
                ->setParameter('date', $date->format('Y-m-d'));
        }

        if (isset($filters['etat']) && $filters['etat']) {
            $qb->andWhere('c.etat = :etat')
                ->setParameter('etat', $filters['etat']);
        }

        if (isset($filters['client']) && $filters['client']) {
            $qb->andWhere('cl.id = :client')
                ->setParameter('client', $filters['client']);
        }

        return $qb->orderBy('c.dateCommande', 'DESC')
            ->getQuery()
            ->getResult();
    }

    public function save(Commande $commande, bool $flush = false): void
    {
        $this->getEntityManager()->persist($commande);
        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    public function remove(Commande $commande, bool $flush = false): void
    {
        $this->getEntityManager()->remove($commande);
        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    public function findCommandesAAffecter(): array
    {
        return $this->createQueryBuilder('c')
            ->where('c.typeCommande = :type')
            ->andWhere('c.livreur IS NULL')
            ->andWhere('c.etat IN (:etats)')
            ->setParameter('type', 'Livraison')
            ->setParameter('etats', ['En cours', 'Validée'])
            ->orderBy('c.dateCommande', 'DESC')
            ->getQuery()
            ->getResult();
    }
}
