<?php

namespace App\Controller\Impl;

use App\Service\Interface\UserServiceInterface;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;

#[Route('/gestionnaire/livreur')]
class UserController extends AbstractController
{
    #[Route('/', name: 'app_livreur_index')]
    public function index(UserServiceInterface $userService): Response
    {
        $livreurs = $userService->getLivreurs();
        
        return $this->render('livreur/index.html.twig', [
            'livreurs' => $livreurs,
        ]);
    }

    #[Route('/{id}/toggle-disponibilite', name: 'app_livreur_toggle_disponibilite', methods: ['POST'])]
    public function toggleDisponibilite(int $id, UserServiceInterface $userService): Response
    {
        $livreur = $userService->toggleDisponibilite($id);
        $this->addFlash('success', 'Disponibilité modifiée avec succès');
        
        return $this->redirectToRoute('app_livreur_index');
    }
}
