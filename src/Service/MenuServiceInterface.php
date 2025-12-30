<?php

namespace App\Service;

use App\Entity\Menu;

interface MenuServiceInterface
{
    public function findAll(): array;
    public function findNonArchived(): array;
    public function find(int $id): ?Menu;
    public function create(Menu $menu): void;
    public function update(Menu $menu): void;
    public function archive(int $id): void;
    public function delete(Menu $menu): void;
}
