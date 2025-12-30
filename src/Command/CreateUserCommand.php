<?php

namespace App\Command;

use App\Entity\User;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Component\Console\Attribute\AsCommand;
use Symfony\Component\Console\Command\Command;
use Symfony\Component\Console\Input\InputInterface;
use Symfony\Component\Console\Output\OutputInterface;
use Symfony\Component\Console\Style\SymfonyStyle;
use Symfony\Component\PasswordHasher\Hasher\UserPasswordHasherInterface;

#[AsCommand(
    name: 'app:create-user',
    description: 'Créer un utilisateur gestionnaire',
)]
class CreateUserCommand extends Command
{
    public function __construct(
        private EntityManagerInterface $em,
        private UserPasswordHasherInterface $passwordHasher
    ) {
        parent::__construct();
    }

    protected function execute(InputInterface $input, OutputInterface $output): int
    {
        $io = new SymfonyStyle($input, $output);

        $user = new User();
        $user->setEmail('admin@brasilburger.com');
        $user->setNom('Admin');
        $user->setPrenom('Gestionnaire');
        $user->setTelephone('771234567');
        $user->setRoles(['ROLE_GESTIONNAIRE']);

        $hashedPassword = $this->passwordHasher->hashPassword($user, 'admin123');
        $user->setPassword($hashedPassword);

        $this->em->persist($user);
        $this->em->flush();

        $io->success('Utilisateur gestionnaire créé avec succès !');
        $io->note('Email: admin@brasilburger.com');
        $io->note('Mot de passe: admin123');

        return Command::SUCCESS;
    }
}
