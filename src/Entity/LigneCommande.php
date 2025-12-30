<?php

namespace App\Entity;

use App\Repository\Impl\LigneCommandeRepository;
use Doctrine\Common\Collections\ArrayCollection;
use Doctrine\Common\Collections\Collection;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: LigneCommandeRepository::class)]
class LigneCommande
{
    #[ORM\Id]
    #[ORM\GeneratedValue]
    #[ORM\Column]
    private ?int $id = null;

    #[ORM\Column]
    private ?int $quantite = 1;

    #[ORM\Column]
    private ?float $prixUnitaire = 0;

    #[ORM\ManyToOne(inversedBy: 'ligneCommandes')]
    #[ORM\JoinColumn(nullable: false)]
    private ?Commande $commande = null;

    #[ORM\ManyToOne(inversedBy: 'ligneCommandes')]
    private ?Burger $burger = null;

    #[ORM\ManyToOne(inversedBy: 'ligneCommandes')]
    private ?Menu $menu = null;

    #[ORM\ManyToMany(targetEntity: Complement::class, inversedBy: 'ligneCommandes')]
    private Collection $complements;

    public function __construct()
    {
        $this->complements = new ArrayCollection();
    }

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getQuantite(): ?int
    {
        return $this->quantite;
    }

    public function setQuantite(int $quantite): static
    {
        $this->quantite = $quantite;
        return $this;
    }

    public function getPrixUnitaire(): ?float
    {
        return $this->prixUnitaire;
    }

    public function setPrixUnitaire(float $prixUnitaire): static
    {
        $this->prixUnitaire = $prixUnitaire;
        return $this;
    }

    public function getCommande(): ?Commande
    {
        return $this->commande;
    }

    public function setCommande(?Commande $commande): static
    {
        $this->commande = $commande;
        return $this;
    }

    public function getBurger(): ?Burger
    {
        return $this->burger;
    }

    public function setBurger(?Burger $burger): static
    {
        $this->burger = $burger;
        return $this;
    }

    public function getMenu(): ?Menu
    {
        return $this->menu;
    }

    public function setMenu(?Menu $menu): static
    {
        $this->menu = $menu;
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
}
