<?php

namespace App\Controller\Impl;

use App\Entity\Menu;
use App\Form\MenuType;
use App\Service\MenuServiceInterface;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;

#[Route('/gestionnaire/menu')]
class MenuController extends AbstractController
{
    #[Route('/', name: 'app_menu_index')]
    public function index(MenuServiceInterface $service): Response
    {
        return $this->render('menu/index.html.twig', [
            'menus' => $service->findAll(),
        ]);
    }

    #[Route('/new', name: 'app_menu_new')]
    public function new(Request $request, MenuServiceInterface $service): Response
    {
        $menu = new Menu();
        $form = $this->createForm(MenuType::class, $menu);
        $form->handleRequest($request);

        if ($form->isSubmitted() && $form->isValid()) {
            $service->create($menu);
            $this->addFlash('success', 'Menu ajouté avec succès');
            return $this->redirectToRoute('app_menu_index');
        }

        return $this->render('menu/new.html.twig', [
            'form' => $form->createView(),
        ]);
    }

    #[Route('/{id}/edit', name: 'app_menu_edit')]
    public function edit(Request $request, Menu $menu, MenuServiceInterface $service): Response
    {
        $form = $this->createForm(MenuType::class, $menu);
        $form->handleRequest($request);

        if ($form->isSubmitted() && $form->isValid()) {
            $service->update($menu);
            $this->addFlash('success', 'Menu modifié avec succès');
            return $this->redirectToRoute('app_menu_index');
        }

        return $this->render('menu/edit.html.twig', [
            'form' => $form->createView(),
            'menu' => $menu,
        ]);
    }

    #[Route('/{id}/archive', name: 'app_menu_archive')]
    public function archive(Menu $menu, MenuServiceInterface $service): Response
    {
        $service->archive($menu->getId());
        $this->addFlash('success', 'Menu archivé avec succès');
        return $this->redirectToRoute('app_menu_index');
    }
}
