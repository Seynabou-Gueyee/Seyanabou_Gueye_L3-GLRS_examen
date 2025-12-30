<?php

namespace App\Entity;

use App\Repository\Impl\MenuRepository;
use Doctrine\Common\Collections\ArrayCollection;
use Doctrine\Common\Collections\Collection;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: MenuRepository::class)]
class Menu
{
    #[ORM\Id]
    #[ORM\GeneratedValue]
    #[ORM\Column]
    private ?int $id = null;

    #[ORM\Column(length: 150)]
    private ?string $nom = null;

    #[ORM\Column(length: 255, nullable: true)]
    private ?string $image = null;

    #[ORM\Column]
    private ?bool $archive = false;

    #[ORM\ManyToMany(targetEntity: Burger::class, inversedBy: 'menus')]
    private Collection $burgers;

    #[ORM\ManyToMany(targetEntity: Complement::class, inversedBy: 'menus')]
    private Collection $complements;

    #[ORM\OneToMany(mappedBy: 'menu', targetEntity: LigneCommande::class)]
    private Collection $ligneCommandes;

    public function __construct()
    {
        $this->burgers = new ArrayCollection();
        $this->complements = new ArrayCollection();
        $this->ligneCommandes = new ArrayCollection();
    }

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getNom(): ?string
    {
        return $this->nom;
    }

    public function setNom(string $nom): static
    {
        $this->nom = $nom;
        return $this;
    }

    public function getImage(): ?string
    {
        return $this->image;
    }

    public function setImage(?string $image): static
    {
        $this->image = $image;
        return $this;
    }

    public function isArchive(): ?bool
    {
        return $this->archive;
    }

    public function setArchive(bool $archive): static
    {
        $this->archive = $archive;
        return $this;
    }

    public function getBurgers(): Collection
    {
        return $this->burgers;
    }

    public function addBurger(Burger $burger): static
    {
        if (!$this->burgers->contains($burger)) {
            $this->burgers->add($burger);
        }
        return $this;
    }

    public function removeBurger(Burger $burger): static
    {
        $this->burgers->removeElement($burger);
        return $this;
    }

    public function getComplements(): Collection
    {
        return $this->complements;
    }

    public function addComplement(Complement $complement): static
    {
        if (!$this->complements->contains($complement)) {
            $this->complements->add($complement);
        }
        return $this;
    }

    public function removeComplement(Complement $complement): static
    {
        $this->complements->removeElement($complement);
        return $this;
    }

    public function getPrix(): float
    {
        $prix = 0;
        foreach ($this->burgers as $burger) {
            $prix += $burger->getPrix();
        }
        foreach ($this->complements as $complement) {
            $prix += $complement->getPrix();
        }
        return $prix;
    }

    public function getLigneCommandes(): Collection
    {
        return $this->ligneCommandes;
    }
}
