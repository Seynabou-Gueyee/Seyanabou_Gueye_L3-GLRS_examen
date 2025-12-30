<?php

namespace App\Controller\Impl;

use App\Service\CommandeServiceInterface;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;

#[Route('/gestionnaire/statistiques')]
class StatistiquesController extends AbstractController
{
    #[Route('/', name: 'app_statistiques')]
    public function index(CommandeServiceInterface $commandeService): Response
    {
        // Récupérer les statistiques
        $recettesJournalieres = $commandeService->getRecettesJournalieres() ?? 0;
        $commandesEnCours = count($commandeService->findCommandesEnCoursDuJour());
        $commandesValidees = count($commandeService->findCommandesValideesDuJour());
        $commandesAnnulees = count($commandeService->findCommandesAnnuleesDuJour());
        $produitsPlusVendus = $commandeService->getBurgersMenusPlusVendusDuJour();
        
        return $this->render('statistiques/index.html.twig', [
            'recettesJournalieres' => $recettesJournalieres,
            'commandesEnCours' => $commandesEnCours,
            'commandesValidees' => $commandesValidees,
            'commandesAnnulees' => $commandesAnnulees,
            'produitsPlusVendus' => $produitsPlusVendus,
        ]);
    }
}
