<?php

namespace App\Repository;

use App\Entity\Burger;

interface BurgerRepositoryInterface
{

    public function findAll(): array;
    public function save(Burger $burger, bool $flush = false): void;
    public function remove(Burger $burger, bool $flush = false): void;
}
