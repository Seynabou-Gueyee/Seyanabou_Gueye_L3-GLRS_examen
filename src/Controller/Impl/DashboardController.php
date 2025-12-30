<?php

namespace App\Controller\Impl;

use App\Service\CommandeServiceInterface;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;

#[Route('/gestionnaire')]
class DashboardController extends AbstractController
{
    #[Route('/dashboard', name: 'app_dashboard')]
    public function index(CommandeServiceInterface $service): Response
    {
        $commandesEnCours = $service->findCommandesEnCoursDuJour();
        $commandesValidees = $service->findCommandesValideesDuJour();
        $commandesAnnulees = $service->findCommandesAnnuleesDuJour();
        $recettesJournalieres = $service->getRecettesJournalieres() ?? 0;
        $produitsPlusVendus = $service->getBurgersMenusPlusVendusDuJour();

        return $this->render('dashboard/index.html.twig', [
            'commandesEnCours' => count($commandesEnCours),
            'commandesValidees' => count($commandesValidees),
            'commandesAnnulees' => count($commandesAnnulees),
            'recettesJournalieres' => $recettesJournalieres,
            'produitsPlusVendus' => $produitsPlusVendus,
        ]);
    }
}
