<?php

namespace App\Service;

use App\Entity\Zone;

interface ZoneServiceInterface
{
    public function findAll(): array;
    public function find(int $id): ?Zone;
    public function create(Zone $zone): void;
    public function update(Zone $zone): void;
    public function delete(Zone $zone): void;
    public function findCommandesByZone(int $zoneId): array;
}
