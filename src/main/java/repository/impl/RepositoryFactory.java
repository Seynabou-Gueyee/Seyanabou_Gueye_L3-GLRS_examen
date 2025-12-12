package repository.impl;
import repository.IRepositoryFactory;
import repository.IUtilisateurRepository;

public class RepositoryFactory implements IRepositoryFactory{
    private static RepositoryFactory instance;
    
    private final IUtilisateurRepository utilisateurRepo = new UtilisateurRepository();

    private RepositoryFactory() {}

    public static RepositoryFactory getInstance() {
        if (instance == null) {
            instance = new RepositoryFactory();
        }
        return instance;
    }

    @Override
    public IUtilisateurRepository getUtilisateurRepository() {
        return utilisateurRepo;
    }
}
