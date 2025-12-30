<?php

namespace App\Service;

use App\Entity\Complement;

interface ComplementServiceInterface
{
    public function findAll(): array;
    public function findNonArchived(): array;
    public function find(int $id): ?Complement;
    public function create(Complement $complement): void;
    public function update(Complement $complement): void;
    public function archive(int $id): void;
    public function delete(Complement $complement): void;
}
