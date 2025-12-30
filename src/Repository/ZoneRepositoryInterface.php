<?php

namespace App\Repository;

use App\Entity\Zone;

interface ZoneRepositoryInterface
{
    public function save(Zone $zone, bool $flush = false): void;
    public function remove(Zone $zone, bool $flush = false): void;
}
