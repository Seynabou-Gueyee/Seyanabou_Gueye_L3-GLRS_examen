<?php

namespace App\Service;

use App\Entity\Burger;

interface BurgerServiceInterface
{
    public function findAll(): array;
    public function findNonArchived(): array;
    public function find(int $id): ?Burger;
    public function create(Burger $burger): void;
    public function update(Burger $burger): void;
    public function archive(int $id): void;
    public function delete(Burger $burger): void;
}
