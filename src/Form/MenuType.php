<?php

namespace App\Form;

use App\Entity\Menu;
use App\Entity\Burger;
use App\Entity\Complement;
use Symfony\Bridge\Doctrine\Form\Type\EntityType;
use Symfony\Component\Form\AbstractType;
use Symfony\Component\Form\Extension\Core\Type\FileType;
use Symfony\Component\Form\Extension\Core\Type\TextType;
use Symfony\Component\Form\FormBuilderInterface;
use Symfony\Component\OptionsResolver\OptionsResolver;
use Symfony\Component\Validator\Constraints\File;

class MenuType extends AbstractType
{
    public function buildForm(FormBuilderInterface $builder, array $options): void
    {
        $builder
            ->add('nom', TextType::class, ['label' => 'Nom du menu'])
            ->add('burgers', EntityType::class, [
                'class' => Burger::class,
                'choice_label' => 'nom',
                'multiple' => true,
                'expanded' => true,
                'label' => 'Burgers',
            ])
            ->add('complements', EntityType::class, [
                'class' => Complement::class,
                'choice_label' => 'nom',
                'multiple' => true,
                'expanded' => true,
                'label' => 'Compléments',
            ])
            ->add('image', FileType::class, [
                'label' => 'Image',
                'mapped' => false,
                'required' => false,
                'constraints' => [
                    new File([
                        'maxSize' => '2M',
                        'mimeTypes' => ['image/jpeg', 'image/png', 'image/jpg'],
                    ])
                ],
            ]);
    }

    public function configureOptions(OptionsResolver $resolver): void
    {
        $resolver->setDefaults([
            'data_class' => Menu::class,
        ]);
    }
}
