<?php

namespace App\Service\Impl;

use App\Entity\User;
use App\Repository\UserRepositoryInterface;
use App\Service\Interface\UserServiceInterface;
use Doctrine\ORM\EntityManagerInterface;

class UserService implements UserServiceInterface
{
    public function __construct(
        private UserRepositoryInterface $userRepository,
        private EntityManagerInterface $entityManager
    ) {}

    public function getLivreurs(): array
    {
        return $this->userRepository->findByRole('ROLE_LIVREUR');
    }

    public function getLivreursDisponibles(): array
    {
        return $this->userRepository->findLivreursDisponibles();
    }

    public function toggleDisponibilite(int $id): User
    {
        $user = $this->userRepository->find($id);
        
        if (!$user) {
            throw new \RuntimeException('Utilisateur non trouvé');
        }
        
        $user->setDisponible(!$user->isDisponible());
        $this->entityManager->flush();
        
        return $user;
    }
}
