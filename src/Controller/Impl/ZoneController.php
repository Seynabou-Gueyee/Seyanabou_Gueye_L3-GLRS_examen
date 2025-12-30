<?php

namespace App\Controller\Impl;

use App\Entity\Zone;
use App\Form\ZoneType;
use App\Service\ZoneServiceInterface;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;

#[Route('/gestionnaire/zone')]
class ZoneController extends AbstractController
{
    #[Route('/', name: 'app_zone_index')]
    public function index(ZoneServiceInterface $service): Response
    {
        return $this->render('zone/index.html.twig', [
            'zones' => $service->findAll(),
        ]);
    }

    #[Route('/new', name: 'app_zone_new')]
    public function new(Request $request, ZoneServiceInterface $service): Response
    {
        $zone = new Zone();
        $form = $this->createForm(ZoneType::class, $zone);
        $form->handleRequest($request);

        if ($form->isSubmitted() && $form->isValid()) {
            $service->create($zone);
            $this->addFlash('success', 'Zone ajoutée avec succès');
            return $this->redirectToRoute('app_zone_index');
        }

        return $this->render('zone/new.html.twig', [
            'form' => $form->createView(),
        ]);
    }

    #[Route('/{id}/edit', name: 'app_zone_edit')]
    public function edit(Request $request, Zone $zone, ZoneServiceInterface $service): Response
    {
        $form = $this->createForm(ZoneType::class, $zone);
        $form->handleRequest($request);

        if ($form->isSubmitted() && $form->isValid()) {
            $service->update($zone);
            $this->addFlash('success', 'Zone modifiée avec succès');
            return $this->redirectToRoute('app_zone_index');
        }

        return $this->render('zone/edit.html.twig', [
            'form' => $form->createView(),
            'zone' => $zone,
        ]);
    }

    #[Route('/{id}/commandes', name: 'app_zone_commandes')]
    public function commandes(int $id, ZoneServiceInterface $service): Response
    {
        $zone = $service->find($id);
        $commandes = $service->findCommandesByZone($id);

        return $this->render('zone/commandes.html.twig', [
            'zone' => $zone,
            'commandes' => $commandes,
        ]);
    }
}
