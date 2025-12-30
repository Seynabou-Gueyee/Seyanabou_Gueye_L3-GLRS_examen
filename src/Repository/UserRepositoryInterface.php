<?php

namespace App\Repository;

use App\Entity\User;

interface UserRepositoryInterface
{
    public function save(User $user, bool $flush = false): void;
    public function remove(User $user, bool $flush = false): void;
    public function findByRole(string $role): array;
    public function findLivreursDisponibles(): array;
}
