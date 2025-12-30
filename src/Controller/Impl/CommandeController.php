<?php

namespace App\Controller\Impl;

use App\Service\CommandeServiceInterface;
use App\Repository\BurgerRepositoryInterface;
use App\Repository\MenuRepositoryInterface;
use App\Repository\UserRepositoryInterface;
use App\Repository\ZoneRepositoryInterface;
use App\Form\AffecterLivreurType;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;

#[Route('/gestionnaire/commande')]
class CommandeController extends AbstractController
{
    #[Route('/', name: 'app_commande_index')]
    public function index(Request $request, CommandeServiceInterface $service, BurgerRepositoryInterface $burgerRepo, MenuRepositoryInterface $menuRepo, UserRepositoryInterface $userRepo): Response
    {
        $filters = [];
        if ($request->query->get('burger')) {
            $filters['burger'] = $request->query->get('burger');
        }
        if ($request->query->get('menu')) {
            $filters['menu'] = $request->query->get('menu');
        }
        if ($request->query->get('date')) {
            $filters['date'] = $request->query->get('date');
        }
        if ($request->query->get('etat')) {
            $filters['etat'] = $request->query->get('etat');
        }
        if ($request->query->get('client')) {
            $filters['client'] = $request->query->get('client');
        }

        $commandes = empty($filters) ? $service->findAll() : $service->filterCommandes($filters);

        return $this->render('commande/index.html.twig', [
            'commandes' => $commandes,
            'burgers' => $burgerRepo->findAll(),
            'menus' => $menuRepo->findAll(),
            'clients' => $userRepo->findAll(),
        ]);
    }

    #[Route('/{id}/annuler', name: 'app_commande_annuler')]
    public function annuler($id, CommandeServiceInterface $service): Response
    {
        $service->annuler($id);
        $this->addFlash('success', 'Commande annulée avec succès');
        return $this->redirectToRoute('app_commande_index');
    }

    #[Route('/{id}/terminer', name: 'app_commande_terminer')]
    public function terminer($id, CommandeServiceInterface $service): Response
    {
        $service->terminer($id);
        $this->addFlash('success', 'Commande terminée avec succès');
        return $this->redirectToRoute('app_commande_index');
    }

    #[Route('/{id}/affecter-livreur', name: 'app_commande_affecter_livreur')]
    public function affecterLivreur(Request $request, int $id, CommandeServiceInterface $service, UserRepositoryInterface $userRepo, EntityManagerInterface $em): Response
    {
        $commande = $service->find($id);
        if (!$commande) {
            throw $this->createNotFoundException('Commande non trouvée');
        }

        // Récupérer les livreurs via le repository
        $livreurs = $userRepo->findByRole('ROLE_LIVREUR');

        $form = $this->createForm(AffecterLivreurType::class, $commande, [
            'livreurs' => $livreurs,
        ]);
        $form->handleRequest($request);

        if ($form->isSubmitted() && $form->isValid()) {
            $em->flush();
            $this->addFlash('success', 'Livreur affecté avec succès');
            return $this->redirectToRoute('app_commande_index');
        }

        return $this->render('commande/affecter_livreur.html.twig', [
            'form' => $form->createView(),
            'commande' => $commande,
        ]);
    }

    #[Route('/zone/{zoneId}/commandes-livraison', name: 'app_commande_par_zone')]
    public function commandesParZone(int $zoneId, ZoneRepositoryInterface $zoneRepo): Response
    {
        $zone = $zoneRepo->find($zoneId);
        if (!$zone) {
            throw $this->createNotFoundException('Zone non trouvée');
        }

        $commandes = $zone->getCommandes()->filter(function($commande) {
            return $commande->getTypeCommande() === 'Livraison' && !$commande->getLivreur();
        });

        return $this->render('commande/par_zone.html.twig', [
            'zone' => $zone,
            'commandes' => $commandes,
        ]);
    }
}
