<?php

namespace App\Repository;

use App\Entity\Complement;

interface ComplementRepositoryInterface
{

    public function findAll(): array;
    public function save(Complement $complement, bool $flush = false): void;
    public function remove(Complement $complement, bool $flush = false): void;
}
