<?php

namespace App\Repository;

use App\Entity\Menu;

interface MenuRepositoryInterface
{

    public function findAll(): array;
    public function save(Menu $menu, bool $flush = false): void;
    public function remove(Menu $menu, bool $flush = false): void;
}
