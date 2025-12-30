<?php

namespace App\Controller\Impl;

use App\Entity\Complement;
use App\Form\ComplementType;
use App\Service\ComplementServiceInterface;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;

#[Route('/gestionnaire/complement')]
class ComplementController extends AbstractController
{
    #[Route('/', name: 'app_complement_index')]
    public function index(ComplementServiceInterface $service): Response
    {
        return $this->render('complement/index.html.twig', [
            'complements' => $service->findAll(),
        ]);
    }

    #[Route('/new', name: 'app_complement_new')]
    public function new(Request $request, ComplementServiceInterface $service): Response
    {
        $complement = new Complement();
        $form = $this->createForm(ComplementType::class, $complement);
        $form->handleRequest($request);

        if ($form->isSubmitted() && $form->isValid()) {
            $service->create($complement);
            $this->addFlash('success', 'Complément ajouté avec succès');
            return $this->redirectToRoute('app_complement_index');
        }

        return $this->render('complement/new.html.twig', [
            'form' => $form->createView(),
        ]);
    }

    #[Route('/{id}/edit', name: 'app_complement_edit')]
    public function edit(Request $request, Complement $complement, ComplementServiceInterface $service): Response
    {
        $form = $this->createForm(ComplementType::class, $complement);
        $form->handleRequest($request);

        if ($form->isSubmitted() && $form->isValid()) {
            $service->update($complement);
            $this->addFlash('success', 'Complément modifié avec succès');
            return $this->redirectToRoute('app_complement_index');
        }

        return $this->render('complement/edit.html.twig', [
            'form' => $form->createView(),
            'complement' => $complement,
        ]);
    }

    #[Route('/{id}/archive', name: 'app_complement_archive')]
    public function archive(Complement $complement, ComplementServiceInterface $service): Response
    {
        $service->archive($complement->getId());
        $this->addFlash('success', 'Complément archivé avec succès');
        return $this->redirectToRoute('app_complement_index');
    }
}
