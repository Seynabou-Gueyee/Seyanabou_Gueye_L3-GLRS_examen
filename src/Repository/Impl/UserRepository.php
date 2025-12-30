<?php

namespace App\Repository\Impl;

use App\Entity\User;
use App\Repository\UserRepositoryInterface;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;
use Symfony\Component\Security\Core\Exception\UnsupportedUserException;
use Symfony\Component\Security\Core\User\PasswordAuthenticatedUserInterface;
use Symfony\Component\Security\Core\User\PasswordUpgraderInterface;

class UserRepository extends ServiceEntityRepository implements UserRepositoryInterface, PasswordUpgraderInterface
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, User::class);
    }

    public function upgradePassword(PasswordAuthenticatedUserInterface $user, string $newHashedPassword): void
    {
        if (!$user instanceof User) {
            throw new UnsupportedUserException(sprintf('Instances of "%s" are not supported.', $user::class));
        }

        $user->setPassword($newHashedPassword);
        $this->getEntityManager()->persist($user);
        $this->getEntityManager()->flush();
    }

    public function save(User $user, bool $flush = false): void
    {
        $this->getEntityManager()->persist($user);
        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    public function remove(User $user, bool $flush = false): void
    {
        $this->getEntityManager()->remove($user);
        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    public function findByRole(string $role): array
    {
        $conn = $this->getEntityManager()->getConnection();
        $sql = 'SELECT * FROM "user" WHERE roles::text LIKE ? ORDER BY nom ASC';
        
        $result = $conn->executeQuery($sql, ['%"' . $role . '"%']);
        
        $users = [];
        foreach ($result->fetchAllAssociative() as $row) {
            $users[] = $this->hydrateUser($row);
        }
        
        return $users;
    }

    public function findLivreursDisponibles(): array
    {
        $conn = $this->getEntityManager()->getConnection();
        $sql = 'SELECT * FROM "user" WHERE roles::text LIKE ? AND disponible = ? ORDER BY nom ASC';
        
        $result = $conn->executeQuery($sql, ['%"ROLE_LIVREUR"%', true]);
        
        $users = [];
        foreach ($result->fetchAllAssociative() as $row) {
            $users[] = $this->hydrateUser($row);
        }
        
        return $users;
    }
    
    private function hydrateUser(array $row): User
    {
        $user = new User();
        $user->setEmail($row['email']);
        $user->setPassword($row['password']);
        $user->setNom($row['nom']);
        $user->setPrenom($row['prenom']);
        $user->setTelephone($row['telephone']);
        $user->setRoles(json_decode($row['roles'], true));
        
        // Utiliser la réflexion pour définir l'ID
        $reflection = new \ReflectionClass($user);
        $idProperty = $reflection->getProperty('id');
        $idProperty->setAccessible(true);
        $idProperty->setValue($user, $row['id']);
        
        if (isset($row['disponible'])) {
            $dispProperty = $reflection->getProperty('disponible');
            $dispProperty->setAccessible(true);
            $dispProperty->setValue($user, $row['disponible']);
        }
        
        return $user;
    }
}
