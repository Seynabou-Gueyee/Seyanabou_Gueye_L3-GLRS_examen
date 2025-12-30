<?php

namespace App\Command;

use App\Entity\User;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Component\Console\Attribute\AsCommand;
use Symfony\Component\Console\Command\Command;
use Symfony\Component\Console\Input\InputArgument;
use Symfony\Component\Console\Input\InputInterface;
use Symfony\Component\Console\Output\OutputInterface;
use Symfony\Component\Console\Style\SymfonyStyle;

#[AsCommand(
    name: 'app:update-user-role',
    description: 'Mettre à jour le rôle d\'un utilisateur',
)]
class UpdateUserRoleCommand extends Command
{
    public function __construct(
        private EntityManagerInterface $em
    ) {
        parent::__construct();
    }

    protected function configure(): void
    {
        $this
            ->addArgument('email', InputArgument::REQUIRED, 'Email de l\'utilisateur')
            ->addArgument('role', InputArgument::REQUIRED, 'Nouveau rôle (ROLE_CLIENT, ROLE_GESTIONNAIRE, ROLE_LIVREUR)');
    }

    protected function execute(InputInterface $input, OutputInterface $output): int
    {
        $io = new SymfonyStyle($input, $output);

        $email = $input->getArgument('email');
        $role = $input->getArgument('role');

        $rolesValides = ['ROLE_CLIENT', 'ROLE_GESTIONNAIRE', 'ROLE_LIVREUR'];
        if (!in_array($role, $rolesValides)) {
            $io->error('Rôle invalide. Utilisez: ROLE_CLIENT, ROLE_GESTIONNAIRE ou ROLE_LIVREUR');
            return Command::FAILURE;
        }

        $user = $this->em->getRepository(User::class)->findOneBy(['email' => $email]);

        if (!$user) {
            $io->error("Aucun utilisateur trouvé avec l'email: $email");
            return Command::FAILURE;
        }

        $user->setRoles([$role]);
        $this->em->flush();

        $io->success("Le rôle de {$user->getPrenom()} {$user->getNom()} a été mis à jour vers $role");

        return Command::SUCCESS;
    }
}
