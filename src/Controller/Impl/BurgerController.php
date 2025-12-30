<?php

namespace App\Controller\Impl;

use App\Entity\Burger;
use App\Form\BurgerType;
use App\Service\BurgerServiceInterface;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;

#[Route('/gestionnaire/burger')]
class BurgerController extends AbstractController
{
    #[Route('/', name: 'app_burger_index')]
    public function index(BurgerServiceInterface $service): Response
    {
        return $this->render('burger/index.html.twig', [
            'burgers' => $service->findAll(),
        ]);
    }

    #[Route('/new', name: 'app_burger_new')]
    public function new(Request $request, BurgerServiceInterface $service): Response
    {
        $burger = new Burger();
        $form = $this->createForm(BurgerType::class, $burger);
        $form->handleRequest($request);

        if ($form->isSubmitted() && $form->isValid()) {
            $service->create($burger);
            $this->addFlash('success', 'Burger ajouté avec succès');
            return $this->redirectToRoute('app_burger_index');
        }

        return $this->render('burger/new.html.twig', [
            'form' => $form->createView(),
        ]);
    }

    #[Route('/{id}/edit', name: 'app_burger_edit')]
    public function edit(Request $request, Burger $burger, BurgerServiceInterface $service): Response
    {
        $form = $this->createForm(BurgerType::class, $burger);
        $form->handleRequest($request);

        if ($form->isSubmitted() && $form->isValid()) {
            $service->update($burger);
            $this->addFlash('success', 'Burger modifié avec succès');
            return $this->redirectToRoute('app_burger_index');
        }

        return $this->render('burger/edit.html.twig', [
            'form' => $form->createView(),
            'burger' => $burger,
        ]);
    }

    #[Route('/{id}/archive', name: 'app_burger_archive')]
    public function archive(Burger $burger, BurgerServiceInterface $service): Response
    {
        $service->archive($burger->getId());
        $this->addFlash('success', 'Burger archivé avec succès');
        return $this->redirectToRoute('app_burger_index');
    }
}
