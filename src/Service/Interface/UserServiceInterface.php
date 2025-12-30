<?php

namespace App\Service\Interface;

use App\Entity\User;

interface UserServiceInterface
{
    public function getLivreurs(): array;
    
    public function getLivreursDisponibles(): array;
    
    public function toggleDisponibilite(int $id): User;
}
