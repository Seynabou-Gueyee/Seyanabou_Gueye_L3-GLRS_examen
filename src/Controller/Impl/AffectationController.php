<?php

namespace App\Controller\Impl;

use App\Service\CommandeServiceInterface;
use App\Service\Interface\UserServiceInterface;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;

#[Route('/gestionnaire/affectation')]
class AffectationController extends AbstractController
{
    #[Route('/', name: 'app_affectation_index')]
    public function index(CommandeServiceInterface $commandeService, UserServiceInterface $userService): Response
    {
        $commandesAAffecter = $commandeService->getCommandesAAffecter();
        $livreursDisponibles = $userService->getLivreursDisponibles();
        
        return $this->render('affectation/index.html.twig', [
            'commandes' => $commandesAAffecter,
            'livreurs' => $livreursDisponibles,
        ]);
    }
}
